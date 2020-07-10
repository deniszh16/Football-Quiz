using UnityEngine;
using UnityEngine.UI;

namespace Cubra.Players
{
    public class PhotoButton : MonoBehaviour
    {
        [Header("Номер кнопки")]
        [SerializeField] private int _number;

        public int Number => _number;

        [Header("Рамка результата")]
        [SerializeField] private Image _frame;

        public Image Frame => _frame;

        [Header("Спрайты рамок")]
        [SerializeField] private Sprite[] _frames;

        public Image Image { get; private set; }
        public Button Button { get; private set; }

        private void Awake()
        {
            Image = GetComponent<Image>();
            Button = GetComponent<Button>();
        }

        /// <summary>
        /// Отображение рамки результата
        /// </summary>
        /// <param name="correctAnswer">правильный ответ</param>
        public void ShowImageFrame(bool correctAnswer)
        {
            _frame.gameObject.SetActive(true);
            _frame.sprite = correctAnswer ? _frames[0] : _frames[1];
        }
    }
}