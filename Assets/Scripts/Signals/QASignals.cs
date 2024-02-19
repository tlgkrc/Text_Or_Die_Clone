using System;
using System.Collections.Generic;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class QASignals: MonoSingleton<QASignals>
    {
        public UnityAction onNextQuestion = delegate {  };
        public UnityAction<string> onWriteTrueAnswer =delegate {  };
        public UnityAction<int,char,Color32> onWriteLetterToStair  =delegate {  };
        public UnityAction<List<string>> onDistributeAIAnswers = delegate {  };
        public Func<float> onGetQuestionIndex = delegate { return 1; };
    }
}