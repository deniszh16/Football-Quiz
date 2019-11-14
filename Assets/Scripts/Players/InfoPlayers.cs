using UnityEngine;
using UnityEngine.UI;

public class InfoPlayers : FileProcessing
{
    [Header("Фотографии игроков")]
    [SerializeField] private Sprite[] photos;

    // Свойство для получения количества фотографий
    public int QuantityPhotos { get { return photos.Length; } }

    // Объект для работы с json файлом по игрокам
    private PhoJson phoJson = new PhoJson();

    // Свойство для получения информации по игрокам
    public PhoJson PhoJson { get { return phoJson; } }

    // Ссылка на компонент
    private Image image;

    private void Awake()
    {
        // Получаем графический компонент
        image = GetComponent<Image>();

        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile(Photos.category);
        // Преобразовываем строку в объект
        ConvertToObject(ref phoJson, jsonString);
    }

    private void Start()
    {
        ShowTaskImage();
    }

    /// <summary>Установка изображения в соответствии с текущим прогрессом викторины</summary>
    public void ShowTaskImage()
    {
        image.sprite = photos[PlayerPrefs.GetInt(Photos.category)];
    }
}