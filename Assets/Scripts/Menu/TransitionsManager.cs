using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cubra
{
    public class TransitionsManager : MonoBehaviour
    {
        // Ссылка на страницу игры в Google Play
        private readonly string URL = "https://play.google.com/store/apps/details?id=ru.cubra.football";

        /// <summary>
        /// Переход на указанную сцену
        /// </summary>
        /// <param name="scene">id сцены</param>
        public void GoToScene(int scene)
        {
            SceneManager.LoadScene(scene);
        }

        /// <summary>
        /// Переход на указанную сцену с паузой
        /// </summary>
        /// <param name="seconds">пауза до перехода</param>
        /// <param name="scene">сцена для загрузки</param>
        public IEnumerator GoToSceneWithPause(float seconds, int scene)
        {
            yield return new WaitForSeconds(seconds);
            GoToScene(scene);
        }

        /// <summary>
        /// Перезапуск текущей сцены
        /// </summary>
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Переход на страницу приложения
        /// </summary>
        public void OpenGamePage()
        {
            Application.OpenURL(URL);
        }

        /// <summary>
        /// Выход из игры
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}