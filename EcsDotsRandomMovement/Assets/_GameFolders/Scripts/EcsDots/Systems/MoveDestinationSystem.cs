using EcsDostRandomMovement.EcsDots.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace EcsDostRandomMovement.EcsDots.Systems
{
    [BurstCompile]
    public partial struct MoveDestinationSystem : ISystem
    {
        float _extraTimeElapsed;
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            float elapsedTime = (float)SystemAPI.Time.ElapsedTime;
            
            _extraTimeElapsed += elapsedTime * deltaTime * 1000f;
            
            new MoveDestinationJob()
            {
                DeltaTime = deltaTime,
                ElapsedTime = _extraTimeElapsed
            }.ScheduleParallel();
        }
    }

    [BurstCompile]
    public partial struct MoveDestinationJob : IJobEntity
    {
        public float DeltaTime;
        public float ElapsedTime;
        
        private void Execute(MoveToDestinationAspect moveToDestinationAspect)
        {
            moveToDestinationAspect.MoveDestination(DeltaTime, ElapsedTime);
        }
    }
}