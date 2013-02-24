#region Header
/*
 * TMXJsonReader.cs
 * Author: Will McCullough
 * Last Modified: 2/18/2013
 * 
 * This class provides functionality to load in .json strings that are generated
 * by the Tiled Map Editor
 */

#endregion

#region Using Statements
using Newtonsoft.Json.Linq;
using System.Collections.Generic; 
#endregion

namespace TMXJson
{
    public class TMXJSonReader
    {
        #region Methods
        /// <summary>
        /// Loads a file containing a .json string generated from Tiled map editor
        /// </summary>
        /// <param name="filename">The file that will be loaded</param>
        /// <returns>TMXMap</returns>
        public TMXMap Load(string filename)
        {
            TMXMap result = null;

            JObject mapJSON = new JObject().FromFile(filename);

            if (mapJSON != null)
            {
                Dictionary<string, string> mapProperties = new Dictionary<string, string>();

                int mapWidth = int.Parse(mapJSON["width"].ToString());
                int mapHeight = int.Parse(mapJSON["height"].ToString());
                int tileWidth = int.Parse(mapJSON["tilewidth"].ToString());
                int tileHeight = int.Parse(mapJSON["tileheight"].ToString());

                JObject propertiesJson = (JObject)mapJSON["properties"];

                string orientation = mapJSON["orientation"].ToString();
                TMXOrientationType orientationType = TMXOrientationType.Orthogonal;

                if (orientation == "orthogonal")
                {
                    orientationType = TMXOrientationType.Orthogonal;
                }

                float version = (float)mapJSON["version"];


                foreach (var kvp in propertiesJson)
                {
                    mapProperties.Add(kvp.Key, kvp.Value.ToString());
                }

                result = new TMXMap(mapWidth, mapHeight, tileWidth, tileHeight, mapProperties, orientationType, version);

                JArray tilesetJson = (JArray)mapJSON["tilesets"];

                foreach (JObject tilesets in tilesetJson)
                {
                    int tilesetTileWidth = (int)tilesets["tilewidth"];
                    int tilesetTileHeight = (int)tilesets["tileheight"];
                    int tilesetImageWidth = (int)tilesets["imagewidth"];
                    int tilesetImageHeight = (int)tilesets["imageheight"];
                    int margin = (int)tilesets["margin"];
                    int firstgid = (int)tilesets["firstgid"];
                    int spacing = (int)tilesets["spacing"];
                    string name = tilesets["name"].ToString();
                    string imagePath = tilesets["image"].ToString();

                    JObject tilesetProperties = (JObject)tilesets["properties"];
                    Dictionary<string, string> tilesetPropertiesCollection = new Dictionary<string, string>();

                    foreach (var prop in tilesetProperties)
                    {
                        tilesetPropertiesCollection.Add(prop.Key, prop.Value.ToString());
                    }

                    JObject tilesetTileProperties = (JObject)tilesets["tileproperties"];
                    Dictionary<string, string> tilesetTilePropertiesCollection = new Dictionary<string, string>();

                    if (tilesetTileProperties != null)
                    {
                        foreach (var prop in tilesetTileProperties)
                        {
                            int numericKey = int.Parse(prop.Key);

                            numericKey += 1;
                            tilesetTilePropertiesCollection.Add(numericKey.ToString(), prop.Value.ToString());
                        }
                    }

                    TMXTileset tileset = new TMXTileset(name, tilesetTileWidth, tilesetTileHeight, tilesetImageWidth, tilesetImageHeight,
                        imagePath, margin, firstgid, spacing, tilesetPropertiesCollection, tilesetTilePropertiesCollection);

                    result.AddTileset(tileset);
                }

                foreach (JObject layer in mapJSON["layers"])
                {
                    JArray dataJson = (JArray)layer["data"];
                    string name = layer["name"].ToString();
                    int layerWidth = (int)layer["width"];
                    int layerHeight = (int)layer["height"];
                    float opacity = (float)layer["opacity"];
                    string layerType = layer["type"].ToString();
                    TMXLayerType tmxLayerType = TMXLayerType.Tile;
                    bool layerVisible = (bool)layer["visible"];

                    if (layerType == "tilelayer")
                    {
                        tmxLayerType = TMXLayerType.Tile;
                    }
                    else if (layerType == "objectlayer" || layerType == "objectgroup")
                    {
                        tmxLayerType = TMXLayerType.Object;
                    }

                    int[,] tileData = new int[layerWidth, layerHeight];

                    int i = 0;

                    for (int y = 0; y < layerHeight; y++)
                    {
                        for (int x = 0; x < layerWidth; x++)
                        {
                            if (dataJson != null) tileData[x, y] = (int)dataJson[i];
                            i++;
                        }
                    }


                    TMXLayer tmxLayer = new TMXLayer(name, layerWidth, layerHeight, tileData, opacity, tmxLayerType, layerVisible);

                    //If this is an object layer, build the objects
                    if (tmxLayerType == TMXLayerType.Object)
                    {
                        foreach (JObject objectLayerObject in layer["objects"])
                        {
                            Dictionary<string, object> objectProps = new Dictionary<string, object>();

                            foreach (var objectKVP in objectLayerObject)
                            {
                                if (objectKVP.Key == "properties")
                                {
                                    JObject objectPropertyValues = JObject.Parse(objectKVP.Value.ToString());

                                    Dictionary<string, string> objectPropertyColection =
                                        new Dictionary<string, string>();

                                    foreach (var token in objectPropertyValues)
                                    {
                                        objectPropertyColection.Add(token.Key, token.Value.ToString());
                                    }

                                    objectProps.Add(objectKVP.Key, objectPropertyColection);
                                }
                                else
                                {
                                    objectProps.Add(objectKVP.Key, objectKVP.Value.ToString());
                                }
                                
                            }

                            tmxLayer.AddObject(objectProps["name"].ToString(), objectProps);
                        }
                    }

                    result.AddLayer(tmxLayer);
                }



            }

            return result;
        } 
        #endregion
    }
}
