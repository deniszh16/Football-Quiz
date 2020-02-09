using UnityEngine;

public class TasksPlayers : MonoBehaviour
{
    [Header("Режимы игры")]
    [SerializeField] private GameObject[] modes;

    [Header("Кнопка подсказки")]
    [SerializeField] private GameObject hint;

    private void Awake()
    {
        for (int i = 0; i < modes.Length; i++)
        {
            // Если название объекта совпадает с выбранной категорией
            if (modes[i].name.ToLower() == Photos.category)
            {
                // Активируем панель задания
                modes[i].SetActive(true);
                break;
            }
        }

        CheckHint();
    }

    /// <summary>
    /// Проверка доступности подсказки
    /// </summary>
    public void CheckHint()
    {
        // Если достаточно монет, отображаем кнопку подсказки
        if (PlayerPrefs.GetInt("coins") >= 50) hint.SetActive(true);
    }
}