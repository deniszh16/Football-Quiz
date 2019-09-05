using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonFileProcessing : MonoBehaviour
{
    // Путь до json файла
    private string path;

    protected virtual void Awake() {}

    protected virtual void Start() {}

    // Чтение json файла
    protected string ReadJsonFile(string fileName)
    {
        // Получаем путь до json файла
        path = Path.Combine(Application.streamingAssetsPath, fileName + ".json");

        // Получаем данные по указанному пути
        UnityWebRequest reader = UnityWebRequest.Get(path);
        // Выполняем обработку полученнных данных
        reader.SendWebRequest();
        // Ждем завершения обработки
        while (!reader.isDone) {}

        // Возвращаем прочитанную строку
        return reader.downloadHandler.text;
    }
}