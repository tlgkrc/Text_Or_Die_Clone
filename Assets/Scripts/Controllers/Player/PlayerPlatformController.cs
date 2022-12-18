using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerPlatformController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;

        #endregion

        #region Private Variables

        private Vector3 _lastPos;
        private Sequence _stairSequence;
        private List<GameObject> _stairList = new List<GameObject>();

        #endregion

        #endregion

        private void Start()
        {
            AddDefaultPlatform();
        }

        public void WriteTrueAnswerToPlatforms(string answer)
        {
            manager.RisePlayer(answer.Length + answer.Length*0.05f);
            for (var i = answer.Length-1; i >=0; i--)
            {
                var stairGameObject = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Stair.ToString(),transform);
                if (stairGameObject == null) continue;
                _stairList.Add(stairGameObject);
                stairGameObject.transform.localScale = Vector3.zero;
                stairGameObject.transform.position = manager.transform.position;
                SendStairSignalAsync(stairGameObject,answer[i]);
            }
        }

        private async void  SendStairSignalAsync(GameObject stair,char letter)
        {
            SetStairPos(stair);
            await Task.Delay(1500);
            QASignals.Instance.onWriteLetterToStair?.Invoke(stair.gameObject.GetInstanceID(),letter);
        }

        private void AddDefaultPlatform()
        {
            _lastPos = manager.transform.position;
            for (var i = 0; i < 4; i++)
            {
                var stairGameObject = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Stair.ToString(),transform);
                if (stairGameObject != null) SetDefaultStairPos(stairGameObject,i);
            }
        }

        private void SetStairPos(GameObject stair)
        {

            _lastPos += Vector3.up  +new Vector3(0, 0.05f, 0);
            _stairSequence = DOTween.Sequence();
            _stairSequence.Append(stair.transform.DOMoveY(_lastPos.y, .5f));
            _stairSequence.Join(stair.transform.DOScale(Vector3.one, .5f));
        }

        private void SetDefaultStairPos(GameObject stair, int indexOfStair)
        {
            stair.transform.localPosition = Vector3.down * indexOfStair * stair.transform.localScale.y +new Vector3(0, 0.05f, 0) ;
            _stairList.Add(stair);
        }

        public void MovePlatform(short distance)
        {
            foreach (var stair in _stairList)
            {
                stair.transform.DOMoveX(stair.transform.position.x + distance, .5f);
                _lastPos.x += distance;
            }
        }
    }
}