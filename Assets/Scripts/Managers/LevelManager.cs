using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
       
        #endregion

        #region Private Variables
        

        #endregion

        #endregion
        
        #region Event Supscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            LevelSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            LevelSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        
        private void OnLevelSuccessful()
        {
            
        }
    }
}