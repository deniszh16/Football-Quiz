using UnityEngine;

public class TasksPlayers : MonoBehaviour
{
    [Header("Режимы игры")]
    [SerializeField] private GameObject[] photos;

    [Header("Кнопка подсказки")]
    [SerializeField] private GameObject tips;

    [Header("Набор плиток")]
    [SerializeField] private GameObject buttons;

    private void Awake()
    {
        for (int i = 0; i < photos.Length; i++)
        {
            // Если название объекта совпадает с выбранной категорией
            if (photos[i].name.ToLower() == Modes.category)
            {
                // Активируем объект и выходим из цикла
                photos[i].SetActive(true);
                break;
            }
        }

        CheckHint();
    }
    
    public void CheckHint()
    {
        // Если достаточно монет, отображаем кнопку подсказки
        if (PlayerPrefs.GetInt("coins") >= 50) tips.SetActive(true);
    }

    // Отображение/скрытие плиток
    public void UpdateButtons(bool state)
    {
        for (int i = 0; i < buttons.transform.childCount; i++)
        {
            // Устанавливаем выбранное состояние плитки
            buttons.transform.GetChild(i).GetComponent<Animator>().enabled = state;
            // Если плитка установлена в закрытом состоянии, сбрасываем её анимацию
            if (!state) buttons.transform.GetChild(i).GetComponent<Animator>().Rebind();
        }
    }
}