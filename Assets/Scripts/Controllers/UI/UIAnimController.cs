using DG.Tweening;
using Signals;
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
        [SerializeField] private GameObject warningTMP;

        #endregion

        #region Private Variables

        private Sequence _submitSequence;
        private Sequence _resetSequence;
        private Sequence _warningSequence;

        #endregion

        #endregion

        public void PlaySubmitAnim()
        {
            _submitSequence = DOTween.Sequence();
            _submitSequence.Append(question.transform.DOLocalMoveY(question.transform.localPosition.y + 600f, 1f));
            _submitSequence.Append(answer.transform.DOLocalMoveX(answer.transform.localPosition.x + 1000f, 1f));
            _submitSequence.Join(keyboard.transform.DOLocalMoveY(keyboard.transform.localPosition.y - 600f, 1f)
                .OnComplete(
                    () => { LevelSignals.Instance.onRiseWaterLevel?.Invoke(); }));
        }

        public void ResetUIAnim()
        {
            _resetSequence = DOTween.Sequence();
            _resetSequence.Append(question.transform.DOLocalMoveY(question.transform.localPosition.y - 600f, 1f));
            _resetSequence.Append(answer.transform.DOLocalMoveX(answer.transform.localPosition.x - 1000f, 1f));
            _resetSequence.Join(keyboard.transform.DOLocalMoveY(keyboard.transform.localPosition.y + 600f, 1f));
        }

        public void PlayWarningAnim()
        {
            warningTMP.SetActive(true);
            _warningSequence = DOTween.Sequence();
            _warningSequence.Append(warningTMP.transform.DOScale(Vector3.one, 1f));
            _warningSequence.Append(warningTMP.transform.DOShakeScale(1f, 1f));
            _warningSequence.Join(warningTMP.transform.DOScale(Vector3.zero, 1f).OnComplete(() =>
            {
                warningTMP.SetActive(false);
            }));
        }
    }
}