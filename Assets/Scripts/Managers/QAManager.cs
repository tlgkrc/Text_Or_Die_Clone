using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class QAManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private QACurrentQuestionController questionController;

        #endregion

        #region Private Variables

        private TextAsset _textAsset;

        #endregion

        #endregion

        private void Awake()
        {
            _textAsset = GetQuestions();
            SendDataToControllers();
        }

        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onCheckAnswer += OnCheckAnswer;
            QASignals.Instance.onNextQuestion += OnNextQuestion;
            QASignals.Instance.onGetQuestionIndex += OnGetQuestionIndex;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onCheckAnswer -= OnCheckAnswer;
            QASignals.Instance.onNextQuestion -= OnNextQuestion;
            QASignals.Instance.onGetQuestionIndex -= OnGetQuestionIndex;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private TextAsset GetQuestions()
        {
            return Resources.Load<CD_Questions>("Data/CD_Questions").QuestionsDatas.TextAsset;
        }

        private void SendDataToControllers()
        {
            questionController.SetQuestionData(_textAsset);
        }

        private void OnCheckAnswer(string answer)
        {
            questionController.CheckAnswer(answer);
        }

        private void OnNextQuestion()
        {
            questionController.NextQuestion();
        }

        private float OnGetQuestionIndex()
        {
            return questionController.GetQuestionIndex();
        }
    }
}