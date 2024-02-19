using System;
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
        [SerializeField] private new Renderer renderer;

        #endregion

        #endregion

        private void Awake()
        {
            //_material = gameObject.GetComponent<Renderer>().material;
        }

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

        private void OnWriteLetter(int id,char letter,Color32 color)
        {
            if (gameObject.GetInstanceID() == id)
            {
                letterText.text = letter.ToString();
                renderer.material.color = color;
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