using UnityEngine;
using Controllers.Player;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Signals;

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
        [SerializeField] private PlayerMovementController movementController;

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
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactivateMovement;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            PlayerSignals.Instance.onGetNewPlayerScale += OnSetNewPlayerScale;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            PlayerSignals.Instance.onGetNewPlayerScale -= OnSetNewPlayerScale;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_PlayerData").Data;

        private void GetReferences()
        {
            _data = GetPlayerData();
        }

        private void SendDataToControllers()
        {
            movementController.SetData(_data);
        }

        private void OnPlay()
        {
            movementController.ActivateMovement();
        }
        private void OnReset()
        {
            movementController.StopPlayer();
        }

        private void OnActivateMovement()
        {
            PlayerSignals.Instance.onSetPlayerPos?.Invoke(transform.position.z);
        }

        private void OnDeactivateMovement()
        {
            ResetScale();
        }

        private void OnSetNewPlayerScale(float newScale)
        {
            var scale = new Vector3(newScale, newScale, transform.localScale.z);
            transform.DOScale(scale*_data.ScaleFactor, .5f);
        }

        private void ResetScale()
        {
            transform.DOScale(Vector3.one, .5f);
        }
    }
}
