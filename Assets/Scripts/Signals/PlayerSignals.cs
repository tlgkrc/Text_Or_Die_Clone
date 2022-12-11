using System;
using System.Collections.Generic;
using Extentions;
using Managers;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<float>  onSetPlayerPos = delegate { };
        public UnityAction<float> onGetNewPlayerScale = delegate{  };
        public UnityAction onActivateImpactBomb = delegate {  };
        public UnityAction onActivatePlayerBomb = delegate {  };
        public Func<ushort> onGetAICount = delegate { return 0;};
        public UnityAction<OpponentAIManager> onSubscribeOpponentMediator = delegate {  };
    }
}