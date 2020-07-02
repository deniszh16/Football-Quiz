using Cubra.Helpers;
using UnityEngine;

namespace Cubra
{
    public class Sets : MonoBehaviour
    {
        // Выбранная категории
        public static int Category;
        
        [Header("Категории по странам")]
        [SerializeField] private Tasks _task;

        public Tasks Task => _task;

        [Header("Эффект открытия")]
        [SerializeField] private Animator _effect;

        public Animator Effect => _effect;

        [Header("Анимация текста")]
        [SerializeField] private Animator _textAnimation;

        public Animator TextAnimation => _textAnimation;

        // Объект для json по прогрессу
        public SetsHelper SetsHelper { get; private set; }

        public PointsEarned PointsEarned { get; private set; }

        private void Awake()
        {
            SetsHelper = new SetsHelper();
            SetsHelper = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("sets"));

            PointsEarned = Camera.main.GetComponent<PointsEarned>();
        }
    }
}