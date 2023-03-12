using UnityEngine;

namespace Code.Logic.UI
{
    public class AspectRatio : MonoBehaviour
    {
        public static float Ratio;

        [SerializeField] private Camera _camera;

        private void Awake() =>
            Ratio = _camera.aspect;
    }
}