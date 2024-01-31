using UnityEngine;
using TMPro;

namespace Logic.Countries
{
    public class ArrangementOfVariants : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        [SerializeField] private AnswerFromVariants _answerFromVariants;

        [Header("Варианты для задания")]
        [SerializeField] private TextMeshProUGUI[] _variants;

        private void Awake() =>
            _answerFromVariants.TaskCompleted += HideAllVariants;

        public void ArrangeVariants()
        {
            for (int i = 0; i < _variants.Length; i++)
            {
                _variants[i].transform.parent.gameObject.SetActive(true);
                _variants[i].text = _tasks.CountriesStaticData.Questions[_tasks.CurrentQuestion].Variants[i];
            }
        }

        private void HideAllVariants() =>
            gameObject.SetActive(false);

        private void OnDestroy() =>
            _answerFromVariants.TaskCompleted -= HideAllVariants;
    }
}