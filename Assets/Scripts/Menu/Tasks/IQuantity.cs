namespace Cubra
{
    public interface IQuantity
    {
        // Количество категорий
        int QuantityCategories { get; }

        // Количество заданий в категориях
        int this[int index] { get; }
    }
}