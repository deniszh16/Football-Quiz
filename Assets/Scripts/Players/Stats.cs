using UnityEngine;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Players
{
    public class Stats : MonoBehaviour
    {
        [Header("Количество заданий")]
        [SerializeField] private Tasks _task;
        
        private TextMeshProUGUI _stats;

        private void Awake()
        {
            _stats = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _stats.text = "Всего заданий: - " + _task[0] + IndentsHelpers.LineBreak(2);
            _stats.text += "Успешные задания - " + PlayerPrefs.GetInt("photos-successfully") + IndentsHelpers.LineBreak(2);
            _stats.text += "Количество ошибок - " + PlayerPrefs.GetInt("photos-errors");
        }
    }
}