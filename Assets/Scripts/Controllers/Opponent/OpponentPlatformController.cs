using System;
using System.Threading.Tasks;
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

        #endregion

        #endregion

        public void WriteTrueAnswerToPlatforms(string answer)
        {
            manager.RiseOpponent(answer.Length + answer.Length* 0.05f,1.5f*answer.Length);
            for (var i = answer.Length-1; i >=0; i--)
            {
                var stairGameObject = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Stair.ToString(),transform);
                if (stairGameObject == null) continue;
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
        }
    }
}