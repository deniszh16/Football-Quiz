using DZGames.Football.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Football.UI
{
    public class SceneOpenButton : MonoBehaviour
    {
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        [SerializeField] private Scenes _scene;

        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _button.onClick.AddListener(GoToScene);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToScene);

        private void GoToScene() =>
            _sceneLoaderService.Load(sceneName: _scene.ToString());

        public void ReplaceScene(Scenes scene) =>
            _scene = scene;
    }
}