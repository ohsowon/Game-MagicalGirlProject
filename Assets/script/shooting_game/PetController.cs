using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetController : MonoBehaviour
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
        transform.position = new Vector3(playerPos.x + 1, playerPos.y - 0.3f, transform.position.z);  // 플레이어보다 왼쪽 아래에서 플레이어를 따라 이동 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
