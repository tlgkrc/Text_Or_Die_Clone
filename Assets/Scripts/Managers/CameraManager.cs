using Cinemachine;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private CinemachineVirtualCamera startCam;
        [SerializeField] private CinemachineVirtualCamera levelCam;
       
        #endregion

        #region Private Variables

        private Animator _camAnimator;
        private CameraStates _cameraState;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            OnSetCameraState(CameraStates.StartCam);
        }

        private void GetReferences()
        {
            _camAnimator = GetComponent<Animator>();
        }

        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void SetCameraStates()
        {
            if (_cameraState == CameraStates.StartCam)
            {
                _camAnimator.Play(CameraStates.StartCam.ToString());
            }
            else if (_cameraState == CameraStates.LevelCam)
            {
                _camAnimator.Play(CameraStates.LevelCam.ToString());
            }
        }

        private void OnSetCameraState(CameraStates cameraState)
        {
            _cameraState = cameraState;
            SetCameraStates();
        }
        
        private void OnPlay()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            levelCam.Follow = playerManager;
            OnSetCameraState(CameraStates.LevelCam);
        }
     
        private void OnReset()
        {
            _cameraState = CameraStates.LevelCam;
            levelCam.Follow = null; 
            levelCam.LookAt = null;
        }
    }
}