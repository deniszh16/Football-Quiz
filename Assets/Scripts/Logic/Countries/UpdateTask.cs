﻿using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.Countries
{
    public class UpdateTask : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        
        [Header("Кнопка обновления")]
        [SerializeField] private Button _button;

        private void Awake() =>
            _button.onClick.AddListener(GoToNextTask);
        
        private void OnDestroy() =>
            _button.onClick.RemoveListener(GoToNextTask);

        public void ToggleButton(bool state) =>
            _button.interactable = state;

        private void GoToNextTask()
        {
            _tasks.GetCurrentTask();
            _tasks.CheckTaskExists();
            ToggleButton(state: false);
        }
    }
}