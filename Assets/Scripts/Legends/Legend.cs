using UnityEngine;
using UnityEngine.UI;

namespace Cubra.Legends
{
    public class Legend : MonoBehaviour
    {
        [Header("Открытая карточка")]
        [SerializeField] private Sprite _openCard;

        [Header("Биография игрока")]
        [SerializeField] private bool _biography;

        public bool Biography => _biography;

        [Header("Идентификатор достижения")]
        [SerializeField] private string _achievement;

        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Отображение открытой карточки
        /// </summary>
        public void ShowImageCard()
        {
            _image.sprite = _openCard;

            if (Application.internetReachability != NetworkReachability.NotReachable)
                GooglePlayServices.UnlockingAchievement(_achievement);
        }
    }
}