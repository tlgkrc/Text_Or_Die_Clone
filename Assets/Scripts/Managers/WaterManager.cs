using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class WaterManager : MonoBehaviour
    {
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onRiseWaterLevel += OnRiseWaterLevel;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onRiseWaterLevel -= OnRiseWaterLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnRiseWaterLevel()
        {
            var index =  QASignals.Instance.onGetQuestionIndex?.Invoke();
            if (index != null) transform.DOLocalMoveY((float)(transform.position.y+ index+3), .5f).OnComplete(() =>
            {
                QASignals.Instance.onNextQuestion?.Invoke();
                LevelSignals.Instance.onCheckWaterLevel?.Invoke(transform.position.y);
            });
        }
    }
}