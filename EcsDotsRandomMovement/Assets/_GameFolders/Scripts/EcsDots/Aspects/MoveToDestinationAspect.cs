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
        readonly RefRW<RandomIncreaseDataComponent> RandomIncraseRW;

        public PersonTagDataComponent PersonTagData => PersonTagDataRO.ValueRO;

        public void MoveDestination(float deltaTime, float elapsedTime)
        {
            if (math.all(DestinationDataRW.ValueRO.Destination == LocalTransformRW.ValueRO.Position))
            {
                elapsedTime = math.abs(Entity.Index + elapsedTime);
                uint roundValue = (uint)math.round(elapsedTime);
                RandomIncraseRW.ValueRW.Value += roundValue;
                uint seed = RandomIncraseRW.ValueRO.Value;
                seed += RandomIncraseRW.ValueRO.Value % 2 == 0 ? (uint)10 : 20;
                float x = Random.CreateFromIndex(seed).NextFloat(0f, 500f);
                seed += RandomIncraseRW.ValueRO.Value % 2 == 0 ? (uint)100 : 200;
                float z = Random.CreateFromIndex(seed).NextFloat(0, 500f);
                DestinationDataRW.ValueRW.Destination = new float3(x, 0f, z);
            }
            
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