using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotAttack : MonoBehaviour
{
    AudioSource aud;
    public AudioClip attackSE;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("enemy"))
        {
            aud.PlayOneShot(attackSE);
            Destroy(collision.gameObject);
            Destroy(gameObject, attackSE.length);
        }

        if (collision.CompareTag("enemy shot"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.CompareTag("stage2 boss"))
        {
            BossHpManager stage2boss = collision.GetComponent<BossHpManager>();
            if (stage2boss != null)
            {
                stage2boss.BossTakeDamage();
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("stage3 boss"))
        {
            Stage3BossManager stage3boss = collision.GetComponent<Stage3BossManager>();
            if (stage3boss != null)
            {
                stage3boss.BossTakeDamage();
            }

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
