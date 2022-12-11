using DG.Tweening;
using Managers;
using Signals;
using TMPro;
using UnityEngine;

namespace Controllers.UI
{
    public class UIAnimController : MonoBehaviour
    {
        #region Self Variables
        
        #region Serialized Variables

        [SerializeField] private GameObject question;
        [SerializeField] private GameObject answer;
        [SerializeField] private GameObject keyboard;

        #endregion

        #region Private Variables

        private Sequence _submitSequence;
        private Sequence _resetSequence;
        
        #endregion
        #endregion

        public void PlaySubmitAnim()
        {
            _submitSequence = DOTween.Sequence();
            _submitSequence.Append(question.transform.DOLocalMoveY(question.transform.localPosition.y+600f, 1f));
            _submitSequence.Append(answer.transform.DOLocalMoveX(answer.transform.localPosition.x + 1000f, 1f));
            _submitSequence.Join(keyboard.transform.DOLocalMoveY(keyboard.transform.localPosition.y - 600f, 1f).OnComplete(
                () =>
                {
                    LevelSignals.Instance.onRiseWaterLevel?.Invoke();
                }));
        }

        public void ResetUIAnim()
        {
            _resetSequence = DOTween.Sequence();
            _resetSequence.Append(question.transform.DOLocalMoveY(question.transform.localPosition.y-600f, 1f));
            _resetSequence.Append(answer.transform.DOLocalMoveX(answer.transform.localPosition.x - 1000f, 1f));
            _resetSequence.Join(keyboard.transform.DOLocalMoveY(keyboard.transform.localPosition.y + 600f, 1f));
        }

        
    }
}