using UnityEngine;

public class ButtonsBall : MonoBehaviour
{
    private Statistics statistics;

    private void Awake()
    {
        statistics = Camera.main.GetComponent<Statistics>();
    }

    private void Start() { RemoveButton(); }

    // Открытие случайных плиток
    public void RemoveButton()
    {
        // Номера плиток
        int first, second;

        // Определяем номера плиток
        DefineRandomButton(out first, out second);

        // Скрываем выпавшие плитки
        transform.GetChild(first).GetComponent<Animator>().enabled = true;
        transform.GetChild(second).GetComponent<Animator>().enabled = true;
    }

    // Определение двух случайных номеров
    private void DefineRandomButton(out int first, out int second)
    {
        first = Random.Range(0, 15);
        second = Random.Range(0, 16);

        // Если номера совпали, увеличиваем второй номер
        if (second == first) second = first + 1;
    }
}