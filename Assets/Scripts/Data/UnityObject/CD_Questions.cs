using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Questions", menuName = "Game/CD_Questions", order = 0)]
    public class CD_Questions : ScriptableObject
    {
        public QuestionsData QuestionsDatas;
    }
}