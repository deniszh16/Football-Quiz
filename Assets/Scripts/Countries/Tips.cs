using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour
{
    // Отображение панели подсказок
    private bool displayTips = false;

    [Header("Кнопки подсказок")]
    [SerializeField] private Button[] buttonsTips;

    // Стоимость подсказок
    private byte[] pricesTips = { 20, 50, 80 };
    // Статусы кнопок подсказок
    private bool[] availableTips = { true, true, true };

    // Перечисление подсказок
    private enum tipsName { first, letters, pass };

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    [Header("Спрайт первой буквы")]
    [SerializeField] private Sprite spriteLetter;

    [Header("Анимация расширения")]
    [SerializeField] private Animator animatorFrame;

    private Image image;
    private TasksCountries questions;
    private Statistics statistics;

    private void Awake()
    {
        image = GetComponent<Image>();
        questions = Camera.main.GetComponent<TasksCountries>();
        statistics = Camera.main.GetComponent<Statistics>();
        animatorFrame = animatorFrame.GetComponent<Animator>();
    }

    // Проверка подсказок
    private void CheckTips()
    {
        for (int i = 0; i < buttonsTips.Length; i++)
        {
            // Если подсказка использована или нехватает монет
            if (availableTips[i] != true || PlayerPrefs.GetInt("coins") < pricesTips[i])
            {
                // Отключаем кнопку подсказки
                buttonsTips[i].interactable = false;
                // Скрываем текст по стоимости подсказки
                buttonsTips[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    // Открытие / закрытие панели подсказок
    public void ChoiceHints()
    {
        // Переключение переменной отображения
        displayTips = !displayTips;

        // Если панель подсказок открыта, проверяем подсказки
        if (displayTips) CheckTips();

        // Установка прозрачности кнопки в зависимости от состояния панели
        image.color = displayTips ? new Color(255, 255, 255, 0.45f) : Color.white;

        // Активация аниматора панели
        animatorFrame.enabled = true;
        // Переключение на анимацию открытия / закрытия
        animatorFrame.SetBool("Open", displayTips);
    }

    // Скрытие панели подсказок (при нажатии на букву)
    public void CloseTips() { if (displayTips) ChoiceHints(); }

    // Восстановление всех (выбранных) букв
    private void ShowAllLetters()
    {
        // Отображаем все двеннадцать букв
        for (int i = 0; i < 12; i++) { letters.transform.GetChild(i).gameObject.SetActive(true); }
    }

    // Подсказка: выделение первой буквы
    public void FirstLetter()
    {
        // Используем первую подсказку
        UsingHints((int)tipsName.first);

        for (int i = 0; i < letters.transform.childCount; i++)
        {
            // Если буква в массиве совпадает с первой буквой ответа
            if (questions.Tasks.questions[questions.Progress - 1].letters[i] == questions.FirstLetter)
            {
                // Заменяем стандартный спрайт буквы и выходим из цикла
                letters.transform.GetChild(i).GetComponent<Image>().sprite = spriteLetter;
                break;
            }
        }

        // Увеличиваем количество использованных подсказок
        PlayerPrefs.SetInt("countries-tips", PlayerPrefs.GetInt("countries-tips") + 1);
    }

    // Подсказка: удаление всех лишних букв
    public void AllLetters()
    {
        // Используем вторую подсказку
        UsingHints((int)tipsName.letters);

        // Скрываем лишние буквы
        EnumerateLetters();

        // Увеличиваем количество использованных подсказок
        PlayerPrefs.SetInt("countries-tips", PlayerPrefs.GetInt("countries-tips") + 1);
    }

    // Переборка и скрытие лишних букв
    private void EnumerateLetters()
    {
        for (int i = 0; i < letters.transform.childCount; i++)
        {
            // Переборка каждой буквы ответа
            for (int j = 0; j < questions.Tasks.questions[questions.Progress - 1].answer.Length; j++)
            {
                // Если буква на кнопке совпадает с буквой в ответе, выходим из цикла переборки ответа
                if (letters.transform.GetChild(i).GetComponentInChildren<Text>().text == questions.Tasks.questions[questions.Progress - 1].answer[j]) break;
                // Если весь ответ проверен и буква не найдена, скрываем кнопку
                else if (j + 1 == questions.Tasks.questions[questions.Progress - 1].answer.Length) letters.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    // Подсказка: пропуск вопроса
    public void SkipQuestion()
    {
        // Используем третью подсказку
        UsingHints((int)tipsName.pass);

        // Получаем правильный ответ
        questions.Answer.GetRightAnswer();

        // Увеличиваем количество пропусков вопросов
        PlayerPrefs.SetInt("countries-pass", PlayerPrefs.GetInt("countries-pass") + 1);
    }

    // Использование подсказки
    private void UsingHints(int hintNumber)
    {
        // Вычитаем из общего количества монет стоимость подсказки и обновляем статистику
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - pricesTips[hintNumber]);
        statistics.UpdateCoins(true);

        // Сворачиваем панель
        ChoiceHints();

        // Отключаем подсказку
        availableTips[hintNumber] = false;

        // Если выбран не пропуск вопроса, восстанавливаем все буквы
        if (hintNumber < 2) ShowAllLetters();

        // Если использована первая подсказка, а вторая подсказка была ранее открыта
        if (hintNumber == (int)tipsName.first && availableTips[(int)tipsName.letters] != true)
            // Скрываем лишние буквы
            EnumerateLetters();

        // Сбрасываем массив с ответом пользователя
        questions.Answer.ResetLetters(true);
    }
}