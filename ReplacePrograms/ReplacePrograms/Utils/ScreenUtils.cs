namespace ReplacePrograms.Utils
{
    public static class ScreenUtils
    {
        /// <summary>
        /// Calculate Centerposition with current Window.
        /// </summary>
        /// <param name="screenWidth">Width of the monitor screen.</param>
        /// <param name="windowWidth">Width of the program screen.</param>
        /// <param name="screenHeight">Heigth of the monitor screen.</param>
        /// <param name="windowHeight">Heigth of the program screen.</param>
        /// <returns></returns>
        public static (int x, int y) CenterPosition(int screenWidth, int windowWidth, int screenHeight, int windowHeight)
        {
            int x = ((int)((screenWidth - windowWidth) / 2));
            int y = ((int)((screenHeight - windowHeight) / 2));

            return (x, y);
        }
    }
}