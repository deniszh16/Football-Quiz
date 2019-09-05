using UnityEngine;
using UnityEngine.UI;

public class LegendDescription : JsonFileProcessing
{
    [Header("Заголовок")]
    [SerializeField] private Text heading;

    [Header("Биография легенды")]
    [SerializeField] private Text legend;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;

    // Объект для работы с json по описанию игрока
    private DescriptionJson description = new DescriptionJson();

    protected override void Awake()
    {
        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile("legend-" + Legends.descriptionCard);
        // Преобразовываем строку в объект
        ConvertToObject(jsonString);
    }

    // Преобразование json строки в объект
    private void ConvertToObject(string json) { description = JsonUtility.FromJson<DescriptionJson>(json); }

    protected override void Start()
    {
        // Устанавливаем в заголовок имя игрока
        heading.text = description.name;

        // Выводим клубные достижения
        legend.text += description.progress.club;

        // Если есть международные достижения
        if (description.progress.team != "")
        {
            // Создаем отделяющую черту
            legend.text += Indents.LineBreak(2) + Indents.Underscore(16) + Indents.LineBreak(2);
            // Выводим международные достижения
            legend.text += description.progress.team;
        }

        // Если есть особые достижения, выводим их в текстовое поле
        if (description.progress.personal != "")
        {
            // Создаем отделяющую черту
            legend.text += Indents.LineBreak(2) + Indents.Underscore(16) + Indents.LineBreak(2);
            // Выводим особые достижения
            legend.text += description.progress.personal;
            // И добавляем пару отступов
            legend.text += Indents.LineBreak(2);
        }

        // Перемещаем скролл вверх текста
        scroll.verticalNormalizedPosition = 1;
    }
}