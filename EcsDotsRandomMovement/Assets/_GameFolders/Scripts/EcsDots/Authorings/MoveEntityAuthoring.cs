using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsDostRandomMovement.EcsDots.Authorings
{
    public class MoveEntityAuthoring : MonoBehaviour
    {
        
    }

    public class MoveEntityBaker : Baker<MoveEntityAuthoring>
    {
        [System.Obsolete("Obsolete")]
        public override void Bake(MoveEntityAuthoring authoring)
        {
            AddComponent<DestinationDataComponent>();
            AddComponent<MovementSpeedDataComponent>();
            AddComponent<PersonTagDataComponent>();
            AddComponent<RandomIncreaseDataComponent>();
        }
    }
}

