using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaGoat
{
    class Level
    {
        bool[,] levelWalls;
        int width, height;
        MyGameWindow gameWindow;

        public Level(MyGameWindow gameWindow, string fileName)
        {
            this.gameWindow = gameWindow;
            Bitmap image = new Bitmap(fileName);
            width = image.Width;
            height = image.Height;
            levelWalls = new bool[image.Width, image.Height];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    bool isWall = false;
                    uint c = (uint)image.GetPixel(x, image.Height - y - 1).ToArgb();
                    if (c == 0xff000000)
                        isWall = true;

                    levelWalls[x, y] = isWall;

                    if (isWall)
                        new Entity(gameWindow, new Vector2(x, y), Vector2.One, 0);
                }
            }
        }

        public bool Touching(Entity entity)
        {
            Vector2 min = entity.Position;
            Vector2 max = entity.Position + entity.Scale;

            int minX = (int)min.X, minY = (int)min.Y;
            int maxX = (int)max.X, maxY = (int)max.Y;

            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                {
                    if(x > 
                    if (levelWalls[x, y])
                        return true;
                }
            return false;
        }
    }
}
