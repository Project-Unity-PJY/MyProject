using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Transform[] pathPoints; // 경로의 포인트들
    public float speed = 2f; // 이동 속도
    private int currentPointIndex = 0;

    void Update()
    {
        if (pathPoints.Length == 0)
            return;

        Transform targetPoint = pathPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
        }
    }
}
