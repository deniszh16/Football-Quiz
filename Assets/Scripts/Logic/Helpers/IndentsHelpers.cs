namespace Logic.Helpers
{
    public static class IndentsHelpers
    {
        public static string LineBreak(int repeat) =>
            new string('\n', repeat);
        
        public static string Underscore(int repeat) =>
            new string('-', repeat);
    }
}