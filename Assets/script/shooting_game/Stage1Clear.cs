using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Stage1Clear : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    AudioSource aud;
    public AudioClip clearSE;
    public AudioSource bgmSource;

    public TMP_Text ClearText;

    bool hasTriggered = false;

    public TextMeshProUGUI ScoreText;
    public int Stage1Score;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        ClearText.gameObject.SetActive(false);
    }

    void Clear()
    {
        bgmSource.Stop();
        aud.PlayOneShot(clearSE);
        ClearText.gameObject.SetActive(true);
        Invoke("NextScene", 3f);
    }

    void NextScene()
    {
        SceneManager.LoadScene("VN2Scene");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy1 == null && enemy2 == null && enemy3 == null && hasTriggered == false)
        {
            hasTriggered = true;
            Stage1Score = int.Parse(ScoreText.text);
            TotalScoreManager.Instance.SetScore(1, Stage1Score);
            Debug.Log("½ºÅ×ÀÌÁö1 È¹µæ Á¡¼ö: " + Stage1Score);
            player.GetComponent<PlayerController_Stage1>().enabled = false;
            Clear();

        }
    }
}
