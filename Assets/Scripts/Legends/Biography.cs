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
        // Устанавливаем в заголовок имя игрока
        heading.text = biography.Name;

        // Выводим клубные достижения
        legend.text += biography.Progress.Club;

        // Если есть международные достижения
        if (biography.Progress.Team != "")
        {
            // Создаем отделяющую черту
            legend.text += Indents.LineBreak(2) + Indents.Underscore(16) + Indents.LineBreak(2);
            // Выводим международные достижения
            legend.text += biography.Progress.Team;
        }

        // Если есть особые достижения, выводим их в текстовое поле
        if (biography.Progress.Personal != "")
        {
            // Создаем отделяющую черту
            legend.text += Indents.LineBreak(2) + Indents.Underscore(16) + Indents.LineBreak(2);
            // Выводим особые достижения
            legend.text += biography.Progress.Personal;
            // Добавляем пару отступов
            legend.text += Indents.LineBreak(2);
        }

        // Перемещаем скролл вверх текста
        scroll.verticalNormalizedPosition = 1;
    }
}