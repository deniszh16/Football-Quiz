using UnityEngine;

namespace Cubra.Countries
{
    public class DeletingLetters : MonoBehaviour
    {
        [Header("Панель букв")]
        [SerializeField] private GameObject _letters;

        /// <summary>
        /// Восстановление ранее выбранной буквы
        /// </summary>
        /// <param name="number">номер буквы</param>
        public void RecoverLetter(int number)
        {
            // Отображаем скрытую кнопку буквы
            _letters.transform.GetChild(number).gameObject.SetActive(true);
        }
    }
}