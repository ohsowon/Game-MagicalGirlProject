using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
    }

    void FixedUpdate()
    {
        Vector3 playerPos = this.player.transform.position;
        transform.position = new Vector3(transform.position.x, playerPos.y + 3.8f, transform.position.z);  // playerPos.y + 3.8f : 플레이어를 화면의 중앙보다 아래에 오도록 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
