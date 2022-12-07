using Commands;
using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables


        #endregion

        #region Private Variables

        private bool _isReadyForTouch; 
        private bool _isFirstTimeTouchTaken;
        private float _currentVelocity; //ref type
        private float _screenWidth;
        private Vector2? _mousePosition; //ref type
        private Vector3 _moveVector; //ref type
        private QueryPointerOverUIElementCommand _queryPointerOverUIElementCommand;

        #endregion

        #endregion
        
        private void Awake()
        {
            Data = GetInputData();
            Init();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        private void Init()
        {
            _queryPointerOverUIElementCommand = new QueryPointerOverUIElementCommand();
            _screenWidth = Screen.width;
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void Update()
        {
            //if (!_isReadyForTouch) return;
            
            if (Input.GetMouseButtonUp(0) && !_queryPointerOverUIElementCommand.Execute())
            {
                MouseButtonUp();
            }
            
           
            if (Input.GetMouseButtonDown(0) && !_queryPointerOverUIElementCommand.Execute())
            {
                MouseButtonDown();
            }
        }

        #region Event Methods
        
        private void OnEnableInput()
        {
            _isReadyForTouch = true;
        }
        
        private void OnDisableInput()
        {
            _isReadyForTouch = false;
        }
        
        private void OnPlay()
        {
            _isReadyForTouch = true;
        }

        private void OnReset()
        {
            _isReadyForTouch = false;
            _isFirstTimeTouchTaken = false;
        }

        #endregion
        
        private void MouseButtonUp()
        {
            InputSignals.Instance.onInputReleased?.Invoke();
        }

        private void MouseButtonDown()
        {
            InputSignals.Instance.onInputTaken?.Invoke();
            if (!_isFirstTimeTouchTaken)
            {
                _isFirstTimeTouchTaken = true;
                InputSignals.Instance.onFirstTimeTouchTaken?.Invoke();
            }
        }
    }
}