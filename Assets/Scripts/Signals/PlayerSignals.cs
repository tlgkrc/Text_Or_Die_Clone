using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<float>  onSetPlayerPos = delegate { };
        public UnityAction<float> onGetNewPlayerScale = delegate{  };
        public UnityAction onActivateImpactBomb = delegate {  };
        public UnityAction onActivatePlayerBomb = delegate {  };
    }
}