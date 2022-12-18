using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class StairManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro letterText;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            QASignals.Instance.onWriteLetterToStair += OnWriteLetter;
            LevelSignals.Instance.onCheckWaterLevel += OnCheckWaterLevel;
        }

        private void UnsubscribeEvents()
        {
            QASignals.Instance.onWriteLetterToStair -= OnWriteLetter;
            LevelSignals.Instance.onCheckWaterLevel -= OnCheckWaterLevel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnWriteLetter(int id,char letter)
        {
            if (gameObject.GetInstanceID() == id)
            {
                letterText.text = letter.ToString();
            }
        }

        private void OnCheckWaterLevel(float waterLevel)
        {
            if (!(waterLevel > transform.position.y)) return;
            var newColor = letterText.color;
            letterText.color = new Color(newColor.a, newColor.b, newColor.g, .2f);
        }
    }
}