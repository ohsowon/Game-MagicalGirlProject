using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public bool isTutorial = true;  // true : Æ©Åä¸®¾ó ÁøÇà Áß, false : Æ©Åä¸®¾ó ³¡

    public TMP_Text TutorialText;
    public float messageDelay = 2f;
    public string[] messages1;
    public string[] messages2;
    public string[] messages3;
    bool tutorial3Called = false;

    public GameObject tutorialEnemy;
    public GameObject tutorialPoint;
    public GameObject skipButton;

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

    // Start is called before the first frame update
    void Start()
    {
        tutorialEnemy.SetActive(false);
        tutorialPoint.SetActive(false);
        StartCoroutine(Tutorial1());        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tutorial")) 
        {
            ScoreManager.instance.AddScore();
            Destroy(collision.gameObject);
            StartCoroutine(Tutorial2());
        }
    }

    public void Skip()  // Æ©Åä¸®¾ó ½ºÅµ
    {
        isTutorial = false;
        TutorialText.gameObject.SetActive(false);
        StopAllCoroutines();
        skipButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorial3Called == false && tutorialEnemy == null)
        {
            tutorial3Called = true;
            StartCoroutine(Tutorial3());
        }
    }

    IEnumerator Tutorial1()
    {
        foreach (string msg in messages1)
        {
            TutorialText.GetComponent<TextMeshProUGUI>().text = msg;
            yield return new WaitForSeconds(messageDelay);
        }

        tutorialPoint.SetActive(true);
    }

    IEnumerator Tutorial2()
    {
        foreach (string msg in messages2)
        {
            TutorialText.GetComponent<TextMeshProUGUI>().text = msg;
            yield return new WaitForSeconds(messageDelay);
        }

        tutorialEnemy.SetActive(true);
    }

    IEnumerator Tutorial3()
    {
        foreach (string msg in messages3)
        {
            TutorialText.GetComponent<TextMeshProUGUI>().text = msg;
            yield return new WaitForSeconds(messageDelay);
        }

        TutorialText.GetComponent<TextMeshProUGUI>().text = "";
        isTutorial = false;
    }

}
