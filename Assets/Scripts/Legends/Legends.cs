using UnityEngine;
using UnityEngine.UI;

public class Legends : MonoBehaviour
{
    // Номер выбранной карточки
    public static int descriptionCard;
    // Позиция скролла в списке карточек
    public static float scrollPosition = 1;

    [Header("Набор карточек")]
    [SerializeField] private GameObject cards;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;
    public ScrollRect Scroll { get { return scroll; } }

    [Header("Анимация эффекта")]
    [SerializeField] private Animator effect;
    public Animator Effect { get { return effect; } }

    public Statistics Statistics { get; private set; }

    // Объект для json по статусам карточек
    public StatusJson Statuses { get; private set; } = new StatusJson();

    private void Awake()
    {
        // Преобразовываем сохраненную json строку в объект
        Statuses = JsonUtility.FromJson<StatusJson>(PlayerPrefs.GetString("legends"));

        effect = effect.GetComponent<Animator>();
        Statistics = Camera.main.GetComponent<Statistics>();

        // Если карточек больше, чем размер сохраненного списка
        if (Statuses.status.Count < cards.transform.childCount)
        {
            // Подсчитываем разницу
            int difference = cards.transform.childCount - Statuses.status.Count;
            // Циклом добавляем недостающие закрытые элементы
            for (int i = 0; i < difference; i++) { Statuses.status.Add("no"); }
            // Сохраняем карточки
            SaveCards();
        }
    }

    private void Start()
    {
        // Устанавливаем позицию скролла
        scroll.verticalNormalizedPosition = scrollPosition;
    }

    // Сохранение обновленного списка в json строку
    public void SaveCards() { PlayerPrefs.SetString("legends", JsonUtility.ToJson(Statuses)); }
}