using System.Threading.Tasks;
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

        #endregion

        #endregion

        private void Start()
        {
            AddDefaultPlatform();
        }

        public void WriteTrueAnswerToPlatforms(string answer)
        {
            manager.RisePlayer(answer.Length);
            for (var i = answer.Length-1; i >=0; i--)
            {
                var stairGameObject = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolTypes.Stair.ToString(),transform);
                if (stairGameObject == null) continue;
                stairGameObject.transform.position = manager.transform.position;
                SetStairPos(stairGameObject);
                SendStairSignalAsync(stairGameObject,answer[i]);
            }
        }

        private async void  SendStairSignalAsync(GameObject stair,char letter)
        {
            await Task.Yield();
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

            _lastPos += Vector3.up * stair.transform.localScale.z +new Vector3(0, 0.05f, 0);
            stair.transform.position = _lastPos ;
        }

        private void SetDefaultStairPos(GameObject stair, int indexOfStair)
        {
            stair.transform.position = Vector3.down * indexOfStair * stair.gameObject.transform.localScale.z +new Vector3(0, 0.05f, 0) ;
        }
    }
}