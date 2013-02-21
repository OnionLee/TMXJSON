#region Header
/*
 * TMXLayer.cs
 * Author: Will McCullough
 * Last Modified: 2/18/2013
 * 
 * This class represents a map layer in a TMX file
 */

#endregion

using System.Collections.Generic;

namespace TMXJson
{
    public class TMXLayer
    {
        /// <summary>
        /// The name of the layer
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The width of the layer
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the layer
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The tile index data for the layer
        /// </summary>
        public int[,] Data { get; private set; }

        /// <summary>
        /// The layer type
        /// </summary>
        public TMXLayerType Type { get; private set; }

        /// <summary>
        /// The opacity of the layer
        /// </summary>
        public float Opacity { get; private set; }

        /// <summary>
        /// The visibility of the layer
        /// </summary>
        public bool Visible { get; private set; }

        public Dictionary<string, Dictionary<string, string>> Objects { get; private set; }

        /// <summary>
        /// Constructs a new layer with the specified values
        /// </summary>
        /// <param name="name">The name of the layer</param>
        /// <param name="width">The width of the layer</param>
        /// <param name="height">The height of the layer</param>
        /// <param name="data">The tile index data for the layer</param>
        /// <param name="opacity">The opacity for the layer</param>
        /// <param name="type">The type of layer</param>
        /// <param name="visible">The visibility for the layer</param>
        public TMXLayer(string name, int width, int height, int[,] data, float opacity, TMXLayerType type, bool visible)
        {
            this.Name = name;

            this.Width = width;

            this.Height = height;

            this.Data = data;

            this.Opacity = opacity;

            this.Type = type;

            this.Visible = visible;

            Objects = new Dictionary<string, Dictionary<string, string>>();
        }

        /// <summary>
        /// Sets the opacity to a value between 0.0 and 1.0
        /// </summary>
        /// <param name="opacity"></param>
        public void SetOpacity(float opacity)
        {
            if (opacity > 1.0)
                this.Opacity = 1.0f;

            if (opacity < 0.0)
                this.Opacity = 0.0f;

            this.Opacity = opacity;
        }

        /// <summary>
        /// Sets the layer visibility to the specified value
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisible(bool visible)
        {
            this.Visible = visible;
        }

        /// <summary>
        /// Toggles the layer visibility
        /// </summary>
        public void ToggleVisible()
        {
            this.Visible = !this.Visible;
        }

        /// <summary>
        /// Returns the tile index at the specified location
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetTileIndex(int x, int y)
        {
            return Data[x, y];
        }

        public string GetObjectProperty(string name, string key)
        {
            if (Objects.ContainsKey(name))
            {
                Dictionary<string, string> props = Objects[name];
                   
                if (props.ContainsKey(key))
                {
                    return props[key];
                }
            }


            return "";
        }

        public void AddObject(string name, Dictionary<string,string> properties)
        {
            Objects.Add(name, properties);
        }
    }
}
