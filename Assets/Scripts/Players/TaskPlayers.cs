using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Players
{
    public class TaskPlayers : FileProcessing
    {
        // Объект для json по вопросам
        private PlayersHelpers _playersHelpers;

        [Header("Количество заданий")]
        [SerializeField] private Tasks _task;

        [Header("Текст вопроса")]
        [SerializeField] private TextMeshProUGUI _question;

        // Кнопка обновления задания
        private Button _updateTask;

        [Header("Кнопки с фотографиями")]
        [SerializeField] private PhotoButton[] _photoButtons;

        [Header("Наборы фотографий")]
        [SerializeField] private Photos[] _listPhotos;

        // Активный набор фотографий
        private Photos _photos;

        [Header("Текст описания")]
        [SerializeField] private TextMeshProUGUI _description;

        // Количество ответов
        private int _target;
        // Количество попыток
        private int _attempts;

        // Объект для json прогресса подборки
        private SetsHelper _setsHelper;
        // Текущий прогресс
        private int _progress;

        private PointsEarned _pointsEarned;

        private void Awake()
        {
            _playersHelpers = new PlayersHelpers();

            // Текст с вопросами из json файла
            var jsonString = ReadJsonFile("players-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _playersHelpers, jsonString);

            _setsHelper = new SetsHelper();
            _setsHelper = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("photo-quiz"));

            _progress = _setsHelper.arraySets[Sets.Category - 1];

            // Получаем набор фотографий под уровни
            _photos = _listPhotos[Sets.Category - 1];

            _updateTask = _question.gameObject.GetComponent<Button>();
            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
        }

        private void Start()
        {
            ShowNextTask();
        }

        /// <summary>
        /// Настройка текущего задания
        /// </summary>
        private void CustomizeTask()
        {
            _question.text = _playersHelpers.PhotoTasks[_progress].Question;

            for (int i = 0; i < _photoButtons.Length; i++)
            {
                // Устанавливаем фотографии для задания
                _photoButtons[i].Image.sprite = _photos[_playersHelpers.PhotoTasks[_progress].Options[i]];

                _photoButtons[i].Image.color = Color.white;
                _photoButtons[i].Button.interactable = true;
                _photoButtons[i].Frame.gameObject.SetActive(false);
            }

            // Устанавливаем количество ответов и попыток в задании
            _target = _playersHelpers.PhotoTasks[_progress].QuantityAnswers;
            _attempts = _playersHelpers.PhotoTasks[_progress].Attempts;

            ShowLevelStats();
        }

        /// <summary>
        /// Отображение статистики по уровню
        /// </summary>
        private void ShowLevelStats()
        {
            _description.text = "Осталось найти: " + _target + IndentsHelpers.LineBreak(1);
            _description.text += "Доступно попыток: " + _attempts;
        }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        /// <param name="number">выбранная фотография</param>
        public void CheckAnswer(PhotoButton photoButton)
        {
            _attempts--;

            photoButton.Button.interactable = false;

            // Если выбранная фотография правильная
            if (_playersHelpers.PhotoTasks[_progress].Answers[photoButton.Number - 1] == true)
            {
                _target--;
                PlayerPrefs.SetInt("photo-quiz-answer", PlayerPrefs.GetInt("photo-quiz-answer") + 1);
            }
            else
            {
                photoButton.Image.color = new Color(1, 1, 1, 0.7f);
                PlayerPrefs.SetInt("photo-quiz-errors", PlayerPrefs.GetInt("photo-quiz-errors") + 1);
            }

            // Отображаем рамку с результатом
            photoButton.ShowImageFrame(_playersHelpers.PhotoTasks[_progress].Answers[photoButton.Number - 1]);
            CheckQuantityAnswers();

            ShowLevelStats();
        }

        /// <summary>
        /// Проверка количества ответов
        /// </summary>
        private void CheckQuantityAnswers()
        {
            if (_target > 0)
            {
                if (_attempts < _target) ShowResults(false);
            }
            else
            {
                ShowResults(true);
            }
        }

        /// <summary>
        /// Отображение результатов уровня
        /// </summary>
        /// <param name="victory">итог уровня</param>
        private void ShowResults(bool victory)
        {
            ShowCorrectPhotos();

            if (victory == true)
            {
                _question.text = "Уровень пройден!" + IndentsHelpers.LineBreak(1);
                PlayerPrefs.SetInt("photo-quiz-successfully", PlayerPrefs.GetInt("photo-quiz-successfully") + 1);
                
                _pointsEarned.ChangeQuantityCoins(50);
                _pointsEarned.ChangeTotalScore(5);
            }
            else
            {
                _question.text = "Уровень провален!" + IndentsHelpers.LineBreak(1);
                _pointsEarned.ChangeQuantityCoins(-20);
            }

            _question.text += _playersHelpers.PhotoTasks[_progress].Description;

            _progress++;
            _setsHelper.arraySets[Sets.Category - 1]++;
            PlayerPrefs.SetString("photo-quiz", JsonUtility.ToJson(_setsHelper));

            _updateTask.interactable = true;
        }

        /// <summary>
        /// Отображение правильных фотографий
        /// </summary>
        private void ShowCorrectPhotos()
        {
            for (int i = 0; i < _photoButtons.Length; i++)
            {
                _photoButtons[i].Button.interactable = false;

                // Если фотография не является ответом, скрываем ее
                if (_playersHelpers.PhotoTasks[_progress].Answers[i] == false)
                {
                    _photoButtons[i].Image.color = new Color(1, 1, 1, 0);
                    _photoButtons[i].Frame.gameObject.SetActive(false);
                }
                else
                {
                    _photoButtons[i].ShowImageFrame(true);
                }
            }
        }

        /// <summary>
        /// Переход к следующему заданию
        /// </summary>
        public void ShowNextTask()
        {
            _updateTask.interactable = false;

            // Если прогресс не превышает количество заданий
            if (_progress < _task[Sets.Category - 1])
            {
                CustomizeTask();
            }
            else
            {
                Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.PlayersResult);
            }
        }
    }
}