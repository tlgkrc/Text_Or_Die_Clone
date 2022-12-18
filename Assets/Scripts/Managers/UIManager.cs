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
        [SerializeField] private TextMeshProUGUI questionText;
        [SerializeField] private UIInputController uIInputController;
        [SerializeField] private GameObject keyboardGameObject;
        [SerializeField] private UIAnimController animController;

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
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
            LevelSignals.Instance.onNextTournament -= OnNextTournament;
            LevelSignals.Instance.onRiseWaterLevel += OnRiseWaterLevelWarning;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetScoreText += OnSetScoreText;
            UISignals.Instance.onAddCharToInputText += OnAddCharToInputText;
            UISignals.Instance.onDeleteInputText += OnDeleteInputText;
            UISignals.Instance.onSubmitInputText += OnSubmitInputText;
            UISignals.Instance.onSetQuestionText += OnSetQuestionText;
            QASignals.Instance.onNextQuestion += OnNextQuestion;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onNextTournament -= OnNextTournament;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
            LevelSignals.Instance.onRiseWaterLevel -= OnRiseWaterLevelWarning;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetScoreText -= OnSetScoreText;
            UISignals.Instance.onAddCharToInputText -= OnAddCharToInputText;
            UISignals.Instance.onDeleteInputText -= OnDeleteInputText;
            UISignals.Instance.onSubmitInputText -= OnSubmitInputText;
            UISignals.Instance.onSetQuestionText -= OnSetQuestionText;
            QASignals.Instance.onNextQuestion -= OnNextQuestion;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

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
            keyboardGameObject.SetActive(true);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
        }

        private void OnNextTournament()
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
            uIInputController.AddCharToInputText(character,panels[(int)UIPanels.StartPanel].activeInHierarchy);
        }

        private void OnDeleteInputText()
        {
            uIInputController.DeleteInputText(panels[(int)UIPanels.StartPanel].activeInHierarchy);
        }

        private void OnSubmitInputText()
        {
            uIInputController.SubmitInputText(panels[(int)UIPanels.StartPanel].activeInHierarchy);
        }

        private void OnSetQuestionText(string question)
        {
            questionText.text = question;
        }

        public void ArrangeKeyboardPanel()
        {
            keyboardGameObject.SetActive(!keyboardGameObject.activeInHierarchy);
        }

        public void ActivateSubmitAnim()
        {
            animController.PlaySubmitAnim();
        }

        private void OnNextQuestion()
        {
            animController.ResetUIAnim();
            uIInputController.DeleteInputText(false);
        }

        private void OnRiseWaterLevelWarning()
        {
            animController.PlayWarningAnim();
        }
    }
}