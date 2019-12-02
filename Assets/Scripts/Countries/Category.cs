using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    [Header("Номер категории")]
    [SerializeField] private int number;

    [Header("Стоимость открытия")]
    [SerializeField] private int price;

    [Header("Спрайт открытой категории")]
    [SerializeField] private Sprite sprite;

    [Header("Идентификатор достижения")]
    [SerializeField] private string identifier;

    // Номер текущего вопроса
    private int currentQuestion;

    // Ссылки на компоненты
    private Text text;
    private Image image;
    private Categories categories;

    private void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
        categories = Camera.main.GetComponent<Categories>();
    }

    private void Start()
    {
        // Получаем номер текущего вопроса по категории
        currentQuestion = categories.Sets.ArraySets[number];

        // Если категория открыта, обновляем информацию
        if (currentQuestion != 0) UpdateCategory();

        // Если категория полностью пройдена
        if (currentQuestion > categories.Tasks[number])
            if (Application.internetReachability != NetworkReachability.NotReachable)
                // Разблокируем достижение с указанным идентификатором
                PlayServices.UnlockingAchievement(identifier);
    }

    /// <summary>Обновление информации по открытой категории</summary>
    private void UpdateCategory()
    {
        // Меняем спрайт
        image.sprite = sprite;

        // Убираем прозрачность текста
        text.color = Color.white;
        // Выводим количество пройденных и общее количество вопросов
        text.text = currentQuestion - 1 + " /" + categories.Tasks[number];
    }

    /// <summary>Открытие/приобретение категории</summary>
    public void OpenCategory()
    {
        // Если категория открыта и не пройдена полностью
        if (currentQuestion != 0 && currentQuestion <= categories.Tasks[number])
        {
            // Записываем номер категории
            Categories.category = number;
            categories.Transitions.GoToScene(3);
        }
        // Если категория полностью пройдена
        else if (currentQuestion > categories.Tasks[number])
        {
            Categories.category = number;
            categories.Transitions.GoToScene(4);
        }
        else
        {
            if (PlayerPrefs.GetInt("coins") >= price)
            {
                // Покупаем категорию
                PaymentCategory();
            }
            else
            {
                // Активируем анимацию нехватки монет
                categories.TextAnimator.enabled = true;
                categories.TextAnimator.Rebind();
            }
        }
    }

    /// <summary>Покупка новой категории</summary>
    private void PaymentCategory()
    {
        // Вычитаем стоимость категории
        categories.Statistics.ChangeTotalCoins(-price);

        // Открываем категорию
        categories.Sets.ArraySets[number] = 1;
        // Увеличиваем номер текущего вопроса
        currentQuestion++;
        // Сохраняем обновленное значение
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(categories.Sets));

        // Обновляем категорию
        UpdateCategory();

        // Перемещаем эффект открытия к категории и воспроизводим
        categories.Effect.transform.position = transform.position;
        categories.Effect.Rebind();
    }
}