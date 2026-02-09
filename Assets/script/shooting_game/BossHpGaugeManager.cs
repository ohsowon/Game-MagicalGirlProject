using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpGaugeManager : MonoBehaviour
{
    public Transform boss;
    public Vector3 offset = new Vector3(0, -1f, 0);
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null || mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToScreenPoint(boss.position + offset);

        
        transform.position = screenPos;
    }
}
