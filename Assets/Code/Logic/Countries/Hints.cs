using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Logic.Countries
{
    public class Hints : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private AnswerFromLetters _answerFromLetters;
        [SerializeField] private HintBase[] _hintsButtons;
        
        [Header("Кнопка подсказок")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;
        
        [Header("Анимация попапа")]
        [SerializeField] private Animator _popupAnimation;

        public event Action PopupOpened;
        
        private static readonly int Open = Animator.StringToHash("Open");
        private readonly Color _buttonColor = new(255, 255, 255, 0.45f);
        
        public bool IsPopupClosed => _isPopupClosed;
        private bool _isPopupClosed = true;
        
        public void CustomizeButton()
        {
            _button.onClick.AddListener(SwitchPopup);
            _answerFromLetters.TaskCompleted += HideHintsButtons;
            ResetHintsAvailability();
        }
        
        public void SwitchPopup()
        {
            StopAllCoroutines();
            _popupAnimation.SetBool(id: Open, value: _isPopupClosed);
            StartCoroutine(ToggleHintButtons(state: _isPopupClosed));
            
            if (_isPopupClosed)
            {
                _buttonImage.color = _buttonColor;
                PopupOpened?.Invoke();
            }
            else
            {
                _buttonImage.color = Color.white;
            }

            _isPopupClosed = !_isPopupClosed;
        }
        
        private IEnumerator ToggleHintButtons(bool state)
        {
            float delay = 0;
            if (state) delay = 0.35f;
            
            yield return new WaitForSeconds(delay);
            foreach (HintBase button in _hintsButtons)
                button.ButtonGameObject.SetActive(state);
        }
        
        private void HideHintsButtons()
        {
            gameObject.SetActive(false);
            foreach (HintBase button in _hintsButtons)
                button.ButtonGameObject.SetActive(false);
        }
        
        private void ResetHintsAvailability()
        {
            foreach (HintBase button in _hintsButtons)
                button.ResetHintAvailability();
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(SwitchPopup);
            _answerFromLetters.TaskCompleted -= HideHintsButtons;
        }
    }
}