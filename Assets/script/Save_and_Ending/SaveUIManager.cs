using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveUIManager : MonoBehaviour
{
    public GameObject SaveSlot;
    public GameObject SlotOpenButton;
    //public GameObject NotiText;
    public GameObject MainUI;

    //void Awake()
    //{
    //    DontDestroyOnLoad(this.gameObject);
    //}

    public void SaveSlotOpen()
    {
        SaveSlot.SetActive(true);
        SlotOpenButton.SetActive(false);
        if (MainUI != null)
        {
            MainUI.SetActive(false);
        }
    }

    public void SaveSlotClose()
    {
        SaveSlot.SetActive(false);

        if (MainUI != null)
        {
            MainUI.SetActive(true);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SaveSlot.SetActive(false);
        //NotiText.SetActive(false);
        MainUI = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        string CurrentScene = SceneManager.GetActiveScene().name;
        if (CurrentScene == "VN1scene" || CurrentScene == "VN2scene" || CurrentScene == "VN3scene")
        {
            SlotOpenButton.SetActive(true);
        }
        else
        {
            SlotOpenButton.SetActive(false);
        }
    }
}
