using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class IntellFindPath : MonoBehaviour
{
    public Vector3 nextPosition;
    public float speed;
    public float nextWaypointDistance;
    public bool reachPathEnd;

    private Seeker seek;
    private Path path;
    private int currentWayPoint;
    private Vector2 lastTarget;

    void Start()
    {
        seek = GetComponent<Seeker>();
    }

    // 更新路径后的回调函数
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // 更新路径
    public void UpdatePath(Vector2 target)
    {
        if (Vector2.Distance(target, lastTarget) > 1)
        {
            lastTarget = target;
            seek.StartPath(transform.position, target, OnPathComplete);
        } 
    }

    // 朝下一个点移动
    public void moveTo()
    {
        if (path == null) return;
        reachPathEnd = false;
        float distanceTowayPoint;
        while(true)
        {
            distanceTowayPoint = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
            if (distanceTowayPoint < nextWaypointDistance)
            {
                if (currentWayPoint < path.vectorPath.Count - 1)
                    currentWayPoint++;
                else
                {
                    reachPathEnd = true;
                    break;
                }
            }
            else break;
        }
        var speedFactor = reachPathEnd ? Mathf.Sqrt(distanceTowayPoint / nextWaypointDistance) : 1.0f;
        if (!reachPathEnd)
        {
            nextPosition = path.vectorPath[currentWayPoint];
            Vector3 direction = (nextPosition - transform.position).normalized;
            transform.position += speed * speedFactor * direction * Time.deltaTime;
        }
        else path = null;
    }
}
