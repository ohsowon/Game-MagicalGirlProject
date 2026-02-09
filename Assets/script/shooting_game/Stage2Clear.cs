using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage2Clear : MonoBehaviour
{
    public GameObject boss;

    AudioSource aud;
    public AudioClip clearSE;
    public AudioSource bgmSource;

    public TMP_Text ClearText;

    bool hasTriggered = false;

    public TextMeshProUGUI ScoreText;
    public int Stage2Score;

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
        SceneManager.LoadScene("VN3scene");
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null && hasTriggered == false)
        {
            hasTriggered = true;
            Stage2Score = int.Parse(ScoreText.text);
            TotalScoreManager.Instance.SetScore(2, Stage2Score);
            Debug.Log("½ºÅ×ÀÌÁö2 È¹µæ Á¡¼ö: " + Stage2Score);
            player.GetComponent<PlayerController>().enabled = false;
            Clear();
        }
    }
}
