using UnityEngine;

namespace Cubra
{
    public class AspectRatio : MonoBehaviour
    {
        // Соотношение сторон экрана
        public static float Ratio { get; private set; }

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
        }

        private void Start()
        {
            Ratio = _mainCamera.aspect;
        }
    }
}