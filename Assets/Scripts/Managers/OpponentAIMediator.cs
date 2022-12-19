using System.Collections.Generic;
using System.Linq;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class OpponentAIMediator : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Private Variables

        [ShowInInspector]private List<OpponentAIManager> _opponentList = new List<OpponentAIManager>();
        private const short _offset = 4;

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
                LevelSignals.Instance.onLevelSuccesfull?.Invoke();
            }

            MoveAI(opponentAIManager);
        }
        
        private void OnDistributeAIAnswers(List<string> answerList)
        {
            var randomNumber = Random.Range(0, answerList.Count*2);
            for (int i = 0; i < _opponentList.Count; i++)
            {
                _opponentList[i].WriteAnswerToPlatform(randomNumber == i ? "" : answerList[i]);
            }
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
                    PlayerSignals.Instance.onSetPlayerNewPos.Invoke(_offset / 2);
                }
                else
                {
                    PlayerSignals.Instance.onSetPlayerNewPos.Invoke(-_offset / 2);
                }
            }
            foreach (var opponentAI in _opponentList)
            {
                if (opponentAI.transform.position.x < posX)
                {
                    opponentAI.MoveX(_offset / 2);
                }
                else
                {
                    opponentAI.MoveX(-_offset / 2);
                }
            }
        }
    }
}