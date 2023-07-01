using UnityEngine;

namespace Logic.Players
{
    public class ArrangementOfVariants : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        [SerializeField] private VariantButton[] _variantButtons;

        public void ArrangeVariants()
        {
            for (int i = 0; i < _variantButtons.Length; i++)
            {
                _variantButtons[i].gameObject.SetActive(true);
                
                Sprite variant = _tasks.PlayersStaticData.Questions[_tasks.CurrentQuestion].Variants[i].Variant;
                bool correctVariant = _tasks.PlayersStaticData.Questions[_tasks.CurrentQuestion].Variants[i].CorrectAnswer;
                
                _variantButtons[i].CustomizeButton(variant, correctVariant);
                _variantButtons[i].ToggleButton(state: true);
            }
        }

        public void HideWrongVariants()
        {
            foreach (VariantButton button in _variantButtons)
            {
                button.gameObject.SetActive(button.CorrectVariant);
                button.ShowFrame();
            }
        }
    }
}