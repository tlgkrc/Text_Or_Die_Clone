using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class LevelQuestionController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        private TextAsset _textAsset;
        private string[] _questionsAndAnswers;
        private string[] _currentAnswers;
        private List<ushort> _indexList;

        #endregion
        
        #endregion

        private void Start()
        {
            SetCurrentQuestion(1);
        }

        public void SetQuestionData(TextAsset textAsset)
        {
            _textAsset = textAsset;
            ReadCSV();
        }

        private void ReadCSV()
        {
            _questionsAndAnswers = _textAsset.text.Split(new string[]{"\n"},StringSplitOptions.None);
        }

        private void SetCurrentQuestion(ushort index)
        {
            var question = _questionsAndAnswers[index].Split(",")[0];
            UISignals.Instance.onSetQuestionText?.Invoke(question);
            _currentAnswers = _questionsAndAnswers[index].Split(",")[1].Trim().Split("|");
        }

        public void CheckAnswer(string playerAnswer)
        {
            if (_currentAnswers.Contains(playerAnswer))
            {
                var index = Array.IndexOf(_currentAnswers, playerAnswer);
                _indexList.Add((ushort)index);
               ScoreSignals.Instance.onUpdatePlayerScore?.Invoke((ushort)playerAnswer.Length);
               Debug.Log("dogru cevap");
               
            }
            else
            {
                
            }
            
            var aiCount = PlayerSignals.Instance.onGetAICount?.Invoke();
            for (int i = 0; i < aiCount; i++)
            {
                var index = (ushort)Random.Range(0, _currentAnswers.Length);
                while (_indexList.Contains(index))
                {
                    index = (ushort)Random.Range(0, _currentAnswers.Length);
                }

                _indexList.Add(index);
            }
            //PlayerSignals.Instance.onSetAIAnswers?.Invoke();
        }
        
    }
}