﻿using UnityEngine;
using UnityEngine.UI;

namespace DZGames.Football.Facts
{
    public class VariantButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        
        [Header("Кнопка варианта")]
        [SerializeField] private Button _button;
        [SerializeField] private bool _value;

        private void Awake() =>
            _button.onClick.AddListener(PushButton);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(PushButton);

        private void PushButton()
        {
            _answer.CheckAnswer(_value);
            gameObject.SetActive(false);
        }
    }
}