using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Stickman
{
    public partial class FindRedStickman : SystemBase
    {
        private EntityQuery _blueTeamQuery;
        private EntityQuery _redTeamQuery;

        protected override void OnCreate()
        {
            _blueTeamQuery = GetEntityQuery(ComponentType.ReadOnly<BlueTeamTag>(), ComponentType.ReadOnly<TargetPosition>());
            _redTeamQuery = GetEntityQuery(ComponentType.ReadOnly<RedTeamTag>(), ComponentType.ReadOnly<LocalTransform>());
        }

        protected override void OnUpdate()
        {
            NativeArray<LocalTransform> redTransforms = _redTeamQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

            var job = new FindClosestEnemyJob
            {
                TeamTransformTypeHandle = GetComponentTypeHandle<LocalTransform>(true),
                TeamTargetPositionTypeHandle = GetComponentTypeHandle<TargetPosition>(),
                EnemyTransforms = redTransforms
            };
            
            var jobHandle = job.ScheduleParallel(_blueTeamQuery, Dependency);
            
            jobHandle.Complete();
            redTransforms.Dispose();
        }
    }   
}
