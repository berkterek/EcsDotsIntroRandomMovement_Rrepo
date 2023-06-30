using EcsDostRandomMovement.EcsDots.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace EcsDostRandomMovement.EcsDots.Systems
{
    [BurstCompile]
    public partial struct MoveDestinationSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            
            new MoveDestinationJob()
            {
                DeltaTime = deltaTime
            }.Schedule();
        }
    }

    [BurstCompile]
    public partial struct MoveDestinationJob : IJobEntity
    {
        public float DeltaTime;
        
        private void Execute(MoveToDestinationAspect moveToDestinationAspect)
        {
            moveToDestinationAspect.MoveDestination(DeltaTime);
        }
    }
}