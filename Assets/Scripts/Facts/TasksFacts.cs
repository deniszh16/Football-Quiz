using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Facts
{
    public class TasksFacts : FileProcessing
    {
        // Объект для json по заданиям
        private FactsHelper _factsHelper;

        // Объект для json по наборам
        private StatusHelper _statusHelper;

        [Header("Текст задания")]
        [SerializeField] private TextMeshProUGUI _question;

        [Header("Кнопки вариантов")]
        [SerializeField] private GameObject _variants;

        [Header("Компонент таймера")]
        [SerializeField] private Timer _timer;

        private Coroutine _coroutine;

        [Header("Победный эффект")]
        [SerializeField] private ParticleSystem _victory;

        [Header("Обновление задания")]
        [SerializeField] private Button _updateButton;

        [Header("Предупреждения за ошибки")]
        [SerializeField] private GameObject[] _cards;

        // Этап викторины
        private int _stage;
        // Предупреждения за ошибки
        private int _warnings;

        private PointsEarned _pointsEarned;

        private void Awake()
        {
            // Текст с вопросами из json файла
            var jsonString = ReadJsonFile("facts-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _factsHelper, jsonString);

            _statusHelper = JsonUtility.FromJson<StatusHelper>(PlayerPrefs.GetString("facts"));
            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
        }

        private void Start()
        {
            _timer.TimeIsOver.AddListener(LevelFailed);

            CustomizeTask();
        }

        /// <summary>
        /// Настройка текущего задания
        /// </summary>
        private void CustomizeTask()
        {
            _question.text = _factsHelper.Facts[_stage].Question;
            _variants.SetActive(true);

            // Запускаем отсчет уровня
            _coroutine = StartCoroutine(_timer.Countdown());
        }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        /// <param name="state">состояние кнопки</param>
        public void CheckAnswer(bool state)
        {
            // Останавливаем отсчет
            StopCoroutine(_coroutine);

            if (_factsHelper.Facts[_stage].Answer == state)
            {
                _pointsEarned.ChangeQuantityCoins(50);
                _pointsEarned.ChangeTotalScore(5);

                _victory.Play();

                _question.text = _factsHelper.Facts[_stage].Description;

                PlayerPrefs.SetInt("facts-answer", PlayerPrefs.GetInt("facts-answer") + 1);
            }
            else
            {
                _warnings++;
                // Отображаем карточку предупреждения
                _cards[_warnings - 1].SetActive(true);

                PlayerPrefs.SetInt("facts-errors", PlayerPrefs.GetInt("facts-errors") + 1);

                _pointsEarned.ChangeQuantityCoins(-20);

                if (_warnings >= 2)
                {
                    _question.text = (_timer.Seconds > 0) ?
                        "Неправильно!" + IndentsHelpers.LineBreak(1) + "Получена красная карточка, подборка провалена." :
                        "Время закончилось!" + IndentsHelpers.LineBreak(1) + "Получено предупреждение за затяжку времени, подборка провалена.";

                    CloseCategory("loss");
                    return;
                }
                else
                {
                    _question.text = (_timer.Seconds > 0) ?
                        "Неправильно!" + IndentsHelpers.LineBreak(1) + "Получена первая желтая карточка." :
                        "Время закончилось!" + IndentsHelpers.LineBreak(1) + "Получена первая желтая карточка.";
                }
            }

            _stage++;
            _updateButton.interactable = true;
        }

        /// <summary>
        /// Завершение уровня с проигрышем
        /// </summary>
        private void LevelFailed()
        {
            _variants.SetActive(false);
            CheckAnswer(!_factsHelper.Facts[_stage].Answer);
        }

        /// <summary>
        /// Закрытие доступа к текущей категории
        /// </summary>
        /// <param name="result">результат подборки</param>
        private void CloseCategory(string result)
        {
            _statusHelper.status[Sets.Category] = result;
            // Сохраняем обновленное значение
            PlayerPrefs.SetString("facts", JsonUtility.ToJson(_statusHelper));
            PlayerPrefs.SetInt("facts-quantity", PlayerPrefs.GetInt("facts-quantity") + 1);
        }

        /// <summary>
        /// Переход к следующему заданию
        /// </summary>
        public void ShowNextTask()
        {
            if (_stage < _factsHelper.Facts.Length)
            {
                _timer.ResetTimer();

                CustomizeTask();
            }
            else
            {
                _question.text = "Великолепно!" + IndentsHelpers.LineBreak(1) + "Данная подборка фактов успешно пройдена.";
                PlayerPrefs.SetInt("facts-victory", PlayerPrefs.GetInt("facts-victory") + 1);

                _pointsEarned.ChangeQuantityCoins(350);
                _pointsEarned.ChangeTotalScore(100);

                CloseCategory("victory");
            }
        }
    }
}