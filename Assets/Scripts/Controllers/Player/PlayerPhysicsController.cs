using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}