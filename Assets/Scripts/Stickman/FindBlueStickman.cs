using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace Stickman
{
    public partial class FindBlueStickman : SystemBase
    {
        private EntityQuery _blueTeamQuery;
        private EntityQuery _redTeamQuery;

        protected override void OnCreate()
        {
            _blueTeamQuery = GetEntityQuery(ComponentType.ReadOnly<BlueTeamTag>(), ComponentType.ReadOnly<LocalTransform>());
            _redTeamQuery = GetEntityQuery(ComponentType.ReadOnly<RedTeamTag>(), ComponentType.ReadOnly<TargetPosition>());
        }

        protected override void OnUpdate()
        {
            NativeArray<LocalTransform> blueTransforms = _blueTeamQuery.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

            var job = new FindClosestEnemyJob
            {
                TeamTransformTypeHandle = GetComponentTypeHandle<LocalTransform>(true),
                TeamTargetPositionTypeHandle = GetComponentTypeHandle<TargetPosition>(),
                EnemyTransforms = blueTransforms
            };
            
            var jobHandle = job.ScheduleParallel(_redTeamQuery, Dependency);
            
            jobHandle.Complete();
            blueTransforms.Dispose();
        }
    }   
}