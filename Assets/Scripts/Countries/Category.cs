using UnityEngine;
using UnityEngine.UI;

public class Category : MonoBehaviour
{
    [Header("Номер категории")]
    [SerializeField] private int number;

    [Header("Открытая категория")]
    [SerializeField] private Sprite sprite;

    [Header("Стоимость открытия")]
    [SerializeField] private int price;

    [Header("Идентификатор достижения")]
    [SerializeField] private string identifier;

    // Номер текущего вопроса
    private int numberQuestion = 0;

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
        // Получаем номер текущего вопроса из массива
        numberQuestion = categories.Sets.arraySets[number];

        // Если категория открыта, обновляем ее информацию
        if (numberQuestion != 0) UpdateCategory();

        // Если категория пройдена
        if (numberQuestion > categories.Tasks[number])
            // И если доступен интернет, разблокируем достижение
            if (Application.internetReachability != NetworkReachability.NotReachable) PlayServices.UnlockingAchievement(identifier);
    }

    // Обновление категории (для открытых категорий)
    private void UpdateCategory()
    {
        // Устанавливаем открытый спрайт
        image.sprite = sprite;
        // Убираем прозрачность со статистики
        text.color = Color.white;
        // В статистике выводим количество пройденных и общее количество вопросов
        text.text = numberQuestion - 1 + " /" + categories.Tasks[number];
    }

    // Открытие/приобретение категории
    public void OpenCategory()
    {
        // Если категория открыта и не пройдена, переходим в викторину
        if (numberQuestion != 0 && numberQuestion <= categories.Tasks[number]) CustomizeTransition(3);
        // Если категория викторины пройдена, переходим в список вопросов
        else if (numberQuestion > categories.Tasks[number]) CustomizeTransition(4);
        else
        {
            // Если достаточно монет, открываем категорию и сохраняем
            if (PlayerPrefs.GetInt("coins") >= price) PaymentCategory();
            else
            {
                // Иначе активируем анимацию текста ошибки
                categories.Animator.enabled = true;
                // Перезапускаем анимацию
                categories.Animator.Rebind();
            }
        }
    }

    private void CustomizeTransition(int scene)
    {
        // Записываем номер выбранной категории
        Categories.category = number;
        // Затем переходим на указанную сцену
        categories.Transitions.GoToScene(scene);
    }

    // Оплата и открытие категории
    private void PaymentCategory()
    {
        // Вычитаем стоимость категории и обновляем статистику монет
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - price);
        categories.Statistics.UpdateCoins();

        // Открываем категорию
        categories.Sets.arraySets[number] = 1;
        // Увеличиваем номер текущего вопроса
        numberQuestion++;
        // Сохраняем обновленное значение в json строку по наборам
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(categories.Sets));

        // Обновляем категорию
        UpdateCategory();

        // Перемещаем эффект открытия к категории
        categories.Effect.transform.position = transform.position;
        // Перезапускаем анимацию эффекта
        categories.Effect.Rebind();
    }
}