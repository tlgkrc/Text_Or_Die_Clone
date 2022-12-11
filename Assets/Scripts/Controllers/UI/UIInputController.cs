using System;
using Managers;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class UIInputController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshProUGUI inputTextTMP;
        [SerializeField] private TextMeshProUGUI inputCharCountTMP;
        [SerializeField] private TextMeshProUGUI playerNameTMP;
        [SerializeField] private UIManager manager;

        #endregion

        #region Private Variables

        private string _inputText;
        private const string _defaultPlayerName = "YOU";

        #endregion
        
        #endregion

        private void Awake()
        {
            SetInputTextToAnswer();
        }

        public void AddCharToInputText(char character,bool isInStartPanel)
        {
            _inputText += character;
            _inputText = _inputText.ToUpper();
            if (isInStartPanel)
            {
                SetInputTextToPlayerName();
            }
            else
            {
                SetInputTextToAnswer();
            }
        }

        public void DeleteInputText(bool isInStartPanel)
        {
            _inputText = "";
            if (isInStartPanel)
            {
                SetInputTextToPlayerName();
            }
            else
            {
                SetInputTextToAnswer(); 
            }
            
        }

        public void SubmitInputText(bool isInStartPanel)
        {
            if (isInStartPanel)
            {
                manager.ArrangeKeyboardPanel();
            }
            else
            {
                if (_inputText.Length != 0)
                {
                    UISignals.Instance.onCheckAnswer?.Invoke(_inputText);
                    manager.ActivateSubmitAnim();
                }
            }
        }

        private void SetInputTextToAnswer()
        {
            inputTextTMP.text = _inputText;
            inputCharCountTMP.text = _inputText == null ? 0.ToString() : _inputText.Length.ToString();
        }

        private void SetInputTextToPlayerName()
        {
            playerNameTMP.text = _inputText.Length <= 0 ? _defaultPlayerName : _inputText;
            UISignals.Instance.onSetPlayerName?.Invoke(_inputText);
        }
    }
}