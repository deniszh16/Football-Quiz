using UnityEngine;

public class Categories : MonoBehaviour
{
    // Номер выбранной категории
    public static int category;

    [Header("Количество вопросов")]
    [SerializeField] private Tasks tasks;
    public Tasks Tasks { get { return tasks; } }

    [Header("Эффект открытия")]
    [SerializeField] private Animator effect;
    public Animator Effect { get { return effect; } }

    [Header("Анимация текста описания")]
    [SerializeField] private Animator text;
    public Animator TextAnimator { get { return text; } }

    // Объект для работы с json по прогрессу категорий
    public SetJson Sets { get; private set; } = new SetJson();

    // Ссылка на используемые компоненты
    public Statistics Statistics { get; private set; }
    public TransitionsInMenu Transitions { get; private set; }

    private void Awake()
    {
        // Преобразуем сохраненную json строку по категориям в объект
        Sets = JsonUtility.FromJson<SetJson>(PlayerPrefs.GetString("sets"));

        Statistics = Camera.main.GetComponent<Statistics>();
        Transitions = Camera.main.GetComponent<TransitionsInMenu>();
    }
}