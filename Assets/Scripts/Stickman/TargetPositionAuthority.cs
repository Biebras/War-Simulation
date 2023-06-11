using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Stickman
{
    public class TargetPositionAuthority : MonoBehaviour
    {
        public float2 Position;
        
        public class Baker : Baker<TargetPositionAuthority>
        {
            public override void Bake(TargetPositionAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new TargetPosition
                {
                    Position = authoring.Position
                });
            }
        }
    }
    
    public struct TargetPosition : IComponentData
    {
        public float2 Position;
    }
}