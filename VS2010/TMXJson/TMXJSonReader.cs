using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMXJson
{
    public class TMXJSonReader
    {

        public JObject LoadJson(string filename)
        {
            string fileText = File.ReadAllText(filename);

            JObject result = JObject.Parse(fileText);

            return result;
        }

        public TMXMap Load(string filename)
        {
            TMXMap result = null; 

            JObject mapJSON = LoadJson(filename);

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
                    mapProperties.Add(kvp.Key.ToString(), kvp.Value.ToString());
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
                        tilesetPropertiesCollection.Add(prop.Key.ToString(), prop.Value.ToString());
                    }

                    JObject tilesetTileProperties = (JObject)tilesets["tileproperties"];
                    Dictionary<string, string> tilesetTilePropertiesCollection = new Dictionary<string, string>();

                    foreach (var prop in tilesetTileProperties)
                    {
                        tilesetTilePropertiesCollection.Add(prop.Key.ToString(), prop.Value.ToString());
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
                    else if (layerType == "objectlayer")
                    {
                        tmxLayerType = TMXLayerType.Object;
                    }

                    int[,] tileData = new int[layerWidth, layerHeight];

                    int i = 0;

                    for (int y = 0; y < layerHeight; y++)
                    {
                        for (int x = 0; x < layerWidth; x++)
                        {
                            tileData[x, y] = (int)dataJson[i];
                            i++;
                        }
                    }

                    TMXLayer tmxLayer = new TMXLayer(name, layerWidth, layerHeight, tileData, opacity, tmxLayerType, layerVisible);

                    result.AddLayer(tmxLayer);
                }

               

            }

            return result;
        }
    }
}
