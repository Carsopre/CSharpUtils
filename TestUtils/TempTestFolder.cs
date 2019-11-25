using System;
using System.IO;

namespace TestUtils
{
    /// <summary>
    /// Creates a temporary folder that, when disposed, removes all the data in it,
    /// regardless of the test result.
    /// </summary>
    public class TempTestFolder : IDisposable
    {
        public readonly string DirectoryPath;

        /// <summary>
        /// Creates an instance of a disposable object which
        /// creates a new temporary directory.
        /// </summary>
        public TempTestFolder()
        {
            const string testFolder = "TempTestData";
            DirectoryPath = Path.Combine(
                Environment.CurrentDirectory,
                Path.GetFileName(testFolder));

            if (Directory.Exists(DirectoryPath))
            {
                CleanDirectory();
            }
            Directory.CreateDirectory(DirectoryPath);
        }

        /// <summary>
        /// Creates the local copy of a single (test) file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        public string CreateLocalCopySingleFile(string filePath)
        {
            if (filePath == null)
                return null;
            var newPath = Path.Combine(DirectoryPath, Path.GetFileName(filePath));
            File.Copy(filePath, newPath, true);
            return newPath;
        }

        private void CleanDirectory()
        {
            DirectoryInfo di = new DirectoryInfo(DirectoryPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// Removes all files created within the directory and the
        /// directory itself.
        /// </summary>
        public void Dispose()
        {
            if (!Directory.Exists(DirectoryPath))
                return;
            CleanDirectory();
        }
    }
}