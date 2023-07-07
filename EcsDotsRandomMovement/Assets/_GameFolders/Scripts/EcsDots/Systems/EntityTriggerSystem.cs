using EcsDostRandomMovement.EcsDots.Components;
using Unity.Entities;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine;

namespace EcsDostRandomMovement.EcsDots.Systems
{
    //[UpdateInGroup(typeof(PhysicsDisplayDebugGroup))]
    public partial struct EntityTriggerSystem : ISystem
    {
        struct ComponentDataHandle
        {
            public ComponentLookup<URPMaterialPropertyBaseColor> UrpMaterialBaseColorData;
            public ComponentLookup<PersonTagDataComponent> PersonTagComponentData;

            public ComponentDataHandle(ref SystemState systemState)
            {
                UrpMaterialBaseColorData = systemState.GetComponentLookup<URPMaterialPropertyBaseColor>(false);
                PersonTagComponentData = systemState.GetComponentLookup<PersonTagDataComponent>(true);
            }

            public void Update(ref SystemState systemState)
            {
                UrpMaterialBaseColorData.Update(ref systemState);
                PersonTagComponentData.Update(ref systemState);
            }
        }

        ComponentDataHandle _componentDataHandle;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<URPMaterialPropertyBaseColor>();
            state.RequireForUpdate<PersonTagDataComponent>();
            _componentDataHandle = new ComponentDataHandle(ref state);
        }

        public void OnUpdate(ref SystemState state)
        {
            _componentDataHandle.Update(ref state);

            new EntityTriggerJob()
            {
                UrpMaterialPropertyBaseColorData = _componentDataHandle.UrpMaterialBaseColorData,
                PersonTagComponentData = _componentDataHandle.PersonTagComponentData
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(),state.Dependency);
        }
    }
    
    public struct EntityTriggerJob : ITriggerEventsJobBase
    {
        public ComponentLookup<URPMaterialPropertyBaseColor> UrpMaterialPropertyBaseColorData;
        public ComponentLookup<PersonTagDataComponent> PersonTagComponentData;

        public void Execute(TriggerEvent triggerEvent)
        {
            var entityA = triggerEvent.EntityA;
            Debug.Log(entityA.Index);
        }
    }
}