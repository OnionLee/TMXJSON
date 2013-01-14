using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMXJson
{
    public class TMXTileset
    {
        public string Name { get; private set; }

        public int TileWidth { get; private set; }

        public int TileHeight { get; private set; }

        public int ImageWidth { get; private set; }

        public int ImageHeight { get; private set; }

        public string ImagePath { get; private set; }

        public int Margin { get; private set; }

        public int FirstGrid { get; private set; }

        public int Spacing { get; private set; }

        public Dictionary<string, string> Properties { get; private set; }

        public Dictionary<string, string> TileProperties { get; private set; }

        public TMXTile[] Tiles { get; private set; }

        public TMXTileset(string name, int tileWidth, int tileHeight, int imageWidth, int imageHeight, string imagePath, int margin, int firstgid, int spacing, Dictionary<string, string> properties, Dictionary<string, string> tileProperties)
        {
            this.Name = name;

            this.TileWidth = tileWidth;

            this.TileHeight = tileHeight;

            this.ImageWidth = imageWidth;

            this.ImageHeight = imageHeight;

            this.ImagePath = imagePath;

            this.Margin = margin;

            this.FirstGrid = firstgid;

            this.Spacing = spacing;

            this.Properties = properties;

            this.TileProperties = tileProperties;

            Tiles = new TMXTile[(imageWidth / tileWidth * imageHeight / tileHeight) + 1];

            int i = 1;
            Tiles[i] = new TMXTile(0, 0, 0, 0, 0);

            for (int y = 0; y < imageHeight / tileHeight; y++)
            {
                for (int x = 0; x < imageWidth / tileWidth; x++)
                {
                    Tiles[i] = new TMXTile(i, x * tileWidth, y * tileHeight, tileWidth, tileHeight);

                    i++;
                }
            }
        }

        public TMXTile GetTile(int gid)
        {
            return Tiles.First(t => t.ID == gid);
        }
    }
}
