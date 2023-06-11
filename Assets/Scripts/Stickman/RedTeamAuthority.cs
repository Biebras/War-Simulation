using UnityEngine;
using Unity.Entities;

namespace Stickman
{
    public class RedTeamAuthority : MonoBehaviour
    {
        public class Baker : Baker<RedTeamAuthority>
        {
            public override void Bake(RedTeamAuthority authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RedTeamTag());
            }
        }
    }

    public struct RedTeamTag : IComponentData { }
}