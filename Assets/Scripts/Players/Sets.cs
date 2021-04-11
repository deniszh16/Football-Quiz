using UnityEngine;
using Cubra.Helpers;

namespace Cubra.Players
{
    public class Sets : MonoBehaviour
    {
        // ����� ��������� ���������
        public static int Category;

        [Header("������ ��������")]
        [SerializeField] private Tasks _task;

        // ������ ��� json �� ���������
        private SetsHelper _setsHelper;

        private void Awake()
        {
            _setsHelper = new SetsHelper();
            _setsHelper = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("photo-quiz"));
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        /// <param name="number">����� ���������</param>
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