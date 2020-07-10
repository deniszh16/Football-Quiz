using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;

namespace Cubra.Players
{
    public class Stats : MonoBehaviour
    {
        [Header("Количество заданий")]
        [SerializeField] private Tasks _task;

        private Text _stats;

        private void Awake()
        {
            _stats = GetComponent<Text>();
        }

        private void Start()
        {
            _stats.text = "Всего заданий: - " + _task[0] + IndentsHelpers.LineBreak(2);
            _stats.text += "Успешные задания - " + PlayerPrefs.GetInt("photos-successfully") + IndentsHelpers.LineBreak(2);
            _stats.text += "Количество ошибок - " + PlayerPrefs.GetInt("photos-errors");
        }
    }
}