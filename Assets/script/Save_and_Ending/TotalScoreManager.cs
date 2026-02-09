using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreManager : MonoBehaviour
{
    public static TotalScoreManager Instance;

    public int Stage1Score;
    public int Stage2Score;
    public int Stage3Score;
    public int Vn2Score;
    public int Vn3Score;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //if (DataManager.Instance != null)
        //{
        //    Stage1Score = DataManager.Instance.nowdata.Stage1Score;
        //    Stage2Score = DataManager.Instance.nowdata.Stage2Score;
        //    Stage3Score = DataManager.Instance.nowdata.Stage3Score;
        //    Vn2Score = DataManager.Instance.nowdata.VN2Score;
        //    Vn3Score = DataManager.Instance.nowdata.VN3Score;
        //}
        //else
        //{
        //    Debug.Log("DataManager.instance == null");
        //}
    }

    // 점수 저장하는 함수
    public void SetScore(int scene, int score)
    {
        switch (scene)
        {
            case 1:
                Stage1Score = score;
                break;
            case 2:
                Stage2Score = score;
                break;
            case 3:
                Stage3Score = score;
                break;
            case 4:
                Vn2Score = score;
                break;
            case 5:
                Vn3Score = score;
                break;
        }
    }

    // 점수 가져오는 함수
    public int GetScore(int scene)
    {
        return scene switch
        {
            1 => Stage1Score,
            2 => Stage2Score,
            3 => Stage3Score,
            4 => Vn2Score,
            5 => Vn3Score,
            _ => 0
        };
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}

    

