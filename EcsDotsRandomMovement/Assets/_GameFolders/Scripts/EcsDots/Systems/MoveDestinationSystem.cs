using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDostRandomMovement.EcsDots.Systems
{
    [BurstCompile]
    public partial struct MoveDestinationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            foreach (var localTransform in SystemAPI.Query<RefRW<LocalTransform>>())
            {
                float3 direction = deltaTime * new float3(0f, 0f, 1f);
                localTransform.ValueRW.Position += direction;
            }
        }
    }
}