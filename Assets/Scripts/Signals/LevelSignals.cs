using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onNextTournament = delegate {  }; 
        public UnityAction onLevelFailed = delegate {  };
        public UnityAction onLevelSuccesfull  =delegate {  };
        public UnityAction onRiseWaterLevel = delegate {  };
    }
}