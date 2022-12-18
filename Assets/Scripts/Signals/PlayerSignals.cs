using System;
using Extentions;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<float>  onSetPlayerPos = delegate { };
        public UnityAction<OpponentAIManager> onSubscribeOpponentMediator = delegate {  };
        public UnityAction<OpponentAIManager> onUnsubscribeOpponentMediator = delegate {  };
        public Func<ushort> onGetAICount = delegate { return 0;};
        public UnityAction<short> onSetPlayerNewPos = delegate {  }; 
    }
}