using UnityEngine;
using UnityEngine.UI;

public class Totals : MonoBehaviour
{
    private Text totals;

    private void Awake() { totals = GetComponent<Text>(); }

    private void Start()
    {
        // Выводим общее количества заданий
        totals.text += Indents.LineBreak(1) + "Всего вопросов: " + PlayerPrefs.GetInt(Modes.category) + Indents.LineBreak(2);
        // Выводим количество неправильных ответов
        totals.text += "Неправильные ответы: " + PlayerPrefs.GetInt(Modes.category + "-errors") + Indents.LineBreak(2);
        // Выводим количество дополнительно открытых плиток
        totals.text += "Открытые фрагменты: " + PlayerPrefs.GetInt(Modes.category + "-buttons") + Indents.LineBreak(2);
        // Выводим количество полученных подсказок по названию команд
        totals.text += "Полученные подсказки: " + PlayerPrefs.GetInt(Modes.category + "-tips");
    }
}