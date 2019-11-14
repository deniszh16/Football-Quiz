using UnityEngine;
using UnityEngine.UI;

public class Legend : MonoBehaviour
{
    [Header("Открытая карточка")]
    [SerializeField] private Sprite sprite;

    [Header("Идентификатор достижения")]
    [SerializeField] private string identifier;

    // Ссылка на компонент
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>Отображение открытой карточки</summary>
    public void ShowImageCard()
    {
        // Устанавливаем открытый спрайт
        image.sprite = sprite;

        // Если доступен интернет
        if (Application.internetReachability != NetworkReachability.NotReachable)
            // Разблокируем достижение с указанным идентификатором
            PlayServices.UnlockingAchievement(identifier);
    }
}