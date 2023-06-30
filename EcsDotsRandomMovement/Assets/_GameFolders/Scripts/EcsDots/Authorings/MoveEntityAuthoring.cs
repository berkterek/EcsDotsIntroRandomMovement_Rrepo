using System;
using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using UnityEngine;

namespace EcsDostRandomMovement.EcsDots.Authorings
{
    public class MoveEntityAuthoring : MonoBehaviour
    {
        public float MoveSpeed = 5f;
        public Vector3 Destination;
    }

    public class MoveEntityBaker : Baker<MoveEntityAuthoring>
    {
        [Obsolete("Obsolete")]
        public override void Bake(MoveEntityAuthoring authoring)
        {
            AddComponent(new DestinationDataComponent{Destination = authoring.Destination});
            AddComponent(new MovementSpeedDataComponent { MoveSpeed = authoring.MoveSpeed });
            AddComponent<PersonTagDataComponent>();
        }
    }
}

