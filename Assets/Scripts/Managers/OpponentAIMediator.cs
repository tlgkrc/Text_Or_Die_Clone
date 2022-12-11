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

        #endregion
        
        #endregion
        
        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSubscribeOpponentMediator += OnSubscribeMediator;
            QASignals.Instance.onDistributeAIAnswers += OnDistributeAIAnswers;
            PlayerSignals.Instance.onGetAICount += OnGetAICount;
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onSubscribeOpponentMediator -= OnSubscribeMediator;
            QASignals.Instance.onDistributeAIAnswers -= OnDistributeAIAnswers;
            PlayerSignals.Instance.onGetAICount -= OnGetAICount;
            CoreGameSignals.Instance.onPlay -= OnPlay;
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

        private void OnDistributeAIAnswers(List<string> answerList)
        {
            for (int i = 0; i < _opponentList.Count; i++)
            {
                _opponentList[i].WriteAnswerToPlatform(answerList[i]);
            }
        }

        private ushort OnGetAICount()
        {
            return (ushort)_opponentList.Count();
        }

        private void OnPlay()
        {
            
        }
    }
}