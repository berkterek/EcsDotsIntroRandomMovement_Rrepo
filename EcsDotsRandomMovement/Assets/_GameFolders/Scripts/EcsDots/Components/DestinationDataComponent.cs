using Unity.Entities;
using Unity.Mathematics;

namespace EcsDostRandomMovement.EcsDots.Components
{
    public struct DestinationDataComponent : IComponentData
    {
        public float3 Destination;
    }
}

