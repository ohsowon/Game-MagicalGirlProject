using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotLauncher : MonoBehaviour
{
    public GameObject shotPrefab;
    public float launchSpeed = 10f;
    public float lifeTime = 1f;

    GameObject player;

    AudioSource aud;
    public AudioClip shotSE;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("player");
        aud = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        aud.PlayOneShot(shotSE);
        GameObject shot = Instantiate(shotPrefab);

        Vector3 playerPos = this.player.transform.position;
        shot.transform.position = new Vector3(playerPos.x, playerPos.y + 1, playerPos.z);

        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.velocity = shot.transform.up * launchSpeed;

        Destroy(shot, lifeTime);
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // pc에서 조작이랑 공격 동시에 하기 불편해서 만들어둠
        {
            Shoot();
        }
    }
}
