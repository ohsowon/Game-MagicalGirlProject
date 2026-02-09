using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject minionPrefab;
    public GameObject firstMinion;
    GameObject minion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void GenerateEnemy()
    {
        minion = Instantiate(minionPrefab);
        float PosX = Random.Range(-1.5f, 1.5f);
        minion.transform.position = new Vector2(PosX, 126.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(firstMinion != null || minion != null))
        {
            GenerateEnemy();
        }

        if ((gameObject == null && minion != null) || (gameObject == null && firstMinion != null))
        {
            Destroy(minion);
            Destroy(firstMinion);
        }
        
    }
}
