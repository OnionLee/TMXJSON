#region Header
/*
 * TMXTile.cs
 * Author: Will McCullough
 * Last Modified: 2/18/2013
 * 
 * This class provides functionality to load in .json strings that are generated
 * by the Tiled Map Editor
 */

#endregion

namespace TMXJson
{
    public class TMXTile
    {
        /// <summary>
        /// The ID of the tile, a numerical value that is stored in the layers of a Tiled map
        /// </summary>
        public int ID { get; private set; }

        public int X { get; private set; }

        public int Y { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public TMXTile(int id, int x, int y, int width, int height)
        {
            this.ID = id;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
