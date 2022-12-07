using Data.UnityObject;
using Data.ValueObject;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ScoreTimeController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro timeText;

        #endregion

        #region Private Variables
        
        private float _currentTime;
        private TimeData _data;

        #endregion
        
        #endregion

        private void Awake()
        {
            _data = GetTimeData();
            _currentTime = _data.TimeBorder;
        }
        
        private void Update()
        {
            SetTimer();
        }
        
        private TimeData GetTimeData() => Resources.Load<CD_Time>("Data/CD_Time").TimeData;
        
        private void SetTimer()
        {
            if (_currentTime>0)
            {
                _currentTime -= Time.deltaTime;
                DisplayTimer(_currentTime);
            }
            else
            {
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
                ResetTime();
            }
        }

        private void DisplayTimer(float remainingTime)
        {
            float minutes = Mathf.FloorToInt(remainingTime / 60);
            float seconds = Mathf.FloorToInt(remainingTime % 60); 
            timeText.text = $"{minutes:00}:{seconds:00}";
        }

        private void ResetTime()
        {
            _currentTime = _data.TimeBorder;
        }
    }
}