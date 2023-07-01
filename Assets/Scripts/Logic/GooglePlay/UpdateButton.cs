using UnityEngine;
using UnityEngine.UI;

namespace Logic.GooglePlay
{
    public class UpdateButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Leaderboard _leaderboard;
        [SerializeField] private Button _button;

        private void Awake() =>
            _button.onClick.AddListener(UpdateData);

        private void UpdateData() =>
            _leaderboard.PrepareLeaderboard();

        private void OnDestroy() =>
            _button.onClick.RemoveListener(UpdateData);
    }
}