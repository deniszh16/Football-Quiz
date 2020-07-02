using System;

namespace Cubra.Countries
{
    [Serializable]
    public struct Hint
    {
        // Стоимость
        public int Cost;

        // Активность подсказки
        public bool Activity;

        // Пропуск задания
        public bool Pass;
    }
}