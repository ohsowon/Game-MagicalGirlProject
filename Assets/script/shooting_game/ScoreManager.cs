using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    GameObject ScoreText;
    int score = 0;

    AudioSource aud;
    public AudioClip pointSE;

    // Start is called before the first frame update
    void Start()
    {
        this.ScoreText = GameObject.Find("ScoreText");
        aud = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (instance == null)    
        {
            instance = this;      
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore()
    {
        aud.PlayOneShot(pointSE);
        score += 1;
        UpdateScoreUI();
    }



    void UpdateScoreUI()
    {
        ScoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
