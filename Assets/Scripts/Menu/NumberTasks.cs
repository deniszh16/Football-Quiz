using UnityEngine;

[CreateAssetMenu(fileName = "New Tasks", menuName = "NumberTasks", order = 51)]
public class NumberTasks : ScriptableObject, IQuantity
{
    [Header("Количество вопросов")]
    [SerializeField] private int[] quantity;

    // Свойство для получения длины массива
    public int quantityLength => quantity.Length;

    // Индексатор для получения элементов массива
    public int this[int index] => quantity[index];
}