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
    private int currentQuestion = 0;

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
        // Получаем номер текущего вопроса по данной категории
        currentQuestion = categories.Sets.ArraySets[number];

        // Если категория открыта
        if (currentQuestion != 0)
            // Обновляем информацию
            UpdateCategory();

        // Если категория пройдена
        if (currentQuestion > categories.Tasks[number])
            // Если доступен интернет
            if (Application.internetReachability != NetworkReachability.NotReachable)
                // Разблокируем достижение с указанным идентификатором
                PlayServices.UnlockingAchievement(identifier);
    }

    /// <summary>Обновление информации по открытой категории</summary>
    private void UpdateCategory()
    {
        // Устанавливаем открытый спрайт
        image.sprite = sprite;

        // Убираем прозрачность с текста статистики
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
            // Затем переходим на сцену заданий
            categories.Transitions.GoToScene(3);
        }
        // Если категория полностью пройдена
        else if (currentQuestion > categories.Tasks[number])
        {
            // Записываем номер категории
            Categories.category = number;
            // Затем переходим на сцену ответов
            categories.Transitions.GoToScene(4);
        }
        else
        {
            // Если достаточно монет
            if (PlayerPrefs.GetInt("coins") >= price)
            {
                // Открываем новую категорию
                PaymentCategory();
            }
            else
            {
                // Иначе активируем анимацию нехватки монет
                categories.TextAnimator.enabled = true;
                // Перезапускаем проигрывание
                categories.TextAnimator.Rebind();
            }
        }
    }

    /// <summary>Открытие новой категории</summary>
    private void PaymentCategory()
    {
        // Вычитаем стоимость категории
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - price);
        // Обновляем статистику по монетам
        categories.Statistics.UpdateCoins();

        // Открываем категорию
        categories.Sets.ArraySets[number] = 1;
        // Увеличиваем текущий вопрос
        currentQuestion++;
        // Сохраняем обновленное значение
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(categories.Sets));

        // Обновляем информацию по категории
        UpdateCategory();

        // Перемещаем эффект открытия к категории
        categories.Effect.transform.position = transform.position;
        // Перезапускаем анимацию эффекта
        categories.Effect.Rebind();
    }
}