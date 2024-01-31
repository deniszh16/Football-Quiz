using UnityEngine;
using UnityEngine.UI;

namespace Logic.GooglePlay
{
    public class UpdateButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Leaderboard _leaderboard;
        
        private void Awake() =>
            _button.onClick.AddListener(UpdateData);

        private void UpdateData() =>
            _leaderboard.PrepareLeaderboard();

        private void OnDestroy() =>
            _button.onClick.RemoveListener(UpdateData);
    }
}