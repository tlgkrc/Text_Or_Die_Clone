using Enums;
using Managers;
using UnityEngine;

namespace Controllers.Opponent
{
    public class OpponentAnimController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private OpponentAIManager manager;
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