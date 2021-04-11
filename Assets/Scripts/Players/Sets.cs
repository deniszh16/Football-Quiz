using UnityEngine;
using Cubra.Helpers;

namespace Cubra.Players
{
    public class Sets : MonoBehaviour
    {
        // Номер выбранной категории
        public static int Category;

        [Header("Список подборок")]
        [SerializeField] private Tasks _task;

        // Объект для json по подборкам
        private SetsHelper _setsHelper;

        private void Awake()
        {
            _setsHelper = new SetsHelper();
            _setsHelper = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("photo-quiz"));
        }

        /// <summary>
        /// Открытие категории
        /// </summary>
        /// <param name="number">номер категории</param>
        public void OpenCategory(Category category)
        {
            Category = category.Number;

            if (_setsHelper.arraySets[Category - 1] < _task[Category - 1])
            {
                Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.PlayersQuestions);
            }
            else
            {
                Camera.main.GetComponent<TransitionsManager>().GoToScene((int)TransitionsManager.Scenes.PlayersResult);
            }
        }
    }
}