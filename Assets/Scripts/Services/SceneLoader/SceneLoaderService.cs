using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoaderService : MonoBehaviour, ISceneLoaderService
    {
        public void Load(string sceneName) =>
            LoadScene(sceneName);

        public void Load(string sceneName, float delay) =>
            StartCoroutine(LoadScene(sceneName, delay));

        private void LoadScene(string sceneName) =>
            SceneManager.LoadScene(sceneName);

        private IEnumerator LoadScene(string sceneName, float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName);
        }
    }
}