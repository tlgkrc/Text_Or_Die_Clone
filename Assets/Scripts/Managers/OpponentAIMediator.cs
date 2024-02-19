using System;
using System.Collections.Generic;
using System.Linq;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class OpponentAIMediator : MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        [ShowInInspector]private List<OpponentAIManager> _opponentList = new List<OpponentAIManager>();
        private const short Offset = 4;
        private List<int> _indexList = new List<int>();
        private int _randomNumber;
        private float _randomNumberFactor = 1.5f;
        private float _randomNumberIncreaseMultiplier = 1.3f; //will reduce the number of correct answers for AI's
        #endregion
        
        #endregion
        
        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            PlayerSignals.Instance.onGetAICount += OnGetAICount;
            PlayerSignals.Instance.onSubscribeOpponentMediator += OnSubscribeMediator;
            PlayerSignals.Instance.onUnsubscribeOpponentMediator += OnUnsubscribeOpponentMediator;
            QASignals.Instance.onDistributeAIAnswers += OnDistributeAIAnswers;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            PlayerSignals.Instance.onGetAICount -= OnGetAICount;
            PlayerSignals.Instance.onSubscribeOpponentMediator -= OnSubscribeMediator;
            PlayerSignals.Instance.onUnsubscribeOpponentMediator -= OnUnsubscribeOpponentMediator;
            QASignals.Instance.onDistributeAIAnswers -= OnDistributeAIAnswers;
            
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnSubscribeMediator(OpponentAIManager opponentAIManager)
        {
            _opponentList.Add(opponentAIManager);
        }
        
        private void OnUnsubscribeOpponentMediator(OpponentAIManager opponentAIManager)
        {
            _opponentList.Remove(opponentAIManager);
            _opponentList.TrimExcess();
            if (_opponentList.Count<= 0)
            {
                LevelSignals.Instance.onLevelSuccessful?.Invoke();
            }

            MoveAI(opponentAIManager);
        }
        
        private void OnDistributeAIAnswers(List<string> answerList)
        {
            
            for (int i = 0; i < _opponentList.Count; i++)
            {
                _randomNumber = (int)Random.Range(0, answerList.Count*_randomNumberFactor);
                
                while (_indexList.Contains(_randomNumber))
                {
                    _randomNumber = (int)Random.Range(0, answerList.Count*_randomNumberFactor);
                }
                
                if (_randomNumber > answerList.Count-1)
                {
                    continue;
                }
                _indexList.Add(_randomNumber);
                _opponentList[i].WriteAnswerToPlatform(answerList[_randomNumber]);
            }
            _indexList.Clear();
            _indexList.TrimExcess();
            _randomNumberFactor *= _randomNumberIncreaseMultiplier;
        }

        private ushort OnGetAICount()
        {
            return (ushort)_opponentList.Count();
        }

        private void OnPlay()
        {
        }
        
        private void MoveAI(OpponentAIManager opponentAIManager)
        {
            var posX = opponentAIManager.transform.position.x;

            if (_opponentList.Count % 2 == 0)
            {
                PlayerSignals.Instance.onSetPlayerNewPos?.Invoke(0);
            }
            else
            {
                if (posX > 0)
                {
                    PlayerSignals.Instance.onSetPlayerNewPos.Invoke(Offset / 2);
                }
                else
                {
                    PlayerSignals.Instance.onSetPlayerNewPos.Invoke(-Offset / 2);
                }
            }
            foreach (var opponentAI in _opponentList)
            {
                if (opponentAI.transform.position.x < posX)
                {
                    opponentAI.MoveX(Offset / 2);
                }
                else
                {
                    opponentAI.MoveX(-Offset / 2);
                }
            }
        }
    }
}