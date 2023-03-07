using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Services.SceneLoader
{
    public class SceneLoaderService : MonoBehaviour, ISceneLoaderService
    {
        public void Load(string sceneName, float delay) =>
            StartCoroutine(LoadScene(sceneName, delay));

        private IEnumerator LoadScene(string sceneName, float delay)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
                yield break;

            yield return new WaitForSeconds(delay);
            
            AsyncOperation waitScene = SceneManager.LoadSceneAsync(sceneName);
            if (waitScene.isDone == false)
                yield return null;
        }
    }
}