namespace DZGames.Football.Helpers
{
    public static class IndentsHelpers
    {
        public static string LineBreak(int repeat) =>
            new('\n', repeat);
        
        public static string Underscore(int repeat) =>
            new('-', repeat);
    }
}