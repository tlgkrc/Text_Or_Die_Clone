using Data.ValueObject;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private Rigidbody rb;

        #endregion

        #region Private Variables
        
        private PlayerData _playerData;
        private bool _isReadyToPlay;
        
        #endregion

        #endregion

        private void Awake()
        {
        }

        public void SetData(PlayerData playerData)
        {
            _playerData = playerData;
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                Move();
            }
            else
            {
                StopPlayer();
            }
            
        }

        private void Move()
        {
            var velocity = new Vector3(0, 0, _playerData.ForwardSpeed);
            rb.velocity = velocity;
        }

        public void StopPlayer()
        {
            rb.velocity =Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        public void ActivateMovement()
        {
            _isReadyToPlay = true;
        }

        public void DeactivateMovement()
        {
            _isReadyToPlay = false;
        }
    }
}