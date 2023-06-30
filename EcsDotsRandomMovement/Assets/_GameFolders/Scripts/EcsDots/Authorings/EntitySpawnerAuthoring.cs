using System;
using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsDostRandomMovement.EcsDots.Authorings
{
    public class EntitySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public int GridSize;
        public int Spread;
    }

    public class EntitySpawnerBaker : Baker<EntitySpawnerAuthoring>
    {
        [Obsolete("Obsolete")]
        public override void Bake(EntitySpawnerAuthoring authoring)
        {
            AddComponent(new EntitySpawnerDataComponent()
            {
                Spread = authoring.Spread,
                Prefab = GetEntity(authoring.Prefab),
                GridSize = authoring.GridSize
            });
        }
    }
}