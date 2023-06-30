using Unity.Entities;

namespace EcsDostRandomMovement.EcsDots.Components
{
    public struct EntitySpawnerDataComponent : IComponentData
    {
        public Entity Prefab;
        public int GridSize;
        public int Spread;
    }
}