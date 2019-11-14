using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    // Отображение панели подсказок
    private bool displayTips = false;

    [Header("Кнопки подсказок")]
    [SerializeField] private Button[] buttonsTips;

    // Перечисление подсказок
    private enum ListTips { First, Letters, Pass }

    // Стоимость и доступность подсказок
    private Dictionary<int, bool> tips = new Dictionary<int, bool> {{ 20, true }, { 50, true }, { 80, true }};

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    [Header("Спрайт первой буквы")]
    [SerializeField] private Sprite highlighted;

    [Header("Анимация увеличения панели")]
    [SerializeField] private Animator panelIncrease;

    // Ссылки на компоненты
    private Image image;
    private TasksCountries questions;
    private Statistics statistics;

    private void Awake()
    {
        image = GetComponent<Image>();
        questions = Camera.main.GetComponent<TasksCountries>();
        statistics = Camera.main.GetComponent<Statistics>();
        panelIncrease = panelIncrease.GetComponent<Animator>();
    }

    /// <summary>Проверка подсказок на доступность</summary>
    private void CheckTips()
    {
        for (int i = 0; i < buttonsTips.Length; i++)
        {
            // Если недостаточно монет или подсказка использована
            if (tips.ElementAt(i).Key > PlayerPrefs.GetInt("coins") || !tips.ElementAt(i).Value)
            {
                // Отключаем кнопку подсказки
                buttonsTips[i].interactable = false;
                // Скрываем текст по стоимости подсказки
                buttonsTips[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    /// <summary>Открытие или закрытие панели подсказок</summary>
    public void ChoiceHints()
    {
        // Переключение переменной отображения
        displayTips = !displayTips;

        // Если панель открыта
        if (displayTips)
        {
            // Проверяем подсказки
            CheckTips();
            // Делаем кнопку открытия подсказок полупрозрачной
            image.color = new Color(255, 255, 255, 0.45f);
        }
        else
        {
            // Убираем прозрачность с кнопки открытия подсказок
            image.color = Color.white;
        }

        // Активируем анимацию панели
        panelIncrease.enabled = true;
        // Запускаем указанную анимацию панели
        panelIncrease.SetBool("Open", displayTips);
    }

    /// <summary>Скрытие панели подсказок при нажатии на букву в задании</summary>
    public void CloseTips()
    {
        if (displayTips) ChoiceHints();
    }

    /// <summary>Восстановление всех выбранных (скрытых) букв</summary>
    private void ShowAllLetters()
    {
        for (int i = 0; i < 12; i++)
        {
            // Находим и активируем кнопку
            letters.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    /// <summary>Подсказка для выделения первой буквы ответа</summary>
    public void FirstLetter()
    {
        // Используем подсказку
        UseTip((int)ListTips.First);

        // Поиск первой буквы в массиве
        var letter = questions.LetterSearch();

        // Если буква найдена, заменяем стандартный спрайт на выделенный
        if (letter > 0) letters.transform.GetChild(letter).GetComponent<Image>().sprite = highlighted;
    }

    /// <summary>Подсказка для удаление всех лишних букв</summary>
    public void AllLetters()
    {
        // Используем подсказку
        UseTip((int)ListTips.Letters);

        // Скрываем лишние буквы
        HideExtraLetters();
    }

    /// <summary>Скрытие букв, не входящих в ответ</summary>
    private void HideExtraLetters()
    {
        for (int i = 0; i < letters.transform.childCount; i++)
        {
            // Поиск указанной буквы в массиве ответа
            var letter = questions.LetterSearch(i);

            // Если буква не найдена, скрываем кнопку с этой буквой
            if (letter < 0) letters.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    /// <summary>Подсказка для пропуска задания</summary>
    public void SkipQuestion()
    {
        // Используем подсказку
        UseTip((int)ListTips.Pass);

        // Получаем правильный ответ
        questions.Answer.GetRightAnswer();
    }

    /// <summary>Действия при использовании подсказки (номер подсказки в массиве)</summary>
    private void UseTip(int number)
    {
        // Получаем стоимость подсказки
        var cost = tips.ElementAt(number).Key;

        // Вычитаем из общего количества монет стоимость подсказки
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - cost);
        // Обновляем статистику с анимацией мигания
        statistics.UpdateCoins(true);

        // Сворачиваем панель подсказок
        ChoiceHints();

        // Отключаем подсказку
        tips[cost] = false;

        // Если выбрана подсказка
        if (number < 2)
        {
            // Восстанавливаем буквы
            ShowAllLetters();

            // Если ранее уже были скрыты лишние буквы
            if (number == 0 && !tips.ElementAt(1).Value)
                // Повторяем скрытие
                HideExtraLetters();

            // Увеличиваем количество использованных подсказок
            PlayerPrefs.SetInt("countries-tips", PlayerPrefs.GetInt("countries-tips") + 1);
        }
        else
        {
            // Если выбран пропуск вопроса, увеличиваем количество пропусков
            PlayerPrefs.SetInt("countries-pass", PlayerPrefs.GetInt("countries-pass") + 1);
        }

        // Сбрасываем массив с ответом пользователя
        questions.Answer.ResetLetters(true);
    }
}