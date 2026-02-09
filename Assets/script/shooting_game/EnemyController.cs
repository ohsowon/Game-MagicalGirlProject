using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;         // 이동 속도
    public float moveRange = 1f;         // 좌우 이동 범위

    private Vector3 startPosition;
    public int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 좌우 이동
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // 시작 지점에서 moveRange 거리만큼 이동하면 방향 전환
        if (Mathf.Abs(transform.position.x - startPosition.x) >= moveRange)
        {
            direction *= -1; // 방향 반전
        }
    }
}
