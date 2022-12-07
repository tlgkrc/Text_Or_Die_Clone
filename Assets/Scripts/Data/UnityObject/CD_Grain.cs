using Data.ValueObject;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Grain", menuName = "Game/CD_Grain", order = 0)]
    public class CD_Grain : SerializedScriptableObject
    {
        public GrainData GrainData;
    }
}