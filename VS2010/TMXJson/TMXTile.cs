
namespace TMXJson
{
    public class TMXTile
    {
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
