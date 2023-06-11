using UnityEngine;
using Unity.Entities;

namespace Stickman
{
    public class BlueTeamAuthority : MonoBehaviour
    {
        public class Baker : Baker<BlueTeamAuthority>
        {
            public override void Bake(BlueTeamAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BlueTeamTag());
            }
        }
    }
    
    public struct BlueTeamTag : IComponentData { }
}