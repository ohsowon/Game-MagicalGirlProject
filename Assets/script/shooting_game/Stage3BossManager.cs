using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3BossManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public int maxHP = 35;  // 보스의 최대 HP
    public int currentHP;

    public Slider hpGauge;

    private Vector3 startPosition;
    public float movementSpeed = 1f;
    private float timeCounter = 0f;

    public Vector2 teleportMinBounds = new Vector2(-4, 127);
    public Vector2 teleportMaxBounds = new Vector2(4, 130);
    private bool hasTeleported = false;

    public void BossTakeDamage()
    {
        currentHP -= 1;  // 공격 맞으면 1씩 감소
        Debug.Log("boss HP:" + currentHP);

        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        hpGauge.value = currentHP;

        if (currentHP == 0)  // HP가 0이 되면 Destroy
        {
            Destroy(gameObject);
            Destroy(hpGauge.gameObject);
        }
    }

    void InInfinityPattern()
    {
        timeCounter += Time.deltaTime * 1.5f;

        float offsetX = Mathf.Sin(timeCounter) * 2f;
        float offsetY = Mathf.Cos(timeCounter * 1.5f) * 0.5f;

        Vector3 newPos = startPosition + new Vector3(offsetX, offsetY, 0);

        transform.position = newPos;
    }

    void Teleport()
    {
        float randomX = Random.Range(teleportMinBounds.x, teleportMaxBounds.x);
        float randomY = Random.Range(teleportMinBounds.y, teleportMaxBounds.y);

        transform.position = new Vector3(randomX, randomY, transform.position.z);
    }

    System.Collections.IEnumerator TeleportRepeatedly()
    {
        while (true)
        {
            Teleport();
            yield return new WaitForSeconds(2f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player shot"))
        {
            StartCoroutine(BlinkTwice());
            Destroy(collision.gameObject);
        }
    }
    IEnumerator BlinkTwice() //player가 쏜 shot1에 맞으면 boss객체가 두 번 깜빡임
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpGauge.maxValue = maxHP;
        hpGauge.value = maxHP;

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP > 20)
        {
            InInfinityPattern();
        }
        else if (!hasTeleported)
        {
            StartCoroutine(TeleportRepeatedly());
            hasTeleported = true;
        }
    }
}
