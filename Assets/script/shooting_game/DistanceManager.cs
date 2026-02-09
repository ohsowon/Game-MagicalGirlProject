using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceManager : MonoBehaviour
{
    GameObject player;
    public GameObject boss;
    public Slider distanceGauge;
    public float interval = 7.5f;  // 보스와 플레이어 사이 간격
    float maxDistance;  // 게임 시작 시점에서 보스와 플레이어 사이 거리 (가장 멀리 있을 때)

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
        maxDistance = Mathf.Abs(player.transform.position.y - (boss.transform.position.y - interval));
    }

    // Update is called once per frame
    void Update()
    {
        if (boss != null)  // boss가 존재하면 아래 코드를 실행하고, 없으면 패스. 보스가 Destroy 되었을 때 에러 방지하기 위해 작성
        {
            float distance = Mathf.Abs(player.transform.position.y - (boss.transform.position.y - interval));
            float clampedDistance = Mathf.Clamp(distance, 0, maxDistance);
            distanceGauge.value = clampedDistance;
        }
        
    }
}
