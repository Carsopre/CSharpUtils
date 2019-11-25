using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace TestUtils
{
    public static class TestHelper
    {
        /// <summary>
        /// Creates the local copy of a single (test) file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String.</returns>
        public static string CreateLocalCopySingleFile(string filePath)
        {
            var newPath = Path.Combine(Environment.CurrentDirectory, Path.GetFileName(filePath));
            File.Copy(filePath, newPath, true);
            return newPath;
        }

        /// <summary>
        /// Get's the path in test-data tree section
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>System.String.</returns>
        public static string GetTestFilePath(string filename)
        {
            return Path.Combine(GetDataDir(), filename);
        }

        /// <summary>
        /// Gets the test-data directory for the current test project.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">
        /// Could not get stacktrace.
        /// or
        /// Could not determine the test method.
        /// or
        /// Could not find test class type.
        /// </exception>
        public static string GetDataDir()
        {
            var stackFrames = new StackTrace().GetFrames();
            if (stackFrames == null) throw new Exception("Could not get stacktrace.");

            var testMethod = stackFrames.FirstOrDefault(f => f.GetMethod().GetCustomAttributes(typeof(TestAttribute), true).Any() ||
                                                             f.GetMethod().GetCustomAttributes(typeof(SetUpAttribute), true).Any() ||
                                                             f.GetMethod().GetCustomAttributes(typeof(TestFixtureSetUpAttribute), true).Any() ||
                                                             f.GetMethod().DeclaringType.GetCustomAttributes(typeof(TestFixtureAttribute), true).Any());

            if (testMethod == null) throw new Exception("Could not determine the test method.");

            var testClassType = testMethod.GetMethod().DeclaringType;
            if (testClassType == null) throw new Exception("Could not find test class type.");

            return Path.GetFullPath(string.Format("test-data\\{0}", testClassType.Assembly.GetName().Name));
        }
    }
}