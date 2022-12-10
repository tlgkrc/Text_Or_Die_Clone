using System;
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

        #endregion

        #region Private Variables

        private string _inputText;

        #endregion
        
        #endregion

        private void Awake()
        {
            SetInputText();
        }

        public void AddCharToInputText(char character)
        {
            _inputText +=  character;
            _inputText = _inputText.ToUpper();
            SetInputText();
        }

        public void DeleteInputText()
        {
            _inputText = ""; 
            SetInputText();
        }

        public void SubmitInputText()
        {
            UISignals.Instance.onCheckAnswer?.Invoke(_inputText);
        }

        private void SetInputText()
        {
            inputTextTMP.text = _inputText;
            inputCharCountTMP.text = _inputText == null ? 0.ToString() : _inputText.Length.ToString();
        }
    }
}