using System.Collections.Generic;

namespace TMXJson
{
    public sealed class TMXMap
    {
        /// <summary>
        /// The width of the map
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The height of the map
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The tile width for the map
        /// </summary>
        public int TileWidth { get; private set; }

        /// <summary>
        /// The tile height for the map
        /// </summary>
        public int TileHeight { get; private set; }

        /// <summary>
        /// The individual layers of data for the map
        /// </summary>
        public List<TMXLayer> Layers { get; private set; }

        /// <summary>
        /// The tilesets used by the map
        /// </summary>
        public List<TMXTileset> Tilesets { get; private set; }

        /// <summary>
        /// The properties for the map
        /// </summary>
        public Dictionary<string, string> Properties { get; private set; }

        /// <summary>
        /// The visual orientation of the map
        /// </summary>
        public TMXOrientationType Orientation { get; private set; }

        /// <summary>
        /// The version of the map
        /// </summary>
        public float Version { get; private set; }

        /// <summary>
        /// Constructs a new TMX Map
        /// </summary>
        /// <param name="width">The width of the map</param>
        /// <param name="height">The height of the map</param>
        /// <param name="tileWidth">The tile width for the map</param>
        /// <param name="tileHeight">The tile height for the map</param>
        /// <param name="properties">The individual properties for the map</param>
        /// <param name="orientation">The visual orientation of the map</param>
        /// <param name="version">The version of the map</param>
        public TMXMap(int width, int height, int tileWidth, int tileHeight, Dictionary<string, string> properties, TMXOrientationType orientation, float version)
        {
            this.Width = width;
            this.Height = height;
            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;
            this.Properties = properties;
            this.Orientation = orientation;
            this.Version = version;

            Layers = new List<TMXLayer>();
            Tilesets = new List<TMXTileset>();
        }

        /// <summary>
        /// Adds a new layer to the map
        /// </summary>
        /// <param name="layer"></param>
        public void AddLayer(TMXLayer layer)
        {
            Layers.Add(layer);
        }

        /// <summary>
        /// Adds a new tileset to the map
        /// </summary>
        /// <param name="tileset"></param>
        public void AddTileset(TMXTileset tileset)
        {
            Tilesets.Add(tileset);
        }
    }
}
