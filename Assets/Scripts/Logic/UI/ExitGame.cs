using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.UI
{
    public class ExitGame : MonoBehaviour
    {
        [Header("Кнопка выхода")]
        [SerializeField] private Button _exitButton;

        private void Awake() =>
            _exitButton.onClick.AddListener(QuitGame);

        private void OnDestroy() =>
            _exitButton.onClick.RemoveListener(QuitGame);

        private void QuitGame() =>
            Application.Quit();
    }
}