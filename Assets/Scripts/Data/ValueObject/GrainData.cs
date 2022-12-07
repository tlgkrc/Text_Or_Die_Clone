using System;
using Enums;
using UnityEngine.Rendering;

namespace Data.ValueObject
{
    [Serializable]
    public class GrainData
    {
        public SerializedDictionary<PoolTypes, GrainGOData> GrainDatas;
    }
}