using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private Vector3 startPosition;
    private float timeCounter = 0f;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter += Time.deltaTime * 1.5f;

        float offsetX = Mathf.Sin(timeCounter) * 2f;
        float offsetY = Mathf.Cos(timeCounter * 1.5f) * 0.5f;

        Vector3 newPos = startPosition + new Vector3(offsetX, offsetY, 0);

        transform.position = newPos;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player shot"))
        {
            StartCoroutine(BlinkTwice());
            Destroy(collision.gameObject);
        }
    }
    IEnumerator BlinkTwice() //player°¡ ½ð shot1¿¡ ¸ÂÀ¸¸é boss°´Ã¼°¡ µÎ ¹ø ±ôºýÀÓ
    {
        for (int i = 0; i < 2; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
