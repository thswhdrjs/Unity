using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviTest : MonoBehaviour
{
    // ���� ã�Ƽ� �̵��� ������Ʈ
    NavMeshAgent agent;

    // ������Ʈ�� ������
    [SerializeField]
    Transform target;

    private void Awake()
    {
        // ������ ���۵Ǹ� ���� ������Ʈ�� ������ NavMeshAgent ������Ʈ�� �����ͼ� ����
        agent = GetComponent<NavMeshAgent>() == null ?  gameObject.AddComponent<NavMeshAgent>() : GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // �����̽� Ű�� ������ Target�� ��ġ���� �̵��ϴ� ��θ� ����ؼ� �̵�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ������Ʈ���� �������� �˷��ִ� �Լ�
            agent.SetDestination(target.position);
        }
    }
}
