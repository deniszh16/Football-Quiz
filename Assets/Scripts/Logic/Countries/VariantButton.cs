using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.Countries
{
    public class VariantButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private AnswerFromVariants _answerFromVariants;
        
        [Header("Кнопка варианта")]
        [SerializeField] private Button _button;
        [SerializeField] private int _buttonNumber;

        private void OnEnable() =>
            _button.onClick.AddListener(PushButton);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(PushButton);

        private void PushButton()
        {
            _answerFromVariants.CheckAnswer(_buttonNumber);
            gameObject.SetActive(false);
        }
    }
}