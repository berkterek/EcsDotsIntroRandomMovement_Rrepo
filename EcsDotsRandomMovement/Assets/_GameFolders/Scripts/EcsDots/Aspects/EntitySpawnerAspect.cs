using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace EcsDostRandomMovement.EcsDots.Aspects
{
    public readonly partial struct EntitySpawnerAspect : IAspect
    {
        public readonly Entity Entity;
        readonly RefRO<EntitySpawnerDataComponent> EntitySpawnDataRO;

        public void SpawnEntity(EntityManager entityManager)
        {
            for (int x = 0; x < EntitySpawnDataRO.ValueRO.GridSize; x++)
            {
                for (int z = 0; z < EntitySpawnDataRO.ValueRO.GridSize; z++)
                {
                    var entity = entityManager.Instantiate(EntitySpawnDataRO.ValueRO.Prefab);
                    float spread = EntitySpawnDataRO.ValueRO.Spread;
                    
                    float3 position = new float3(x * spread, 0f, z * spread);
                    
                    entityManager.SetComponentData(entity, new LocalTransform()
                    {
                        Position = position,
                        Scale = 1f
                    });
                    
                    entityManager.SetComponentData(entity, new DestinationDataComponent
                    {
                        Destination = new float3(0f,0f,0f)
                    });
                    
                    entityManager.SetComponentData(entity, new MovementSpeedDataComponent()
                    {
                        MoveSpeed = Random.CreateFromIndex((uint)(z * x)).NextFloat(EntitySpawnDataRO.ValueRO.MinMoveSpeed,EntitySpawnDataRO.ValueRO.MaxMoveSpeed)
                    });
                }
            }
        }
    }
}