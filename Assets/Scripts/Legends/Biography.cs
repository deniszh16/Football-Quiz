using UnityEngine;
using UnityEngine.UI;

public class Biography : FileProcessing
{
    [Header("Заголовок")]
    [SerializeField] private Text heading;

    [Header("Биография легенды")]
    [SerializeField] private Text legend;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;

    // Объект для работы с json по описанию игрока
    private LegJson biography = new LegJson();

    private void Awake()
    {
        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile("legend-" + Legends.descriptionCard);
        // Преобразовываем строку в объект
        ConvertToObject(ref biography, jsonString);
    }

    private void Start()
    {
        // Выводим в заголовок имя игрока
        heading.text = biography.Name;

        // Выводим клубные достижения
        legend.text += biography.Progress.Club;

        // Если есть международные достижения
        if (biography.Progress.Team != "")
        {
            legend.text += Indents.LineBreak(2) + Indents.Underscore(26) + Indents.LineBreak(2);
            // Выводим международные достижения
            legend.text += biography.Progress.Team;
        }

        // Если есть особые достижения
        if (biography.Progress.Personal != "")
        {
            legend.text += Indents.LineBreak(2) + Indents.Underscore(26) + Indents.LineBreak(2);
            // Выводим особые достижения
            legend.text += biography.Progress.Personal;
            legend.text += Indents.LineBreak(2);
        }

        // Перемещаем скролл вверх текста
        scroll.verticalNormalizedPosition = 1;
    }
}