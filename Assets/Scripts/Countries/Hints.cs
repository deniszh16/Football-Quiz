using UnityEngine;
using UnityEngine.UI;

namespace Cubra.Countries
{
    public class Hints : MonoBehaviour
    {
        // Отображение панели подсказок
        private bool _displayHints;

        [Header("Кнопки подсказок")]
        [SerializeField] private Button[] _buttonsHints;

        [Header("Стоимость подсказок")]
        [SerializeField] private Hint[] _hints;

        [Header("Анимация панели подсказок")]
        [SerializeField] private Animator _panelAnimation;

        [Header("Панель букв")]
        [SerializeField] private GameObject _letters;

        [Header("Выделенная буква")]
        [SerializeField] private Sprite _highlighted;

        private TasksCountries _tasksCountries;
        private PointsEarned _pointsEarned;

        private Image _imageButton;

        private void Awake()
        {
            _tasksCountries = Camera.main.GetComponent<TasksCountries>();
            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
            _imageButton = GetComponent<Image>();
        }

        /// <summary>
        /// Открытие/закрытие панели подсказок
        /// </summary>
        public void SwitchToolbar()
        {
            _displayHints = !_displayHints;

            if (_displayHints == true)
            {
                CheckHints();
                // Уменьшаем прозрачность кнопки
                _imageButton.color = new Color(255, 255, 255, 0.45f);
            }
            else
            {
                _imageButton.color = Color.white;
            }

            _panelAnimation.enabled = true;
            _panelAnimation.SetBool("Open", _displayHints);
        }

        /// <summary>
        /// Закрытие панели подсказок при нажатии на букву
        /// </summary>
        public void CloseHintsPanel()
        {
            if (_displayHints) SwitchToolbar();
        }

        /// <summary>
        /// Проверка подсказок на доступность
        /// </summary>
        private void CheckHints()
        {
            for (int i = 0; i < _hints.Length; i++)
            {
                // Если недостаточно монет или подсказка уже использована
                if (_hints[i].Cost > PlayerPrefs.GetInt("coins") || _hints[i].Activity == false)
                {
                    _buttonsHints[i].interactable = false;
                    _buttonsHints[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Подсказка для выделения первой буквы ответа
        /// </summary>
        public void OpenFirstLetter()
        {
            UseGameHint(ref _hints[0]);

            var letter = _tasksCountries.FindFirstLetter();
            // Если буква найдена, выделяем спрайт кнопки
            if (letter > 0) _letters.transform.GetChild(letter).GetComponent<Image>().sprite = _highlighted;
        }

        /// <summary>
        /// Подсказка для удаление всех лишних букв
        /// </summary>
        public void DeleteExtraLetters()
        {
            UseGameHint(ref _hints[1]);
            HideExtraLetters();
        }

        /// <summary>
        /// Подсказка для пропуска задания
        /// </summary>
        public void SkipQuestion()
        {
            UseGameHint(ref _hints[2]);
            // Получаем правильный ответ
            _tasksCountries.Answer.GetRightAnswer();
        }

        /// <summary>
        /// Использование подсказки
        /// </summary>
        /// <param name="number">подсказка</param>
        private void UseGameHint(ref Hint hint)
        {
            // Вычитаем стоимость подсказки
            _pointsEarned.ChangeQuantityCoins(-hint.Cost);
            // Отключаем повторное использование
            hint.Activity = false;

            SwitchToolbar();

            // Если выбран не пропуск
            if (hint.Pass == false)
            {
                ShowAllLetters();

                // Если уже были скрыты лишние буквы, повторяем скрытие
                if (_hints[1].Activity == false) HideExtraLetters();

                // Увеличиваем количество использованных подсказок
                PlayerPrefs.SetInt("countries-tips", PlayerPrefs.GetInt("countries-tips") + 1);
            }
            else
            {
                // Увеличиваем количество пропусков
                PlayerPrefs.SetInt("countries-pass", PlayerPrefs.GetInt("countries-pass") + 1);
            }

            // Сбрасываем ответ игрока
            _tasksCountries.Answer.ResetPlayerAnswer();
        }

        /// <summary>
        /// Восстановление выбранных букв
        /// </summary>
        private void ShowAllLetters()
        {
            for (int i = 0; i < 12; i++)
                _letters.transform.GetChild(i).gameObject.SetActive(true);
        }

        /// <summary>
        /// Скрытие букв, не входящих в ответ
        /// </summary>
        private void HideExtraLetters()
        {
            for (int i = 0; i < _letters.transform.childCount; i++)
            {
                // Ищем указанную букву в массиве ответа
                var letter = _tasksCountries.FindSpecifiedLetter(i);
                // Если буква не найдена, скрываем кнопку с этой буквой
                if (letter < 0) _letters.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}