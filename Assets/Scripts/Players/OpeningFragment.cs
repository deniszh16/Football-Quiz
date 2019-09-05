using UnityEngine;

public class OpeningFragment : MonoBehaviour
{
    private Statistics statistics;

    private void Awake() { statistics = Camera.main.GetComponent<Statistics>(); }

    // Открытие дополнительного фрагмента
    public void OpenFragment(GameObject button)
    {
        // Если достаточно монет для открытия
        if (PlayerPrefs.GetInt("coins") >= 35)
        {
            // Получаем у кнопки аниматор и активируем анимацию
            button.GetComponent<Animator>().enabled = true;

            // Вычитаем стоимость подсказки и обновляем статистику
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 35);
            statistics.UpdateCoins(true);

            // Обновляем общее количество открытых фрагментов
            PlayerPrefs.SetInt(Modes.category + "-buttons", PlayerPrefs.GetInt(Modes.category + "-buttons") + 1);
        }
        // Иначе вызываем мигание монет
        else statistics.UpdateCoins(true);
    }
}