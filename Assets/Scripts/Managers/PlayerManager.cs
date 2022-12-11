using UnityEngine;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;
using TMPro;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

       

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimController animController;
        [SerializeField] private PlayerPlatformController platformController;
        [SerializeField] private TextMeshPro playerNameTMP;

        #endregion

        #region Private Variables

        private float _prePos;
        private PlayerData _data;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            SendDataToControllers();
        }

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            QASignals.Instance.onWriteTrueAnswer += OnWriteTrueAnswer;
            UISignals.Instance.onSetPlayerName += OnSetPlayerName;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            QASignals.Instance.onWriteTrueAnswer -= OnWriteTrueAnswer;
            UISignals.Instance.onSetPlayerName -= OnSetPlayerName;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        //private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_PlayerData").Data;

        private void GetReferences()
        {
            //_data = GetPlayerData();
        }

        private void SendDataToControllers()
        {
        }

        private void OnPlay()
        {
            
        }
        private void OnReset()
        {
        }

        private void OnActivateMovement()
        {
            PlayerSignals.Instance.onSetPlayerPos?.Invoke(transform.position.z);
        }

        private void OnDeactivateMovement()
        {
            ResetScale();
        }

        private void ResetScale()
        {
            transform.DOScale(Vector3.one, .5f);
        }

        private void OnWriteTrueAnswer(string trueAnswer)
        {
            platformController.WriteTrueAnswerToPlatforms(trueAnswer);
        }

        public void RisePlayer(float countOfStair,float delayTime)
        {
            transform.DOLocalMoveY( countOfStair,delayTime/2).SetEase(Ease.OutBack);
        }

        private void OnSetPlayerName(string playerName)
        {
            playerNameTMP.text = playerName;
        }
    }
}
