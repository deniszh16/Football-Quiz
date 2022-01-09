using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Players
{
    public class TaskList : FileProcessing
    {
        [Header("������������ ��������")]
        [SerializeField] private TextMeshProUGUI _questions;

        [Header("��������� �������")]
        [SerializeField] private ScrollRect _scrollRect;

        private PlayersHelpers _playersHelpers;

        private void Awake()
        {
            _playersHelpers = new PlayersHelpers();

            // ������������ json ���� � ���������� � ����������
            string jsonString = ReadJsonFile("players-" + Sets.Category);
            // ��������������� ������ � ������
            ConvertToObject(ref _playersHelpers, jsonString);
        }

        private void Start()
        {
            // ������� ��� ������� �� ���������
            for (int i = 0; i < _playersHelpers.PhotoTasks.Length; i++)
            {
                _questions.text += IndentsHelpers.LineBreak(1) + _playersHelpers.PhotoTasks[i].Question + IndentsHelpers.LineBreak(2);
                _questions.text += "�����: " + _playersHelpers.PhotoTasks[i].Description + IndentsHelpers.LineBreak(1);
                _questions.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}