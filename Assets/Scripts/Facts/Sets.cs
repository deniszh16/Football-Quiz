using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;

namespace Cubra.Facts
{
    public class Sets : IncreaseQuantityItems
    {
        // Выбранная подборка
        public static int Category;

        [Header("Список подборок")]
        [SerializeField] private Tasks _task;

        [Header("Кнопки подборок")]
        [SerializeField] private Image[] _facts;

        [Header("Спрайты результата")]
        [SerializeField] private Sprite[] _results;

        // Объект для json по наборам фактов
        private StatusHelper _statusHelper;

        private void Awake()
        {
            _statusHelper = new StatusHelper();
            _statusHelper = JsonUtility.FromJson<StatusHelper>(PlayerPrefs.GetString("facts"));
        }

        private void Start()
        {
            // Проверяем необходимость увеличения списка подборок
            AddToList(_statusHelper, _facts.Length, "facts");

            CheckCollectionsFacts();
        }

        /// <summary>
        /// Проверка доступности подборок фактов
        /// </summary>
        private void CheckCollectionsFacts()
        {
           for (int i = 0; i < _facts.Length; i++)
           {
                // Если подборка пройдена
                if (_statusHelper.status[i] != "no")
                {
                    // Отключаем текст с количеством вопросов
                    _facts[i].transform.GetChild(0).gameObject.SetActive(false);

                    var result = _facts[i].transform.GetChild(1).GetComponent<Image>();
                    // Убираем прозрачность
                    result.color = Color.white;
                    // Устанавливаем изображение в зависимости от результата
                    result.sprite = _results[(_statusHelper.status[i] == "victory") ? 0 : 1];
                }
                else
                {
                    // Выводим количество заданий в категории
                    _facts[i].transform.GetChild(0).GetComponent<Text>().text = "0 / " + _task[i];
                }
            }
        }

        /// <summary>
        /// Открытие подборки фактов
        /// </summary>
        /// <param name="number">номер подборки</param>
        public void OpenCollectionFacts(int number)
        {
            Category = number;

            // Если подборка не пройдена
            if (_statusHelper.status[number] == "no")
            {
                // Переходим к викторине
                Camera.main.GetComponent<TransitionsManager>().GoToScene(13);
            }
            else
            {
                // Переходим к результатам
                Camera.main.GetComponent<TransitionsManager>().GoToScene(14);
            }
        }
    }
}