using UnityEngine;

namespace Cubra
{
    public class ReturnBack : MonoBehaviour
    {
        [Header("Сцена для возврата")]
        [SerializeField] private int _previousScene;

        private TransitionsManager _transitionsManager;

        private void Awake()
        {
            _transitionsManager = Camera.main.GetComponent<TransitionsManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _transitionsManager.GoToScene(_previousScene);
            }
        }
    }
}