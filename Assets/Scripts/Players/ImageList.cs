using UnityEngine;
using UnityEngine.UI;

public class ImageList : MonoBehaviour
{
    [Header("Фотографии игроков")]
    [SerializeField] private Sprite[] photos;

    [Header("Имена игроков")]
    public string[] names;

    [Header("Фамилии игроков")]
    public string[] lastnames;

    [Header("Команды игроков")]
    public string[] teams;

    private Image image;

    private void Awake() { image = GetComponent<Image>(); }

    private void Start() { ShowImage(); }

    // Установка изображения в соответствии с текущим прогрессом викторины
    public void ShowImage() { image.sprite = photos[PlayerPrefs.GetInt(Modes.category)]; }
}