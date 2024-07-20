using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.Players
{
    public class VariantButton : MonoBehaviour
    {
        public bool CorrectVariant => _correctVariant;
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        
        [Header("Элементы кнопки")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _imageButton;
        [SerializeField] private Image _frame;
        
        [Header("Варианты рамок")]
        [SerializeField] private Sprite _victoryFrame;
        [SerializeField] private Sprite _losingFrame;
        
        private bool _correctVariant;

        private void Awake() =>
            _button.onClick.AddListener(PushButton);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(PushButton);

        public void CustomizeButton(Sprite sprite, bool correctVariant)
        {
            _imageButton.sprite = sprite;
            _imageButton.color = Color.white;
            _correctVariant = correctVariant;
            _frame.gameObject.SetActive(false);
        }

        public void ToggleButton(bool state) =>
            _button.interactable = state;

        private void PushButton()
        {
            _answer.CheckAnswer(this);
            ToggleButton(state: false);
        }

        public void ShowFrame()
        {
            _frame.gameObject.SetActive(true);
            _frame.sprite = CorrectVariant ? _victoryFrame : _losingFrame;

            if (CorrectVariant == false)
                _imageButton.color = new Color(255,255,255,0.7f);
        }
    }
}