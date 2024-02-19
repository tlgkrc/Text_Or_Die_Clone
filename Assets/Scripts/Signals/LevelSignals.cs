using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onLevelFailed = delegate {  };
        public UnityAction onLevelSuccessful  =delegate {  };
        public UnityAction onRiseWaterLevel = delegate {  };
        public UnityAction<float> onCheckWaterLevel = delegate {  };
    }
}