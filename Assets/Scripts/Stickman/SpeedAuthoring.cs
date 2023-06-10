using UnityEngine;
using Unity.Entities;

namespace Stickman
{
    public class SpeedAuthoring : MonoBehaviour
    {
        public float Value;
        
        public class Baker : Baker<SpeedAuthoring>
        {
            public override void Bake(SpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Speed
                {
                    Value = authoring.Value
                });
            }
        }
    }

    public struct Speed : IComponentData
    {
        public float Value;
    }   
}
