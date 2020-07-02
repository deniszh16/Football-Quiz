using System;

namespace Cubra.Helpers
{
    [Serializable]
    public class QuestionsHelpers
    {
        public TaskItems[] TaskItems;
    }

    [Serializable]
    public class TaskItems
    {
        // Тип вопроса
        public string Type;

        // Вопрос
        public string Question;
        // Буквы в задании
        public string[] Letters;
        // Ответ
        public string[] Answer;

        // Варианты ответа
        public string[] Options;
        // Номер ответа
        public int Correct;

        // Полный ответ
        public string FullAnswer;
        // Описание ответа
        public string Description;
    }
}