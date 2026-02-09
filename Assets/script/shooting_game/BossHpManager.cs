using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BossHpManager : MonoBehaviour
{
    public int maxHP = 35;  // 보스의 최대 HP
    public int currentHP;

    public Slider hpGauge;
    
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

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        hpGauge.maxValue = maxHP;
        hpGauge.value = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
