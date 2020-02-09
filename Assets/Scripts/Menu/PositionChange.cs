using UnityEngine;

public class PositionChange : MonoBehaviour
{
    [Header("Позиция на узких экранах")]
    [SerializeField] private Vector2 position;

    // Ссылка на позицию объекта
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        // Если соотношение сторон экрана меньше указанного значения (узкий экран)
        if (AspectRatio._camera.aspect <= 0.5f)
            // Изменяем позицию объекта
            rectTransform.anchoredPosition = position;
    }
}