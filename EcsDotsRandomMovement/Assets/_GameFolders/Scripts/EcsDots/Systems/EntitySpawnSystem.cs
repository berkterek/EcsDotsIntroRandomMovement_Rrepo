using EcsDostRandomMovement.EcsDots.Aspects;
using EcsDostRandomMovement.EcsDots.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace EcsDostRandomMovement.EcsDots.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct EntitySpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EntitySpawnerDataComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var entity = SystemAPI.GetSingletonEntity<EntitySpawnerDataComponent>();
            var entitySpawnAspect = SystemAPI.GetAspect<EntitySpawnerAspect>(entity);
            
            entitySpawnAspect.SpawnEntity(state.EntityManager);
        }
    }
}