using System;
using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;

namespace Cubra.Countries
{
    public class TasksCountries : FileProcessing
    {
        // Объект для json по вопросам
        private QuestionsHelpers _questionsHelpers;

        public QuestionsHelpers QuestionsHelpers => _questionsHelpers;

        // Объект для json по наборам уровней
        public SetsHelper SetsHelper { get; private set; }

        [Header("Панель букв")]
        [SerializeField] private GameObject _letters;

        [Header("Панель вариантов")]
        [SerializeField] private GameObject _variants;

        [Header("Кнопка подсказок")]
        [SerializeField] private GameObject _buttonTips;

        [Header("Кнопка удаления букв")]
        [SerializeField] private GameObject _buttonDelete;

        [Header("Компонент вопроса")]
        [SerializeField] private Question _question;

        [Header("Компонент ответа")]
        [SerializeField] private Answer _answer;

        public Answer Answer => _answer;

        // Прогресс категории
        public int Progress { get; private set; }

        // Первая буква ответа
        private string _firstLetter;

        private void Awake()
        {
            _questionsHelpers = new QuestionsHelpers();

            // Текст с вопросами из json файла
            var jsonString = ReadJsonFile("category-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _questionsHelpers, jsonString);

            SetsHelper = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("sets"));
            Progress = SetsHelper.arraySets[Sets.Category];
        }

        private void Start()
        {
            CustomizeScene();

            // Если прогресс не превышает количество вопросов
            if (Progress - 1 < _questionsHelpers.TaskItems.Length)
            {
                // Отображаем следующий вопрос
                _question.ShowQuestion(_questionsHelpers.TaskItems[Progress - 1].Question);
            }
        }

        /// <summary>
        /// Настройка сцены для текущего вопроса
        /// </summary>
        public void CustomizeScene()
        {
            if (_questionsHelpers.TaskItems[Progress - 1].Type == "letters")
            {
                _letters.SetActive(true);
                _buttonTips.SetActive(true);
                _buttonDelete.SetActive(true);

                // Получаем первую букву ответа (для подсказки)
                _firstLetter = _questionsHelpers.TaskItems[Progress - 1].Answer[0];

                // Настраиваем ответ под задание
                _answer.CustomizeAnswer(_questionsHelpers.TaskItems[Progress - 1].Type, _questionsHelpers.TaskItems[Progress - 1].Answer.Length);

                // Расставляем буквы
                ArrangeLettersOfTask();
            }
            else
            {
                _variants.SetActive(true);
                _answer.CustomizeAnswer(_questionsHelpers.TaskItems[Progress - 1].Type);

                // Расставляем варианты
                ArrangeOptionsOfTask();
            }
        }

        /// <summary>
        /// Расставление букв для задания
        /// </summary>
        private void ArrangeLettersOfTask()
        {
            for (int i = 0; i < _letters.transform.childCount; i++)
                _letters.transform.GetChild(i).GetComponentInChildren<Text>().text = _questionsHelpers.TaskItems[Progress - 1].Letters[i];
        }

        /// <summary>
        /// Расставление вариантов ответа для задания
        /// </summary>
        private void ArrangeOptionsOfTask()
        {
            for (int i = 0; i < _variants.transform.childCount; i++)
                _variants.transform.GetChild(i).GetComponentInChildren<Text>().text = _questionsHelpers.TaskItems[Progress - 1].Options[i];
        }

        /// <summary>
        /// Поиск первой буквы ответа
        /// </summary>
        public int FindFirstLetter()
        {
            return Array.IndexOf(_questionsHelpers.TaskItems[Progress - 1].Letters, _firstLetter);
        }

        /// <summary>
        /// Поиск указанной буквы в массиве ответа
        /// </summary>
        /// <param name="number">номер буквы</param>
        public int FindSpecifiedLetter(int number)
        {
            return Array.IndexOf(_questionsHelpers.TaskItems[Progress - 1].Answer, _questionsHelpers.TaskItems[Progress - 1].Letters[number]);
        }

        /// <summary>
        /// Переход к следующему заданию
        /// </summary>
        public void ShowNextQuestion()
        {
            if (Progress < _questionsHelpers.TaskItems.Length)
            {
                Camera.main.GetComponent<TransitionsManager>().RestartScene();
            }
            else
            {
                // Начисляем бонус за пройденную категорию
                Camera.main.GetComponent<PointsEarned>().ChangeQuantityCoins(500);
                // Переходим в список пройденных вопросов
                Camera.main.GetComponent<TransitionsManager>().GoToScene(4);
            }
        }
    }
}