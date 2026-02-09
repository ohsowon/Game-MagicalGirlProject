using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotLauncher : MonoBehaviour
{
    public GameObject shotPrefab;
    public float launchSpeed = 5f;
    public float lifeTime = 3f;
    public float span = 1f;
    float delta = 0;

    Camera mainCamera;
    bool isVisible = false;


    // Start is called before the first frame update
    void Start()
    {
       mainCamera = Camera.main;
    }

    void Shoot()  // enemy의 위치에서 공격 발사
    {
        GameObject shot = Instantiate(shotPrefab);

        Vector3 enemyPos = transform.position;
        shot.transform.position = new Vector3(enemyPos.x, enemyPos.y - 1, enemyPos.z);

        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        rb.velocity = -shot.transform.up * launchSpeed;

        Destroy(shot, lifeTime);
    }

    bool IsVisibleByCamera()  // 오브젝트가 카메라 범위에 들어왔는지 판단
    {
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        return viewportPos.z > 0 &&
               viewportPos.x >= 0 && viewportPos.x <= 1 &&
               viewportPos.y >= 0 && viewportPos.y <= 1;
    }

    void FixedUpdate()
    {
        isVisible = IsVisibleByCamera();

        if (isVisible)                 // enemy가 카메라 범위에 들어왔다면,
        {
            delta += Time.deltaTime;

            if (delta > span)         // 일정 시간(span)마다 공격  
            {
                Shoot();
                delta = 0;
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
