using UnityEngine;
using UnityEngine.UI;

public class Legends : IncreaseListStatuses
{
    // Номер выбранной карточки
    public static int descriptionCard;

    // Позиция скролла в списке карточек
    public static float scrollPosition = 1;

    [Header("Набор карточек")]
    [SerializeField] private GameObject cards;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;

    [Header("Анимация открытия")]
    [SerializeField] private Animator effect;

    // Объект для json по статусам карточек
    private StaJson statuses = new StaJson();

    // Ссылка на компонент статистики
    private Statistics statistics;

    private void Awake()
    {
        // Преобразовываем сохраненную json строку в объект
        statuses = JsonUtility.FromJson<StaJson>(PlayerPrefs.GetString("legends"));

        statistics = Camera.main.GetComponent<Statistics>();
    }

    private void Start()
    {
        // Проверяем необходимость увеличения списка карточек
        AddToList(statuses, cards.transform.childCount, "legends");

        // Устанавливаем позицию скролла
        scroll.verticalNormalizedPosition = scrollPosition;

        // Проверяем легендарные карточки
        CheckLegendaryCards();
    }

    /// <summary>Проверка легендарных карточек</summary>
    private void CheckLegendaryCards()
    {
        for (int i = 0; i < cards.transform.childCount; i++)
        {
            // Если карточка открыта
            if (statuses.status[i] == "yes")
                // Отображаем открытый вариант
                cards.transform.GetChild(i).GetComponentInChildren<Legend>().ShowImageCard();
        }
    }

    /// <summary>Открытие легендарной карточки (номер карточки)</summary>
    public void OpenLegendaryCard(int number)
    {
        // Если карточка закрытая
        if (statuses.status[number] == "no")
        {
            // Если достаточно монет для открытия
            if (PlayerPrefs.GetInt("coins") >= 950)
            {
                // Вычитаем монеты, увеличиваем счет
                statistics.ChangeTotalCoins(-950);
                statistics.ChangeTotalScore(450);

                // Записываем обновленное значение
                statuses.status[number] = "yes";
                // Сохраняем данные по карточкам
                SaveListStatuses(statuses, "legends");

                // Увеличиваем общее количество открытых карточек
                PlayerPrefs.SetInt("legends-open", PlayerPrefs.GetInt("legends-open") + 1);

                // Отображаем открытую карточку
                cards.transform.GetChild(number).GetComponentInChildren<Legend>().ShowImageCard();
                // Отображаем эффект открытия под карточкой
                ShowOpeningEffect(cards.transform.GetChild(number).gameObject);
            }
            else
            {
                // Вызываем мигание монет
                statistics.UpdateTotalCoins(true);
            }
        }
        else
        {
            // Записываем последнюю позицию скролла
            scrollPosition = scroll.verticalNormalizedPosition;

            // Записываем номер карточки
            descriptionCard = number;

            // Переходим на сцену описания легенды
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(9);
        }
    }

    /// <summary>Отображение эффекта открытия (открытая карточка)</summary>
    private void ShowOpeningEffect(GameObject card)
    {
        // Переставляем эффект к карточке
        effect.transform.position = card.transform.position;
        effect.transform.SetParent(card.transform);
        effect.transform.SetSiblingIndex(0);

        // Перезапускаем анимацию
        effect.Rebind();
    }
}