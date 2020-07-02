using System;

namespace Cubra.Helpers
{
    [Serializable]
    public class FactsHelper
    {
        public FactItems[] Facts;
    }

    [Serializable]
    public class FactItems
    {
        // Вопрос задания
        public string Question;

        // Проверка ответа
        public bool Answer;

        // Описание ответа
        public string Description;
    }
}