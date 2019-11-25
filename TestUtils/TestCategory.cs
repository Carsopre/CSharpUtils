namespace TestUtils
{
    /// <summary>
    /// Tags for test categorization following the V-Model approach.
    /// https://en.wikipedia.org/wiki/V-Model_(software_development)
    /// </summary>
    public class TestCategory
    {
        public const string Unit = "Build.Unit";
        public const string Integration = "Build.Integration";
        public const string System = "Build.System";
        public const string Acceptance = "Build.Acceptance";
        public const string Gui = "Build.Gui";

        public const string WorkInProgress = "Build.WorkInProgress";
    }
}
