using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Stickman
{
    [BurstCompile]
    public struct FindClosestEnemyJob : IJobChunk
    {
        [ReadOnly] 
        public ComponentTypeHandle<LocalTransform> TeamTransformTypeHandle;
        public ComponentTypeHandle<TargetPosition> TeamTargetPositionTypeHandle;
        [ReadOnly] 
        public NativeArray<LocalTransform> EnemyTransforms;
            
        public void Execute(in ArchetypeChunk chunk, int unfilteredChunkIndex, bool useEnabledMask, in v128 chunkEnabledMask)
        {
            NativeArray<LocalTransform> teamTransforms = chunk.GetNativeArray(ref TeamTransformTypeHandle);
            NativeArray<TargetPosition> teamTargetPositions = chunk.GetNativeArray(ref TeamTargetPositionTypeHandle);

            for (int i = 0; i < chunk.Count; i++)
            {
                float minDistance = 99999;
                float2 closestPosition = float2.zero;
                    
                for (int j = 0; j < EnemyTransforms.Length; j++)
                {
                    float distance = math.distance(teamTransforms[i].Position.xy, EnemyTransforms[j].Position.xy);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestPosition = EnemyTransforms[j].Position.xy;
                    }
                }

                var teamTargetPosition = teamTargetPositions[i];
                teamTargetPosition.Position = closestPosition;
                teamTargetPositions[i] = teamTargetPosition;
            }
        }
    }
}