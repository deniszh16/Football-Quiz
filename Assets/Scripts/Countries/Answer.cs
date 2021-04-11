using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using AppodealAds.Unity.Api;
using TMPro;

namespace Cubra.Countries
{
    public class Answer : MonoBehaviour
    {
        // Событие при правильном ответе
        public UnityEvent AnswerReceived;

        // Массив букв, открытых игроком
        private string[] _playerAnswer;

        public int AnswerLength => _playerAnswer.Length;

        // Номера открытых букв
        private int[] _letterNumbers;

        // Количество открытых букв
        private int _quantityLetters;

        [Header("Поле ответа")]
        [SerializeField] private TextMeshProUGUI _answer;

        [Header("Кнопка удаления буквы")]
        [SerializeField] private DeletingLetters _deleteButton;

        [Header("Подробный ответ")]
        [SerializeField] private TextMeshProUGUI _detailedAnswer;

        [Header("Победный эффект")]
        [SerializeField] private ParticleSystem _victory;

        private TasksCountries _tasksCountries;
        private PointsEarned _pointsEarned;

        private void Awake()
        {
            _tasksCountries = Camera.main.GetComponent<TasksCountries>();
            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
        }

        /// <summary>
        /// Настройка ответа под задание
        /// </summary>
        /// <param name="type">тип задания</param>
        /// <param name="letters">количество букв в ответе</param>
        public void CustomizeAnswer(string type, int letters = 0)
        {
            if (type == "letters")
            {
                // Создаем массив под ответ
                _playerAnswer = new string[letters];
                _letterNumbers = new int[letters];

                UpdateAnswerField();
            }
            else
            {
                _answer.text = "*   *   *";
            }
        }

        /// <summary>
        /// Обновление текстового поля ответа
        /// </summary>
        public void UpdateAnswerField()
        {
            _answer.text = "";

            for (int i = 0; i < _playerAnswer.Length; i++)
            {
                _answer.text += _playerAnswer[i] ?? "*";

                // Если символ не последний, добавляем отступ между буквами
                if (i < _playerAnswer.Length - 1) _answer.text += "   ";
            }
        }

        /// <summary>
        /// Удаление последней буквы из пользовательского ответа
        /// </summary>
        public void RemoveLetterFromAnswer()
        {
            if (_quantityLetters - 1 >= 0)
            {
                // Стираем последнюю букву
                _playerAnswer[_quantityLetters - 1] = null;
                UpdateAnswerField();

                // Восстанавливаем скрытую букву
                _deleteButton.RecoverLetter(_letterNumbers[_quantityLetters - 1]);
                // Уменьшаем количество открытых букв
                _quantityLetters--;

                _answer.outlineWidth = 0;
            }
        }

        /// <summary>
        /// Сброс ответа игрока (выбранных букв)
        /// </summary>
        public void ResetPlayerAnswer()
        {
            // Очищаем массивы с выбранными буквами
            Array.Clear(_playerAnswer, 0, _playerAnswer.Length);
            Array.Clear(_letterNumbers, 0, _letterNumbers.Length);

            _quantityLetters = 0;

            UpdateAnswerField();
            _answer.outlineWidth = 0;
        }

        /// <summary>
        /// Запись выбранной буквы в ответ
        /// </summary>
        /// <param name="position">позиция в массиве ответа</param>
        /// <param name="number">номер выбранной кнопки</param>
        /// <param name="letter">буква для записи в ответ</param>
        /// <returns>результат записи</returns>
        public bool WriteLetterInAnswer(int position, int number, string letter)
        {
            // Если ячейка в указанной позиции пуста
            if (_playerAnswer[position] == null)
            {
                _playerAnswer[position] = letter;
                _quantityLetters++;

                // Запоминаем номер выбранной буквы
                _letterNumbers[_quantityLetters - 1] = number;

                UpdateAnswerField();
                CheckPlayerAnswer();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка буквенного ответа игрока
        /// </summary>
        /// <param name="skipQuestion">пропуск вопроса</param>
        public void CheckPlayerAnswer(bool skipQuestion = false)
        {
            var correctAnswer = _tasksCountries.QuestionsHelpers.TaskItems[_tasksCountries.Progress - 1].Answer;

            // Если правильный ответ совпадает с ответом игрока
            if (correctAnswer.SequenceEqual(_playerAnswer))
            {
                CorrectAnswerAccepted(skipQuestion);
            }
            // Если последняя буква заполнена
            else if (_playerAnswer.Last() != null)
            {
                _answer.outlineWidth = 0.25f;
                // Увеличиваем количество ошибок в викторине
                PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
            }
        }

        /// <summary>
        /// Проверка ответа с вариантами 
        /// </summary>
        /// <param name="number">номер вопроса</param>
        public void CheckPlayerAnswer(int number)
        {
            if (_tasksCountries.QuestionsHelpers.TaskItems[_tasksCountries.Progress - 1].Correct == number)
            {
                CorrectAnswerAccepted();
            }
            else
            {
                _pointsEarned.ChangeQuantityCoins(-20);
                PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
            }
        }

        /// <summary>
        /// Действия при получении правильного ответа
        /// </summary>
        /// <param name="skipQuestion">пропуск вопроса</param>
        private void CorrectAnswerAccepted(bool skipQuestion = false)
        {
            AnswerReceived?.Invoke();

            // Если не было пропуска, воспроизводим эффект
            if (skipQuestion == false) _victory.Play();

            // Выводим ответ и полное описание ответа
            _answer.text = _tasksCountries.QuestionsHelpers.TaskItems[_tasksCountries.Progress - 1].FullAnswer;
            _detailedAnswer.text = _tasksCountries.QuestionsHelpers.TaskItems[_tasksCountries.Progress - 1].Description;

            // Увеличиваем прогресс викторины
            IncreaseProgress(skipQuestion);

            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                // Каждый шестой уровень показываем рекламу
                if (PlayerPrefs.GetString("show-ads") == "yes" && _tasksCountries.Progress % 6 == 0)
                {
                    if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
                        Appodeal.show(Appodeal.INTERSTITIAL);
                }
            }
        }

        /// <summary>
        /// Увеличение прогресса викторины
        /// </summary>
        /// <param name="skipQuestion">пропуск вопроса</param>
        private void IncreaseProgress(bool skipQuestion = false)
        {
            if (skipQuestion == false)
            {
                // Увеличиваем счет и монеты
                _pointsEarned.ChangeTotalScore(5);
                _pointsEarned.ChangeQuantityCoins(50);

                // Увеличиваем количество правильных ответов
                PlayerPrefs.SetInt("countries-answer", PlayerPrefs.GetInt("countries-answer") + 1);
            }

            // Увеличиваем прогресс категории
            _tasksCountries.SetsHelper.arraySets[Sets.Category]++;
            PlayerPrefs.SetString("sets", JsonUtility.ToJson(_tasksCountries.SetsHelper));
        }

        /// <summary>
        /// Пропуск текущего задания
        /// </summary>
        public void GetRightAnswer()
        {
            // Получаем правильный ответ на задание
            _playerAnswer = _tasksCountries.QuestionsHelpers.TaskItems[_tasksCountries.Progress - 1].Answer;
            CheckPlayerAnswer(true);
        }
    }
}