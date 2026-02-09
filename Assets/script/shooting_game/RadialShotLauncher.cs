using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialShotLauncher : MonoBehaviour
{
    public GameObject shotPrefab; 
    public int shotCount = 6;    
    public float launchSpeed = 3f;
    public float lifeTime = 3f;
    public float span = 3f;
    public float angle = 0f;
    float delta = 0;

    Camera mainCamera;
    bool isVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    void RadialShoot()
    {
        float angleStep = 360f / shotCount;

        for (int i = 0; i < shotCount; i++)
        {
            // 각도에 따른 방향 벡터 계산
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(dirX, dirY).normalized;

            // 총알 생성
            GameObject shot = Instantiate(shotPrefab, transform.position, Quaternion.identity);
            shot.GetComponent<Rigidbody2D>().velocity = direction * launchSpeed;

            // 총알이 이동 방향을 바라보도록 회전 설정
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            shot.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

            angle += angleStep;

            Destroy(shot, lifeTime);
        }
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
                RadialShoot();
                delta = 0;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
