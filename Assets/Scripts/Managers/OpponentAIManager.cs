using System;
using Controllers.Opponent;
using DG.Tweening;
using Signals;
using UnityEngine;

namespace Managers
{
    public class OpponentAIManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private OpponentAnimController animController;
        [SerializeField] private OpponentPlatformController platformController;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void Start()
        {
            platformController.AddDefaultPlatform();
            PlayerSignals.Instance.onSubscribeOpponentMediator?.Invoke(this);
        }

        public void RiseOpponent(float sizeOfStairs)
        {
            transform.DOLocalMoveY(transform.position.y + sizeOfStairs,.5f).SetEase(Ease.OutBack);
        }

        public void WriteAnswerToPlatform(string answer)
        {
            platformController.WriteTrueAnswerToPlatforms(answer);
        }

        public void MoveX(short distance)
        {
            animController.PlayJumpAnim();
            transform.DOMoveX(transform.position.x + distance, .5f).OnComplete(()=> 
                animController.PlayIdleAnim());
            platformController.MovePlatform(distance);
        }
    }
}