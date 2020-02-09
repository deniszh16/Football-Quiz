using UnityEngine;

public class ButtonsBall : MonoBehaviour
{
    // Ссылка на статистику
    private Statistics statistics;

    private void Awake()
    {
        statistics = Camera.main.GetComponent<Statistics>();
    }

    private void Start()
    {
        RemoveRandomButtons(2);
    }

    /// <summary>
    /// Открытие случайных плиток
    /// </summary>
    /// <param name="quantity">Количество плиток</param>
    public void RemoveRandomButtons(int quantity)
    {
        for (int i = 0; i < quantity; i++)
            // Скрываем случайную плитку
            transform.GetChild(Random.Range(0, 16)).GetComponent<Animator>().enabled = true;
    }

    /// <summary>
    /// Отображение или скрытие плиток
    /// </summary>
    /// <param name="state">Состояние кнопок</param>
    public void UpdateButtons(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // Ссылка на аниматор дочернего объекта
            var animator = transform.GetChild(i).GetComponent<Animator>();

            // Устанавливаем состояние плитки
            animator.enabled = state;
            // Если плитка установлена в закрытое состояние, сбрасываем анимацию
            if (!state) animator.Rebind();
        }
    }

    /// <summary>
    /// Открытие дополнительного фрагмента
    /// </summary>
    /// <param name="button">Кнопка</param>
    public void OpenFragment(GameObject button)
    {
        // Если достаточно монет для открытия
        if (PlayerPrefs.GetInt("coins") >= 35)
        {
            // Получаем у кнопки аниматор и активируем анимацию
            button.GetComponent<Animator>().enabled = true;

            // Вычитаем стоимость открытия плитки
            statistics.ChangeTotalCoins(-35);

            // Обновляем общее количество открытых фрагментов
            PlayerPrefs.SetInt(Photos.category + "-buttons", PlayerPrefs.GetInt(Photos.category + "-buttons") + 1);
        }
        else
        {
            // Иначе уведомляем о нехватке монет
            statistics.UpdateTotalCoins(true);
        }    
    }
}