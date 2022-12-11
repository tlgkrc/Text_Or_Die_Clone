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
        public UnityAction<OpponentAIManager> onSubscribeOpponentMediator = delegate {  };
        public Func<ushort> onGetAICount = delegate { return 0;};
    }
}