using Cinemachine;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

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
            PlayerSignals.Instance.onGetPlayerTransform+= OnGetPlayerTransform;
        }

        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onGetPlayerTransform -= OnGetPlayerTransform;
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

        private void OnGetPlayerTransform(Transform playerTransform)
        {
           levelCam.Follow = playerTransform;
           OnSetCameraState(CameraStates.LevelCam);
        }
    }
}