using System;
using System.IO;

namespace WeekInserterDocsify
{
    /// <summary>
    /// Updates all _sidebar.md files with the desired week number in a parent directory
    /// </summary>
    public class Updater
    {
        #region Properties

        /// <summary>
        /// Parent directory where all documentation files are stored
        /// </summary>
        public string Folderpath { get; set; }

        /// <summary>
        /// Week that needs to be added
        /// </summary>
        public int Week { get; set; }

        #endregion

        #region Public metohds

        /// <summary>
        /// Custom constructor for Updater class
        /// Object creation might be cancelled if invalid folderpath is passed
        /// </summary>
        /// <param name="folderpath">Folder path that contains all files for documentation</param>
        /// <param name="week">Week to add</param>
        public Updater(string folderpath, int week)
        {
            //Cancel object creation if the specified directory cannot be found (file path invalid)
            if (!Directory.Exists(folderpath))
                throw new DirectoryNotFoundException("Could not find the specified directory!");

            //if there is a / resp. \ at the end -> remove it
            Folderpath = folderpath.EndsWith(Path.DirectorySeparatorChar) ? folderpath.Substring(0, folderpath.Length - 1) : folderpath;
            Week = week;
        }

        /// <summary>
        /// Public update method that exposes and calls the private update method
        /// </summary>
        public void Update()
        {
            _update(Folderpath);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Searches all sidebar files and appends desired week
        /// </summary>
        /// <param name="rootpath"></param>
        private void _update(string rootpath)
        {
            //the path for the potential sidebar file is generated
            //Path.DirectorySeparatorChar returns the respective character for Unix (/) or Windows (\)
            string sidebarPath = rootpath + Path.DirectorySeparatorChar + "_sidebar.md";

            //Append text to file if file is found
            if (File.Exists(sidebarPath))
            {
                using StreamWriter sw = File.AppendText(sidebarPath);
                sw.WriteLine($"    * [Woche {Week}](Woche{Week}/)");
            }

            //recursive call for all further directories
            foreach (var subDir in Directory.GetDirectories(rootpath))
                _update(subDir);
        }

        #endregion
    }
}
