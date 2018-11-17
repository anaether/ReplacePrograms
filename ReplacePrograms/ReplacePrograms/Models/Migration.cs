namespace ReplacePrograms.Models
{
    public class Migration
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public bool CreateSymbolicLink { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="source">Source path from the program that we want to migrate.</param>
        /// <param name="destination">Destination path for the program that we want to migrate to.</param>
        public Migration(string source, string destination)
        {
            this.Source = source;
            this.Destination = destination;
            this.CreateSymbolicLink = false;
        }

        /// <summary>
        /// 2. Constructor
        /// </summary>
        /// <param name="source">Source path from the program that we want to migrate.</param>
        /// <param name="destination">Destination path for the program that we want to migrate to.</param>
        /// <param name="symboliclink">Do you want to create a symbolic link?</param>
        public Migration(string source, string destination, bool symboliclink)
        {
            this.Source = source;
            this.Destination = destination;
            this.CreateSymbolicLink = symboliclink;
        }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Migration()
        {
            this.Source = string.Empty;
            this.Destination = string.Empty;
            this.CreateSymbolicLink = false;
        }
    }
}