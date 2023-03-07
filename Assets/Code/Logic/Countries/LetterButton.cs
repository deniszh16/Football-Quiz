using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Code.Logic.Countries
{
    public class LetterButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Hints _hints;
        [SerializeField] private AnswerFromLetters _answerFromLetters;

        [Header("Кнопка с буквой")]
        [SerializeField] private Button _button;
        [SerializeField] private int _buttonNumber;
        [SerializeField] private TextMeshProUGUI _buttonText;

        private void OnEnable() =>
            _button.onClick.AddListener(PushButton);

        private void PushButton()
        {
            if (_hints.IsPopupClosed == false)
                _hints.SwitchPopup();
            
            bool isLetterAdded = _answerFromLetters.GetPressedButton(_buttonText.text, _buttonNumber);
            if (isLetterAdded)
                gameObject.SetActive(false);
            
            _answerFromLetters.CheckAnswer(isSkipped: false);
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(PushButton);
    }
}