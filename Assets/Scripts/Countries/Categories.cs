using UnityEngine;

public class Categories : MonoBehaviour
{
    // Номер выбранной категории
    public static int category;

    [Header("Количество вопросов")]
    [SerializeField] private Tasks tasks;

    // Свойство для получения количества заданий
    public Tasks Tasks { get { return tasks; } }

    [Header("Анимация эффекта открытия")]
    [SerializeField] private Animator effect;

    // Свойство для получения аниматора эффекта
    public Animator Effect { get { return effect; } }

    [Header("Анимация текста описания")]
    [SerializeField] private Animator text;

    // Свойство для получения аниматора текста
    public Animator TextAnimator { get { return text; } }

    // Ссылка на компонент статистики
    public Statistics Statistics { get; private set; }

    // Объект для работы с json по прогрессу категорий
    public SetJson Sets { get; private set; } = new SetJson();

    // Ссылка на компонент переходов
    public TransitionsInMenu Transitions { get; private set; }

    private void Awake()
    {
        // Преобразуем сохраненную json строку по категориям в объект
        Sets = JsonUtility.FromJson<SetJson>(PlayerPrefs.GetString("sets"));

        // Получаем компоненты
        effect = effect.gameObject.GetComponent<Animator>();
        text = text.gameObject.GetComponent<Animator>();
        Statistics = Camera.main.GetComponent<Statistics>();
        Transitions = Camera.main.GetComponent<TransitionsInMenu>();
    }
}