using UnityEngine;

[CreateAssetMenu(fileName = "New Tasks", menuName = "Tasks", order = 51)]
public class Tasks : ScriptableObject, IQuantity
{
    [Header("Количество вопросов")]
    [SerializeField] private int[] quantity;

    // Свойство для получения длины массива
    public int quantityLength { get { return quantity.Length; } }

    // Индексатор для получения элементов массива
    public int this[int index] { get { return quantity[index]; } }
}