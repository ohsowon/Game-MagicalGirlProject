using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeedX = 3f;
    public float moveSpeedY = 1f;
    public float interval = 7.5f;  // 보스와 플레이어 사이 간격
    float MaxHeight;               // 보스가 화면에 나타났을 때 플레이어의 y좌표
    public float minX = -4.8f;     // 게임 화면 밖으로 이동하지 못하도록 제한
    public float maxX = 4.1f;

    public GameObject boss;
    public Joystick joystick;
    private Rigidbody2D rb;

    public float spaceTargetY = 120f;//백스페이스이동시 순간이동

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MaxHeight = boss.transform.position.y - interval;
    }

    void FixedUpdate()
    {
        if (transform.position.y > MaxHeight)  // 보스가 화면의 상단에 위치하면 y축 이동 멈춤
        {
            moveSpeedY = 0;
        }

        Vector2 move = new Vector2(joystick.Horizontal, moveSpeedY);
        rb.velocity = move * moveSpeedX;

        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        transform.position = pos;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("enemy shot") || collision.CompareTag("satellite"))  // 적, 적의 공격, 위성에 닿으면 게임 오버
        {
            string CurrentScene = SceneManager.GetActiveScene().name;
            if (CurrentScene == "Stage2Scene")
            {
                SceneManager.LoadScene("2gameover");
            }
            if (CurrentScene == "Stage3Scene")
            {
                SceneManager.LoadScene("3gameover");
            }
        }

        if (collision.CompareTag("point"))  // 포인트에 닿으면 점수가 1씩 증가
        {
            ScoreManager.instance.AddScore();
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // --- 추가: 백스페이스클릭시순간이동
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Vector3 pos = transform.position;
            pos.y = spaceTargetY;  // 특정 y좌표로 이동
            transform.position = pos;

            // Rigidbody 속도 초기화 (y축 이동 멈춤)
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }
}