using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveAgent : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int nextIdx;

    private readonly float patrolSpeed = 1.5f;
    private readonly float traceSpeed = 4.0f;

    private NavMeshAgent agent;

    private bool _patrolling;
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrolSpeed;
                MoveWayPoint();
            }
        }
    }

    private Vector3 _traceTarget;
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var pObj = new GameObject();
        pObj.name = "wayPointGroup"; // 부모격 오브젝트 하나 생성
        for (int c = 0; c < 5; c++) // 자식 오브젝트 반복 생성
        {
            var newObj = new GameObject();
            newObj.name = "Point"; //이름을 point로 설정
            newObj.transform.parent = pObj.transform;
            newObj.transform.position = new Vector3(Random.Range(-23.0f, 23.0f), 0, Random.Range(-23.0f, 23.0f));//랜덤 위치로 이동
        }

        var group = GameObject.Find("wayPointGroup");
        if(group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoints);
            wayPoints.RemoveAt(0);
        }

        MoveWayPoint();
    }

    void MoveWayPoint()
    {
        if (agent.isPathStale) return;
        agent.destination = wayPoints[nextIdx].position;
        agent.isStopped = false;
    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;

        agent.destination = pos;
        agent.isStopped = false;
    }

    public void Stop() {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_patrolling) return;

        if(agent.velocity.sqrMagnitude >= 0.2f * 0.2f
            && agent.remainingDistance <= 0.5f){
            nextIdx = ++nextIdx % wayPoints.Count;
            MoveWayPoint();
        }
    }
}
