using UnityEngine;
using UnityEngine.UI;

public class FactsCollections : IncreaseListStatuses
{
    // Номер подборки фактов
    public static int collections;

    [Header("Количество заданий")]
    [SerializeField] private Tasks facts;

    [Header("Набор подборок")]
    [SerializeField] private GameObject sets;

    [Header("Спрайты результата")]
    [SerializeField] private Sprite[] sprites;

    // Объект для json по наборам фактов
    private StaJson statuses = new StaJson();

    private void Awake()
    {
        // Преобразовываем сохраненную json строку в объект
        statuses = JsonUtility.FromJson<StaJson>(PlayerPrefs.GetString("facts"));
    }

    private void Start()
    {
        // Проверяем необходимость увеличения количества подборок
        AddToList(statuses, sets.transform.childCount, "facts");

        // Проверяем доступность подборок
        CheckCollectionsFacts();
    }

    /// <summary>Проверка доступности подборок фактов</summary>
    private void CheckCollectionsFacts()
    {
        for (int i = 0; i < sets.transform.childCount; i++)
        {
            // Если подборка пройдена
            if (statuses.Status[i] != "no")
            {
                // Отключаем текст с количеством вопросов
                sets.transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);

                // Получаем графический компонент у изображения результата
                var image = sets.transform.GetChild(i).transform.GetChild(1).GetComponent<Image>();
                // Убираем прозрачность
                image.color = Color.white;
                // Устанавливаем изображение в зависимости от результата
                image.sprite = sprites[(statuses.Status[i] == "victory") ? 0 : 1];
            }
            else
            {
                // Выводим количество заданий в категории
                sets.transform.GetChild(i).GetComponentInChildren<Text>().text = "0 / " + facts[i].ToString();
            }
        }
    }

    /// <summary>Открытие подборки фактов (номер карточки)</summary>
    public void OpenCollectionFacts(int number)
    {
        // Записываем номер подборки
        collections = number;

        // Если подборка не пройдена
        if (statuses.Status[number] == "no")
            // Переходим к викторине
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(13);
        else
            // Иначе переходим к результатам
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(14);
    }
}