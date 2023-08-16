using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Burst;

public class TestObject
{
    public Transform transform;
    public float moveY;
}

public class JobSystem : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<TestObject> obj;

    [SerializeField] private int order;

    private void Start()
    {
        obj = new List<TestObject>();
        GameObject TestParent = new GameObject("TestParent");

        for (int i = 0; i < 25000; i++)
        {
            Transform tr = Instantiate(prefab, new Vector3(UnityEngine.Random.Range(-8f, 8f), UnityEngine.Random.Range(-5f, 5f)), quaternion.identity).transform;
            tr.parent = TestParent.transform;
            obj.Add(new TestObject
            { 
                transform = tr,
                moveY = UnityEngine.Random.Range(1f, 2f)
        });
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            order++;

        if(order % 3 == 0)
        {
            #region IJobParallelFor

            NativeArray<float3> positionArray = new NativeArray<float3>(obj.Count, Allocator.TempJob);
            NativeArray<float> moveYArray = new NativeArray<float>(obj.Count, Allocator.TempJob);

            for (int i = 0; i < obj.Count; i++)
            {
                positionArray[i] = obj[i].transform.position;
                moveYArray[i] = obj[i].moveY;
            }

            IJobParallelForJob testJob = new IJobParallelForJob
            {
                positonArray = positionArray,
                moveYArray = moveYArray,
                deltaTime = Time.deltaTime
            };
            JobHandle handle = testJob.Schedule(obj.Count, 5);
            handle.Complete();

            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].transform.position = positionArray[i];
                obj[i].moveY = moveYArray[i];
            }

            positionArray.Dispose();
            moveYArray.Dispose();

            #endregion
        }

        if(order % 3 == 1)
        {
            #region IJobParallelForTransform

            NativeArray<float> moveYArray2 = new NativeArray<float>(obj.Count, Allocator.TempJob);
            TransformAccessArray transformAccessArray = new TransformAccessArray(obj.Count);

            for (int i = 0; i < obj.Count; i++)
            {
                moveYArray2[i] = obj[i].moveY;
                transformAccessArray.Add(obj[i].transform);
            }

            IJobParallelForTransformJob testJob2 = new IJobParallelForTransformJob
            {
                moveYArray = moveYArray2,
                deltaTime = Time.deltaTime
            };
            JobHandle handle2 = testJob2.Schedule(transformAccessArray);
            handle2.Complete();

            for (int i = 0; i < obj.Count; i++)
                obj[i].moveY = moveYArray2[i];

            moveYArray2.Dispose();
            transformAccessArray.Dispose();

            #endregion
        }

        if(order % 3 == 2)
        {
            foreach (TestObject testObj in obj)
            {
                testObj.transform.position += new Vector3(0, testObj.moveY * Time.deltaTime);

                if (testObj.transform.position.y > 5f)
                    testObj.moveY = -math.abs(testObj.moveY);

                if (testObj.transform.position.y < -5f)
                    testObj.moveY = math.abs(testObj.moveY);
            }
        }
    }

    [BurstCompile]
    public struct IJobParallelForJob : IJobParallelFor
    {
        public NativeArray<float3> positonArray;
        public NativeArray<float> moveYArray;

        [ReadOnly] public float deltaTime;

        public void Execute(int index)
        {
            positonArray[index] += new float3(0f, moveYArray[index] * deltaTime, 0f);

            if (positonArray[index].y > 5f)
                moveYArray[index] = -math.abs(moveYArray[index]);

            if (positonArray[index].y < -5f)
                moveYArray[index] = math.abs(moveYArray[index]);
        }
    }

    [BurstCompile]
    public struct IJobParallelForTransformJob : IJobParallelForTransform
    {
        public NativeArray<float> moveYArray;

        [ReadOnly] public float deltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            transform.position += new Vector3(0f, moveYArray[index] * deltaTime, 0f);

            if (transform.position.y > 5f)
                moveYArray[index] = -math.abs(moveYArray[index]);

            if (transform.position.y < -5f)
                moveYArray[index] = math.abs(moveYArray[index]);
        }
    }
}
