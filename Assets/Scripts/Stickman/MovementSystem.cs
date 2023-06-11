using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Stickman
{
    public partial struct MovementSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, target, speed) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<TargetPosition>, RefRO<Speed>>())
            {
                float2 direction = target.ValueRO.Position - transform.ValueRO.Position.xy;
                float2 velocity = math.normalize(direction) * speed.ValueRO.Value * SystemAPI.Time.DeltaTime;
                transform.ValueRW.Position.xy += velocity;
            }
        }
    }
}
