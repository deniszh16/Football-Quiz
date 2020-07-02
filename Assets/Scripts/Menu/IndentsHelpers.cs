namespace Cubra.Helpers
{
    public sealed class IndentsHelpers
    {
        /// <summary>
        /// Создание переноса строки 
        /// </summary>
        /// <param name="repeat">количество повторов</param>
        public static string LineBreak(int repeat)
        {
            return new string('\n', repeat);
        }

        /// <summary>
        /// Создание отделяющих черточек
        /// </summary>
        /// <param name="repeat">количество повторов</param>
        public static string Underscore(int repeat)
        {
            return new string('-', repeat);
        }
    }
}