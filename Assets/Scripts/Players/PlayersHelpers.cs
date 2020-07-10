using System;

namespace Cubra.Helpers
{
    [Serializable]
    public class PlayersHelpers
    {
        public PhotoTasks[] PhotoTasks;
    }

    [Serializable]
    public class PhotoTasks
    {
        // Вопрос задания
        public string Question;

        // Варианты в задании
        public int[] Options;

        // Номера ответов
        public bool[] Answers;

        // Количество ответов
        public int QuantityAnswers;

        // Количество попыток
        public int Attempts;

        // Описание ответов
        public string Description;
    }
}