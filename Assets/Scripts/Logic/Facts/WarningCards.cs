using Logic.Helpers;
using UnityEngine;

namespace Logic.Facts
{
    public class WarningCards : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        
        [Header("Футбольные карточки")]
        [SerializeField] private GameObject _yellowCard;
        [SerializeField] private GameObject _redCard;
        
        public int ReceivedCards => _receivedCards;
        private int _receivedCards;

        public void GetCurrentCards()
        {
            _receivedCards = _tasks.ProgressService.UserProgress.FactsData
                .ReceivedCards[_tasks.CurrentCategory - ForArrays.MinusOne];
            ShowCard();
        }

        public void AddCard()
        {
            _receivedCards++;
            ShowCard();
        }

        private void ShowCard()
        {
            if (_receivedCards == 0)
            {
                _yellowCard.SetActive(false);
                _redCard.SetActive(false);
            }
            else if (_receivedCards == 1)
            {
                _yellowCard.SetActive(true);
                _redCard.SetActive(false);
            }
            else if (_receivedCards == 2)
            {
                _yellowCard.SetActive(true);
                _redCard.SetActive(true);
            }
        }
    }
}