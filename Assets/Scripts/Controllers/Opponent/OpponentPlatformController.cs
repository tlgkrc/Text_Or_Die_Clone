using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers.Opponent
{
    public class OpponentPlatformController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private OpponentAIManager manager;

        #endregion

        #region Private Variables

        private Vector3 _lastPos;
        private List<GameObject> _stairList = new List<GameObject>();

        #endregion

        #endregion

        public void WriteTrueAnswerToPlatforms(string answer)
        {
            manager.RiseOpponent(answer.Length + answer.Length* 0.05f);
            var newColor = new Color(Random.value, Random.value, Random.value);
            for (var i = answer.Length-1; i >=0; i--)
            {
                var stairGameObject = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Stair.ToString(),transform);
                if (stairGameObject == null) continue;
                _stairList.Add(stairGameObject);
                stairGameObject.transform.position = manager.transform.position;
                SendStairSignalAsync(stairGameObject,answer[i],newColor);
            }
        }

        private async void  SendStairSignalAsync(GameObject stair,char letter,Color32 color)
        {
            SetStairPos(stair);
            await Task.Delay(1600);
            QASignals.Instance.onWriteLetterToStair?.Invoke(stair.gameObject.GetInstanceID(),letter,color);
        }

        public void AddDefaultPlatform()
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

            _lastPos += Vector3.up * stair.transform.localScale.z + new Vector3(0, 0.05f, 0);
            stair.transform.position = _lastPos;
        }

        private void SetDefaultStairPos(GameObject stair, int indexOfStair)
        {
            stair.transform.position = manager.transform.position + Vector3.down * indexOfStair * stair.gameObject.transform.localScale.z +
                                       new Vector3(0, 0.05f, 0) ;
            _stairList.Add(stair);
        }

        public void MovePlatform(short distance)
        {
            foreach (var stair in _stairList)
            {
                stair.transform.DOMoveX(stair.transform.position.x + distance, .5f);
            }

            _lastPos += new Vector3(distance, 0, 0);
        }

        public void DeactivatePlatform()
        {
            foreach (var stair in _stairList)
            {
                PoolSignals.Instance.onReleasePoolObject?.Invoke(PoolTypes.Stair.ToString(),stair);
            }
        }
    }
}