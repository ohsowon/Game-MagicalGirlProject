using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage3Clear : MonoBehaviour
{
    public GameObject boss;

    AudioSource aud;
    public AudioClip clearSE;
    public AudioSource bgmSource;

    public TMP_Text ClearText;

    bool hasTriggered = false;

    public TextMeshProUGUI ScoreText;
    public int Stage3Score;

    public GameObject player;

    int s1;
    int s2;
    int s3;
    int s4;
    int s5;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        ClearText.gameObject.SetActive(false);

        // 각 스테이지의 점수를 TotalScoreManager에서 가져옴
        s1 = TotalScoreManager.Instance.GetScore(1);
        s2 = TotalScoreManager.Instance.GetScore(2);
        s3 = TotalScoreManager.Instance.GetScore(3);
        s4 = TotalScoreManager.Instance.GetScore(4);
        s5 = TotalScoreManager.Instance.GetScore(5);
    }

    void Clear()
    {
        bgmSource.Stop();
        aud.PlayOneShot(clearSE);
        ClearText.gameObject.SetActive(true);
        Debug.Log(s1 + s2 + s3 + s4 + s5);
        Invoke("NextScene", 3f);
    }

    void NextScene()
    {
        if (s1 >= 10 && s2 == 10 && s3 == 20 && ((s4 + s5) >= 1))
        {
            Debug.Log("해피엔딩");
            SceneManager.LoadScene("VNHappyEndingScene");
        }

        if (!(s1 >= 10 && s2 == 10 && s3 == 20) && ((s4 + s5) >= 1))
        {
            Debug.Log("진엔딩");
            SceneManager.LoadScene("Rscene");
        }

        if (!(s1 >= 10 && s2 == 10 && s3 == 20) && ((s4 + s5) < 1))
        {
            Debug.Log("배드엔딩");
            SceneManager.LoadScene("Bscene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null && hasTriggered == false)
        {
            hasTriggered = true;
            Stage3Score = int.Parse(ScoreText.text);
            TotalScoreManager.Instance.SetScore(3, Stage3Score);
            s3 = TotalScoreManager.Instance.GetScore(3);
            Debug.Log("스테이지3 획득 점수: " + Stage3Score);
            player.GetComponent<PlayerController>().enabled = false;
            Clear();
        }
    }
}
