using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Stickman
{
    public class TargetAuthority : MonoBehaviour
    {
        public float2 Position;
        
        public class Baker : Baker<TargetAuthority>
        {
            public override void Bake(TargetAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new Target
                {
                    Position = authoring.Position
                });
            }
        }
    }
    
    public struct Target : IComponentData
    {
        public float2 Position;
    }
}