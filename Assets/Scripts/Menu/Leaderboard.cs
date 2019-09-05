using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class Leaderboard : MonoBehaviour
{
    [Header("Мой рейтинг")]
    [SerializeField] private Text myRating;

    [Header("Таблица лидеров")]
    [SerializeField] private Text leaderboard;

    // Анимация текста загрузки
    private Animator animator;

    [Header("Компонент скроллинга")]
    [SerializeField] private ScrollRect scroll;

    [Header("Кнопка обновления")]
    [SerializeField] private GameObject buttonUpdate;

    // Объект для json по локальной таблице
    private LeaderboardJson leaderboardJson = new LeaderboardJson();

    private void Awake()
    {
        scroll = scroll.GetComponent<ScrollRect>();
        animator = leaderboard.gameObject.GetComponent<Animator>();

        // Преобразуем json строку в объект
        leaderboardJson = JsonUtility.FromJson<LeaderboardJson>(PlayerPrefs.GetString("leaderboard"));
    }

    private void Start()
    {
        // Если доступен интернет
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            // Если пользователь вошел в аккаунт
            if (Social.localUser.authenticated)
            {
                // Отображаем информацию о загрузке
                leaderboard.text = "Загрузка...";
                animator.SetBool("Loading", true);

                // Отправляем свой результат в таблицу лидеров
                PlayServices.PostingScoreLeaderboard(PlayerPrefs.GetInt("score"));

                // Загружаем результаты с сервера
                LoadScoresLeaderboard();
            }
        }
        else
        {
            // Иначе скрываем кнопку обновления результатов
            buttonUpdate.SetActive(false);
            // Выводим локальные данные
            ShowResultsFile();
        }
    }

    // Загрузка результатов из удаленной таблицы лидеров
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
                // Записываем позицию текущего игрока в файл
                leaderboardJson.rating = data.PlayerScore.rank;
                // Выводим позицию в рейтинге
                myRating.text = "Моя позиция - " + data.PlayerScore.rank + " место";

                // Загружаем информацию по остальным игрокам
                LoadUsersLeaderboard(data.Scores);
            }
        );
    }

    // Загрузка и отображение информации по игрокам
    private void LoadUsersLeaderboard(IScore[] scores)
    {
        // Создаем список из id пользователей
        List<string> userIds = new List<string>();

        // Перебираем результаты в массиве и добавляем в список id игроков
        foreach (IScore score in scores) { userIds.Add(score.userID); }

        // Загружаем информацию по пользователям
        Social.LoadUsers(userIds.ToArray(), (users) =>
        {
            // Отключаем анимацию загрузки
            animator.SetBool("Loading", false);
            // Сбрасываем текст
            leaderboard.text = "";

            // Номер для позиции игрока
            int rankingPosition = 0;

            // Перебираем результаты в массиве
            foreach (IScore score in scores)
            {
                // Создаем пользователя и вызываем поиск его id массиве
                IUserProfile user = FindUser(users, score.userID);

                // Выводим результаты в текстовое поле
                leaderboard.text += (rankingPosition + 1) + " - " + ((user != null) ? user.userName : "Unknown") + " (" + score.value + ")" + ((rankingPosition < 9) ? Indents.LineBreak(2) : "");

                // Записываем в json имена и результаты игроков
                leaderboardJson.names[rankingPosition] = (user != null) ? user.userName : "Unknown";
                leaderboardJson.results[rankingPosition] = score.value;

                // Увеличиваем номер
                rankingPosition++;
            }
        });

        // Перемещаем скролл вверх
        scroll.verticalNormalizedPosition = 1;
        // Сохраняем обновленные данные
        PlayerPrefs.SetString("leaderboard", JsonUtility.ToJson(leaderboardJson));
    }

    // Поиск игрока в массиве по id
    private IUserProfile FindUser(IUserProfile[] users, string userid)
    {
        // Переборка игроков в массиве
        foreach (IUserProfile user in users)
        {
            // Если id совпадают, возвращаем найденного игрока
            if (user.id == userid) return user;
        }

        return null;
    }

    // Отображение результатов из json файла
    private void ShowResultsFile()
    {
        // Если рейтинг игрока больше нуля
        if (leaderboardJson.rating > 0)
            // Выводим позицию в рейтинге
            myRating.text = "Моя позиция - " + leaderboardJson.rating.ToString() + " место";

        for (int i = 0; i < leaderboardJson.names.Length; i++)
        {
            // Если имя в массиве не равно стандартному значению
            if (leaderboardJson.names[i] != "noname")
            {
                // Сбрасываем стандартный текст
                if (i == 0) leaderboard.text = "";

                // Выводим результаты
                leaderboard.text += (i + 1) + " - " + leaderboardJson.names[i] + " (" + leaderboardJson.results[i] + ")" + ((i < 9) ? Indents.LineBreak(2) : "");
            }
        }

        // Перемещаем скролл вверх
        scroll.verticalNormalizedPosition = 1;
    }
}