using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    GameObject pauseButton;
    GameObject resumeButton;
    GameObject grayPanel;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        this.pauseButton = GameObject.Find("PauseButton");
        this.resumeButton = GameObject.Find("ResumeButton");
        this.grayPanel = GameObject.Find("GrayPanel");

        resumeButton.SetActive(false);
        grayPanel.SetActive(false);     
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;

        grayPanel.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        grayPanel.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
