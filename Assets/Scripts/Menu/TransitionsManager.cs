using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cubra
{
    public class TransitionsManager : MonoBehaviour
    {
        public enum Scenes
        {
            // Сцены главного меню
            Menu = 1, Cups = 9, Leaderboard = 10, Shop = 14,

            // Сцены раздела стран и турниров
            CountriesSets = 2, CountriesQuestions = 3, CountriesResult = 4,

            // Сцены раздела по фотографиям игроков
            PlayersSets = 5, PlayersQuestions = 6, PlayersResult = 15,

            // Сцены легендарных карточек
            Legends = 7, Biography = 8,

            // Сцены в разделе подборок фактов
            FactsSets = 11, FactsQuestions = 12, FactsResult = 13
        }

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