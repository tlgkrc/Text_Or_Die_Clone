using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Opponent
{
    public class OpponentPhysicController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private OpponentAIManager manager;

        #endregion

        #region Private Variables



        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Water"))
            {
                PlayerSignals.Instance.onUnsubscribeOpponentMediator?.Invoke(manager);
                
                manager.DeactivateOpponent();
            }
        }
    }
}