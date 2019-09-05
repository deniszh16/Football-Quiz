public interface IQuantity
{
    // Получение длины массива
    int quantityLength { get; }
    // Получение количества заданий
    int this[int index] { get; }
}