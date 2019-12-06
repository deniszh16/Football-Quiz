using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
    [Header("Рейтинг игрока")]
    [SerializeField] private Text myRating;

    [Header("Таблица лидеров")]
    [SerializeField] private Text leaderboard;

    // Ссылка на аниматор текста
    private Animator animator;

    [Header("Компонент скроллинга")]
    [SerializeField] private ScrollRect scroll;

    [Header("Кнопка обновления")]
    [SerializeField] private GameObject buttonUpdate;

    // Объект для работы с json по таблице лидеров
    private LeaJson leaderboardJson = new LeaJson();

    private void Awake()
    {
        animator = leaderboard.gameObject.GetComponent<Animator>();

        // Преобразуем json строку в объект
        leaderboardJson = JsonUtility.FromJson<LeaJson>(PlayerPrefs.GetString("leaders"));
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Если пользователь авторизирован
            if (Social.localUser.authenticated)
            {
                // Отображаем информацию о загрузке
                leaderboard.text = "Загрузка...";
                animator.Play("Loading");

                // Отправляем свой результат в таблицу лидеров
                PlayServices.PostingScoreLeaderboard(PlayerPrefs.GetInt("score"));

                // Загружаем результаты
                LoadScoresLeaderboard();
            }
        }
        else
        {
            // Скрываем кнопку обновления
            buttonUpdate.SetActive(false);

            // Выводим сохраненные данные
            ShowResultsFile();
        }
    }

    /// <summary>Загрузка результатов из удаленной таблицы лидеров</summary>
    public void LoadScoresLeaderboard()
    {
        // Загружаем десять лучших результатов из публичной таблицы
        PlayGamesPlatform.Instance.LoadScores(
            GPGSIds.leaderboard,
            LeaderboardStart.TopScores,
            10,
            LeaderboardCollection.Public,
            LeaderboardTimeSpan.AllTime,
            (data) =>
            {
                // Записываем позицию текущего игрока
                leaderboardJson.Rating = data.PlayerScore.rank;
                // Выводим позицию в рейтинге
                myRating.text = "Моя позиция - " + data.PlayerScore.rank + " место";

                // Загружаем информацию по другим игрокам
                LoadUsersLeaderboard(data.Scores);
            }
        );
    }

    /// <summary>Загрузка и отображение информации по игрокам (массив результатов)</summary>
    private void LoadUsersLeaderboard(IScore[] scores)
    {
        // Список id пользователей
        var userIds = new List<string>();
        // Перебираем результаты и добавляем id в список
        foreach (IScore score in scores) userIds.Add(score.userID);

        // Загружаем информацию по пользователям
        Social.LoadUsers(userIds.ToArray(), (users) =>
        {
            // Убираем анимацию загрузки
            animator.Play("Results");
            leaderboard.text = "";

            for (int i = 0; i < scores.Length; i++)
            {
                // Создаем пользователя и вызываем поиск его id массиве
                IUserProfile user = FindUser(users, scores[i].userID);

                // Выводим результаты в текстовое поле
                leaderboard.text += (i + 1) + " - " + ((user != null) ? user.userName : "Unknown") + " (" + scores[i].value + ")" + ((i < 9) ? Indents.LineBreak(2) : "");
                
                // Записываем в json имена и результаты игроков
                leaderboardJson.Names[i] = (user != null) ? user.userName : "Unknown";
                leaderboardJson.Results[i] = scores[i].value;
            }

            // Сохраняем обновленные данные по игрокам
            PlayerPrefs.SetString("leaders", JsonUtility.ToJson(leaderboardJson));
        });

        // Перемещаем скролл вверх списка
        scroll.verticalNormalizedPosition = 1;
    }

    /// <summary>Поиск игрока в массиве по id (массив игроков, id игрока)</summary>
    private IUserProfile FindUser(IUserProfile[] users, string userid)
    {
        foreach (IUserProfile user in users)
        {
            // Если id совпадают, возвращаем игрока
            if (user.id == userid) return user;
        }

        return null;
    }

    /// <summary>Отображение сохраненных данных по игрокам</summary>
    private void ShowResultsFile()
    {
        // Если рейтинг больше нуля
        if (leaderboardJson.Rating > 0)
            // Выводим позицию текущего игрока в рейтинге
            myRating.text = "Моя позиция - " + leaderboardJson.Rating.ToString() + " место";

        for (int i = 0; i < leaderboardJson.Results.Length; i++)
        {
            // Если первый результат не нулевой, очищаем текст результатов
            if (i == 0 && leaderboardJson.Results[i] > 0) leaderboard.text = "";

            if (leaderboardJson.Results[i] > 0)
                // Выводим результаты игроков
                leaderboard.text += (i + 1) + " - " + leaderboardJson.Names[i] + " (" + leaderboardJson.Results[i] + ")" + ((i < 9) ? Indents.LineBreak(2) : "");
        }

        // Перемещаем скролл вверх
        scroll.verticalNormalizedPosition = 1;
    }
}