using Enums;
using Managers;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerAnimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private Animator animator;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public void PlayIdleAnim()
        {
            animator.SetTrigger(AnimTypes.Idle.ToString());
        }

        public void PlayJumpAnim()
        {
            animator.SetTrigger(AnimTypes.Jump.ToString());
        }
    }
}