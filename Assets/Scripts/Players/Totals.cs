using UnityEngine;
using UnityEngine.UI;

public class Totals : MonoBehaviour
{
    // Ссылка на компонент
    private Text totals;

    private void Awake()
    {
        totals = GetComponent<Text>();
    }

    private void Start()
    {
        // Выводим общее количество заданий
        totals.text += Indents.LineBreak(1) + "Всего вопросов: " + PlayerPrefs.GetInt(Photos.category) + Indents.LineBreak(2);

        // Выводим количество неправильных ответов
        totals.text += "Неправильные ответы: " + PlayerPrefs.GetInt(Photos.category + "-errors") + Indents.LineBreak(2);

        // Выводим количество дополнительно открытых плиток
        totals.text += "Открытые фрагменты: " + PlayerPrefs.GetInt(Photos.category + "-buttons") + Indents.LineBreak(2);

        // Выводим количество полученных подсказок и пропусков
        totals.text += "Подсказки / пропуски: " + PlayerPrefs.GetInt(Photos.category + "-tips") + " / " + PlayerPrefs.GetInt(Photos.category + "-pass");
    }
}