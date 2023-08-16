using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;
using Unity.Mathematics;

public struct LevelComonent : IComponentData
{
    public float level;
}

public struct MoveSpeedComponent : IComponentData
{
    public float moveSpeed;
}

public class ECSSystem : MonoBehaviour
{
    [SerializeField] private Mesh _mesh;
    [SerializeField] private Material _material;

    private void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype
        (
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(MoveSpeedComponent)
        );
        NativeArray<Entity> entityArray = new NativeArray<Entity>(25000, Allocator.TempJob);
        entityManager.CreateEntity(entityArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            RenderMeshUtility.AddComponents(entityArray[i], entityManager, new RenderMeshDescription
            (
                mesh: _mesh,
                material: _material
            ));
            entityManager.SetComponentData(entityArray[i], new MoveSpeedComponent
            {
                moveSpeed = UnityEngine.Random.Range(1f, 2f)
            });
            entityManager.SetComponentData(entityArray[i], new Translation
            {
                Value = new float3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-5f, 5f), 0)
            });
        }

        entityArray.Dispose();
    }
}

public class LevelUpSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref LevelComonent levelComponent) =>
        {
            levelComponent.level += 1f * Time.DeltaTime;
        });
    }
}

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
        {
            translation.Value.y += moveSpeedComponent.moveSpeed * Time.DeltaTime;

            if (translation.Value.y > 5f)
                moveSpeedComponent.moveSpeed = -math.abs(moveSpeedComponent.moveSpeed);

            if (translation.Value.y < -5f)
                moveSpeedComponent.moveSpeed = math.abs(moveSpeedComponent.moveSpeed);
        });
    }
}