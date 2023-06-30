using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDostRandomMovement.EcsDots.Aspects
{
    public readonly partial struct MoveToDestinationAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRW<LocalTransform> LocalTransformRW;
        readonly RefRO<PersonTagDataComponent> PersonTagDataRO;
        readonly RefRO<MovementSpeedDataComponent> MoveSpeedDataRO;
        readonly RefRW<DestinationDataComponent> DestinationDataRW;

        public PersonTagDataComponent PersonTagData => PersonTagDataRO.ValueRO;

        public void MoveDestination(float deltaTime)
        {
            if (math.all(DestinationDataRW.ValueRO.Destination == LocalTransformRW.ValueRO.Position)) return;
            
            float3 toDestination = DestinationDataRW.ValueRO.Destination - LocalTransformRW.ValueRO.Position;

            LocalTransformRW.ValueRW.Rotation = quaternion.LookRotation(toDestination, new float3(0f, 1f, 0f));

            float3 movement = deltaTime * MoveSpeedDataRO.ValueRO.MoveSpeed * math.normalize(toDestination);
            if (math.length(movement) >= math.length(toDestination))
            {
                LocalTransformRW.ValueRW.Position = DestinationDataRW.ValueRO.Destination;
            }
            else
            {
                LocalTransformRW.ValueRW.Position += movement;
            }
        }
    }
}