using System;
using System.Collections.Generic;
using Controllers;
using Controllers.UI;
using DG.Tweening;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        
        [SerializeField] private List<GameObject> panels;
        //[SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private UIInputController uIInputController;

        #endregion

        #region Private Variables
        
        private UIPanelController _uiPanelController;

        #endregion

        #endregion

        private void Awake()
        {
            _uiPanelController = new UIPanelController(ref panels);
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelFailed += OnGameFailed;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetScoreText += OnSetScoreText;
            UISignals.Instance.onSetBestScore += OnSetBestScore;
            UISignals.Instance.onAddCharToInputText += OnAddCharToInputText;
            UISignals.Instance.onDeleteInputText += OnDeleteInputText;
            UISignals.Instance.onSubmitInputText += OnSubmitInputText;
            UISignals.Instance.onSetQuestionText += OnSetQuestionText;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onLevelFailed -= OnGameFailed;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetScoreText -= OnSetScoreText;
            UISignals.Instance.onAddCharToInputText -= OnAddCharToInputText;
            UISignals.Instance.onDeleteInputText -= OnDeleteInputText;
            UISignals.Instance.onSubmitInputText -= OnSubmitInputText;
            UISignals.Instance.onSetQuestionText -= OnSetQuestionText;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Start()
        {
        }

        private void OnOpenPanel(UIPanels panelParam)
        {
            _uiPanelController.Execute(panelParam , true);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            _uiPanelController.Execute(panelParam , false);
        }

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnGameFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        private void OnNextLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.NextLevel);
        }


        private void OnSetScoreText(ushort score)
        {
            //scoreText.text = score.ToString();
        }

        private void OnSetBestScore(ushort best)
        {
        }

        private void OnReset()
        {
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
        }

        public void Play()
        {
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        private void OnAddCharToInputText(char character)
        {
            uIInputController.AddCharToInputText(character);
        }

        private void OnDeleteInputText()
        {
            uIInputController.DeleteInputText();
        }

        private void OnSubmitInputText()
        {
            uIInputController.SubmitInputText();
        }

        private void OnSetQuestionText(string question)
        {
            questionText.text = question;
        }
    }
}