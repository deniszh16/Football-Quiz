using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using Firebase.Analytics;

namespace Cubra.Legends
{
    public class Legends : IncreaseQuantityItems
    {
        // Выбранная карточка
        public static int Card;

        // Позиция скролла в списке карточек
        public static float ScrollPosition = 1;

        [Header("Набор карточек")]
        [SerializeField] private Legend[] _cards;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Анимация открытия")]
        [SerializeField] private Animator _victory;

        private StatusHelper _statusHelper;
        private PointsEarned _pointsEarned;

        private void Awake()
        {
            _statusHelper = new StatusHelper();
            _statusHelper = JsonUtility.FromJson<StatusHelper>(PlayerPrefs.GetString("legends"));

            _pointsEarned = Camera.main.GetComponent<PointsEarned>();
        }

        private void Start()
        {
            // Проверяем необходимость увеличения списка карточек
            AddToList(_statusHelper, _cards.Length, "legends");

            _scrollRect.verticalNormalizedPosition = ScrollPosition;

            CheckLegendaryCards();
        }

        /// <summary>
        /// Проверка легендарных карточек
        /// </summary>
        private void CheckLegendaryCards()
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                if (_statusHelper.status[i] == "yes") _cards[i].ShowImageCard();
            }
        }

        /// <summary>
        /// Открытие легендарной карточки
        /// </summary>
        /// <param name="number">номер карточки</param>
        public void OpenLegendaryCard(int number)
        {
            if (_statusHelper.status[number] == "no")
            {
                BuyCard(number);
            }
            else
            {
                if (_cards[number].Biography)
                {
                    ScrollPosition = _scrollRect.verticalNormalizedPosition;

                    Card = number;
                    Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.Biography);
                }
            }
        }

        /// <summary>
        /// Покупка закрытой карточки
        /// </summary>
        /// <param name="number">номер карточки</param>
        private void BuyCard(int number)
        {
            if (PlayerPrefs.GetInt("coins") >= 950)
            {
                _pointsEarned.ChangeQuantityCoins(-950);
                _pointsEarned.ChangeTotalScore(500);

                _statusHelper.status[number] = "yes";
                SaveListStatuses(_statusHelper, "legends");

                PlayerPrefs.SetInt("legends-open", PlayerPrefs.GetInt("legends-open") + 1);

                _cards[number].ShowImageCard();
                ShowOpeningEffect(_cards[number].transform);

                // Событие (для статистики) по покупке легендарной карточки
                FirebaseAnalytics.LogEvent("buying_card");
            }
            else
            {
                _pointsEarned.ShowCurrentQuantityCoins(-950);
            }
        }

        /// <summary>
        /// Отображение эффекта открытия
        /// </summary>
        /// <param name="card">карточка</param>
        private void ShowOpeningEffect(Transform card)
        {
            _victory.transform.position = card.transform.position;
            _victory.transform.SetParent(card.transform.parent);
            _victory.transform.SetSiblingIndex(0);

            _victory.Rebind();
        }
    }
}