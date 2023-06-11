using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace Stickman
{
    public partial class EnemyFindSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .WithAll<BlueTeamTag>()
                .ForEach((ref LocalTransform transform, ref TargetPosition targetPosition) =>
                {
                    float minDistance = 99999;
                    float2 closestPosition = float2.zero;
                    Entity closestEnemy = Entity.Null;
                    float2 position = transform.Position.xy;
                    
                    Entities
                        .WithAll<RedTeamTag>()
                        .ForEach((Entity targetEntity, ref LocalTransform enemyTransform) =>
                        {
                            if (closestEnemy == Entity.Null)
                            {
                                closestEnemy = targetEntity;
                                minDistance = math.distance(position, enemyTransform.Position.xy);
                                closestPosition = enemyTransform.Position.xy;
                            }
                            else
                            {
                                float distance = math.distance(position, enemyTransform.Position.xy);
                                
                                if (distance < minDistance)
                                {
                                    closestEnemy = targetEntity;
                                    minDistance = distance;
                                    closestPosition = enemyTransform.Position.xy;
                                }
                            }
                        }).ScheduleParallel();

                    if (closestEnemy != Entity.Null)
                    {
                        targetPosition.Position = closestPosition;
                    }
                    
                }).ScheduleParallel();

        }
    }   
}
