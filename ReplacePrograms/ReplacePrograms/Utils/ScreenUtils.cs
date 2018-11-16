using System;
using System.Drawing;

namespace ReplacePrograms.Utils
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Calculate Centerposition with current Window. (deprecated)
        /// </summary>
        /// <param name="screenWidth">Width of the monitor screen.</param>
        /// <param name="windowWidth">Width of the program screen.</param>
        /// <param name="screenHeight">Heigth of the monitor screen.</param>
        /// <param name="windowHeight">Heigth of the program screen.</param>
        /// <returns></returns>
        public static Point CenterPosition(int screenWidth, int screenHeight, int windowWidth, int windowHeight)
        {
            int x = ((screenWidth / 2) - (windowWidth / 2));
            int y = ((screenHeight / 2) - (windowHeight / 2));

            Console.WriteLine("x: {0}", x);
            Console.WriteLine("y: {0}", y);

            Point location = new Point(x, y);
            return location;
        }
    }
}