using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Stage1 : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MaxHeight = boss.transform.position.y - interval;
    }

    void FixedUpdate()
    {
        if (TutorialManager.instance != null && TutorialManager.instance.isTutorial == true)  // 튜토리얼 중에는 y축 이동 멈춤
        {
            moveSpeedY = 0f;
        }

        if (TutorialManager.instance != null && TutorialManager.instance.isTutorial == false)  // 튜토리얼 끝나면 y축 이동 시작
        {
            moveSpeedY = 1f;

            if (transform.position.y > MaxHeight)  // 보스가 화면의 상단에 위치하면 y축 이동 멈춤
            {
                moveSpeedY = 0;
            }
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
            SceneManager.LoadScene("1gameover");
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
        
    }
}
