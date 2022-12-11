using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel = delegate { };
        public UnityAction<UIPanels> onClosePanel = delegate { };
        public UnityAction<ushort> onSetScoreText = delegate { };
        public UnityAction<char> onAddCharToInputText = delegate{ };
        public UnityAction onDeleteInputText = delegate {  };
        public UnityAction onSubmitInputText = delegate {  };
        public UnityAction<string> onSetQuestionText = delegate {  };
        public UnityAction<string> onCheckAnswer = delegate {  };
        public UnityAction<string> onSetPlayerName = delegate {  };
    }
}