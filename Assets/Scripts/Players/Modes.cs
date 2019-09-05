using UnityEngine;

public class Modes : MonoBehaviour
{
    [Header("Количество вопросов")]
    [SerializeField] private NumberTasks tasks;

    [Header("Кнопки для перехода")]
    [SerializeField] private GameObject buttons;

    // Название категории викторины
    public static string category;

    private void Start()
    {
        // Если доступен интернет
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Количество заданий
            int quantity = 0;

            // Подсчитываем общее количество заданий
            for (int i = 0; i < tasks.quantityLength; i++) { quantity += tasks[i]; }

            // Если сохраненный прогресс больше общего количества заданий, открываем достижение
            if (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers") >= quantity)
                PlayServices.UnlockingAchievement(GPGSIds.achievement_17);
        }
    }

    // Переход к викторине или результатам
    public void GoToQuiz(int number)
    {
        // Записываем название категории
        category = buttons.transform.GetChild(number).name.ToLower();

        // Если викторина не пройдена, переходим к игре (иначе к результатам)
        int scene = (PlayerPrefs.GetInt(category.ToLower()) < tasks[number]) ? 6 : 7;
        Camera.main.GetComponent<TransitionsInMenu>().GoToScene(scene);
    }
}