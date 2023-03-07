using Code.Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Logic.UI
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

        private void GoToScene() =>
            _sceneLoaderService.Load(sceneName: _scene.ToString(), delay: 0f);

        public void ReplaceScene(Scenes scene) =>
            _scene = scene;

        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToScene);
    }
}