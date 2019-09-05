using UnityEngine;

public class Categories : MonoBehaviour
{
    // Номер выбранной категории
    public static int category;

    [Header("Количество вопросов")]
    [SerializeField] private NumberTasks tasks;
    public NumberTasks Tasks { get { return tasks; } }

    [Header("Анимация эффекта")]
    [SerializeField] private Animator effect;
    public Animator Effect { get { return effect; } }

    [Header("Анимация текста")]
    [SerializeField] private Animator animator;
    public Animator Animator { get { return animator; } }

    public Statistics Statistics { get; private set; }
    public SetsJson Sets { get; private set; } = new SetsJson();
    public TransitionsInMenu Transitions { get; private set; }

    private void Awake()
    {
        // Преобразование сохраненной json строки по наборам в объект
        Sets = JsonUtility.FromJson<SetsJson>(PlayerPrefs.GetString("sets"));

        effect = effect.gameObject.GetComponent<Animator>();
        animator = animator.gameObject.GetComponent<Animator>();
        Statistics = Camera.main.GetComponent<Statistics>();
        Transitions = Camera.main.GetComponent<TransitionsInMenu>();
    }
}