using UnityEngine;
using UnityEngine.UI;

public class Legend : MonoBehaviour
{
    [Header("Номер кнопки")]
    [SerializeField] private int number;

    [Header("Достижения игрока")]
    [SerializeField] private bool cups = false;

    [Header("Открытая карточка")]
    [SerializeField] private Sprite sprite;

    [Header("Идентификатор достижения")]
    [SerializeField] private string identifier;

    private Image image;
    private Legends legends;

    private void Awake()
    {
        image = GetComponent<Image>();
        legends = Camera.main.GetComponent<Legends>();
    }

    private void Start()
    {
        // Если карточка получена
        if (legends.Statuses.status[number - 1] == "yes")
        {
            // Устанавливаем открытый спрайт
            ShowImageCard();

            // Если доступен интернет, разблокируем достижение с указанным идентификатором
            if (Application.internetReachability != NetworkReachability.NotReachable) PlayServices.UnlockingAchievement(identifier);
        }
    }

    // Отображение открытой карточки
    private void ShowImageCard() { image.sprite = sprite; }

    // Открытие новой карточки
    public void OpenCard()
    {
        // Если карточка закрытая
        if (legends.Statuses.status[number - 1] == "no")
        {
            // Если достаточно монет для открытия
            if (PlayerPrefs.GetInt("coins") >= 950)
            {
                // Вычитаем монеты, увеличиваем общий счет и обновляем статистику
                PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 950);
                PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 450);
                legends.Statistics.UpdateCoins();
                legends.Statistics.UpdateScore();

                // Записываем обновленное значение
                legends.Statuses.status[number - 1] = "yes";
                // Сохраняем данные по карточкам
                legends.SaveCards();

                // Увеличиваем общее количество открытых карточек
                PlayerPrefs.SetInt("legends-open", PlayerPrefs.GetInt("legends-open") + 1);

                // Отображаем карточку
                ShowImageCard();
                // Отображаем эффект открытия
                ShowEffect();

                // Если доступен интернет, разблокируем достижение
                if (Application.internetReachability != NetworkReachability.NotReachable) PlayServices.UnlockingAchievement(identifier);
            }
            // Если монет недостаточно, вызываем мигание монет
            else legends.Statistics.UpdateCoins(true);
        }
        else if (cups)
        {
            // Записываем последнюю позицию скролла
            Legends.scrollPosition = legends.Scroll.verticalNormalizedPosition;
            // Записываем номер карточки
            Legends.descriptionCard = number;
            // Переходим на сцену описания легенды
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(9);
        }
    }

    // Отображение эффекта открытия
    private void ShowEffect()
    {
        // Переставляем эффект к карточке
        legends.Effect.transform.position = gameObject.transform.position;
        // Перемещаем эффект в родительский объект карточки
        legends.Effect.transform.SetParent(gameObject.transform.parent);
        // Делаем эффект нулевым в иерархии
        legends.Effect.transform.SetSiblingIndex(0);
        // Перезапускаем анимацию
        legends.Effect.Rebind();
    }
}