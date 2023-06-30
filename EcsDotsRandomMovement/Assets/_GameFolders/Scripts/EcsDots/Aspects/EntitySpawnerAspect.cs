using EcsDostRandomMovement.EcsDots.Components;
using EcsDostRandomMovement.EcsDots.Systems;
using Unity.Collections;
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
                    entityManager.SetComponentData(entity, new LocalTransform()
                    {
                        Position = new float3(x,0f,z),
                        Scale = 1f
                    });
                }
            }
        }
    }
}