using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
    [Header("Рейтинг текущего игрока")]
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
        leaderboardJson = JsonUtility.FromJson<LeaJson>(PlayerPrefs.GetString("leaderboard"));
    }

    private void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Если пользователь вошел в аккаунт
            if (Social.localUser.authenticated)
            {
                // Отображаем информацию о загрузке
                leaderboard.text = "Загрузка...";
                animator.Play("Loading");

                // Отправляем результат в таблицу лидеров
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
                // Выводим позицию текущего игрока в рейтинге
                myRating.text = "Моя позиция - " + data.PlayerScore.rank + " место";
                // Записываем позицию текущего игрока
                leaderboardJson.Rating = data.PlayerScore.rank;

                // Загружаем информацию по другим игрокам
                LoadUsersLeaderboard(data.Scores);
            }
        );
    }

    /// <summary>Загрузка и отображение информации по игрокам (массив результатов)</summary>
    private void LoadUsersLeaderboard(IScore[] scores)
    {
        // Создаем список из id пользователей
        var userIds = new List<string>();

        foreach (IScore score in scores)
            // Добавляем id в список
            userIds.Add(score.userID);

        // Загружаем информацию по пользователям
        Social.LoadUsers(userIds.ToArray(), (users) =>
        {
            // Убираем анимацию загрузки
            animator.Play("Results");
            leaderboard.text = "";

            // Номер позиции игрока
            var rankingPosition = 0;

            foreach (IScore score in scores)
            {
                // Создаем пользователя и ищем его id в списке
                IUserProfile user = FindUser(users, score.userID);

                // Выводим позицию, имя игрока и его набранный счет
                leaderboard.text += (rankingPosition + 1) + " - " + ((user != null) ? user.userName : "Unknown") + " (" + score.value + ")" + ((rankingPosition < 9) ? Indents.LineBreak(2) : "");
                // Записываем данные по игроку
                SaveLeaderboardData(rankingPosition, (user != null) ? user.userName : "Unknown", score.value);

                rankingPosition++;
            }
        });

        // Перемещаем скролл вверх списка
        scroll.verticalNormalizedPosition = 1;

        // Сохраняем обновленные данные по игрокам
        PlayerPrefs.SetString("leaderboard", JsonUtility.ToJson(leaderboardJson));
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

    /// <summary>Сохранение загруженных результатов (позиция игрока, имя игрока, счет игрока)</summary>
    private void SaveLeaderboardData(int position, string user, long score)
    {
        // Записываем игрока и набранный счет
        leaderboardJson.Names[position] = user;
        leaderboardJson.Results[position] = score;
    }

    /// <summary>Отображение сохраненных данных по игрокам</summary>
    private void ShowResultsFile()
    {
        // Если рейтинг больше нуля
        if (leaderboardJson.Rating > 0)
            // Выводим позицию текущего игрока в рейтинге
            myRating.text = "Моя позиция - " + leaderboardJson.Rating.ToString() + " место";

        for (int i = 0; i < leaderboardJson.Names.Length; i++)
        {
            // Сбрасываем стандартный текст
            if (i == 0) leaderboard.text = "";

            // Выводим результаты по остальным игрокам
            leaderboard.text += (i + 1) + " - " + leaderboardJson.Names[i] + " (" + leaderboardJson.Results[i] + ")" + ((i < 9) ? Indents.LineBreak(2) : "");
        }

        // Перемещаем скролл вверх
        scroll.verticalNormalizedPosition = 1;
    }
}