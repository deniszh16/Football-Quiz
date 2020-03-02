using UnityEngine;

public class Review : MonoBehaviour
{
    [Header("Кнопка отзыва")]
    [SerializeField] private GameObject review;

    private void Start()
    {
        // Если игрок немного поиграл (набрал правильные ответы)
        if (PlayerPrefs.GetInt("countries-answer") >= 10)
        {
            // Показываем кнопку отзыва
            review.SetActive(true);
        }
    }
}