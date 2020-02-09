using UnityEngine;

public class Photos : MonoBehaviour
{
    [Header("Количество вопросов")]
    [SerializeField] private Tasks tasks;

    [Header("Список кнопок")]
    [SerializeField] private GameObject buttons;

    // Название категории
    public static string category;

    private void Start()
    {
        // Если доступен интернет
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Количество заданий
            var quantity = 0;
            // Подсчитываем общее количество заданий
            for (int i = 0; i < tasks.quantityLength; i++) quantity += tasks[i];

            // Если сохраненный прогресс больше общего количества заданий
            if (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers") >= quantity)
                // Открываем достижение
                PlayServices.UnlockingAchievement(GPGSIds.achievement_17);
        }
    }

    /// <summary>
    /// Переход в категорию или к результатам
    /// </summary>
    /// <param name="number">Номер кнопки</param>
    public void OpenСategory(int number)
    {
        // Записываем название категории
        category = buttons.transform.GetChild(number).name.ToLower();

        // Если прогресс не превышает количество вопросов, переходим в категорию (иначе к результатам)
        Camera.main.GetComponent<TransitionsInMenu>().GoToScene((PlayerPrefs.GetInt(category) < tasks[number]) ? 6 : 7);
    }
}