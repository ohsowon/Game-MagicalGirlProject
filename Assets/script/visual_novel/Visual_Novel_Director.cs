using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Visual_Novel_Director : MonoBehaviour
{
    public List<string> options = new List<string>();
    public List<Sprite> conversation_images = new List<Sprite>();

    TextMeshProUGUI option1;
    TextMeshProUGUI option2;

    public List<string> Texts;
    public List<int> Names;
    public List<int> Choices;

    public List<string> first_branch_1 = new List<string>();
    public List<string> first_branch_2 = new List<string>();

    public List<string> second_branch_1 = new List<string>();
    public List<string> second_branch_2 = new List<string>();

    List<List<string>> branch = new List<List<string>>();

    int length = 0;
    int side_length = 0;
    int next_Choice = 0;
    int what_choiced = -1;
    Boolean Choice_Flag = false;

    Sprite Name;
    TextMeshProUGUI Text;
    GameObject Choice_Panel;
    GameObject Script_Panel;

    GameObject roki;
    GameObject boss;
    GameObject roki_true_form;

    // Start is called before the first frame update
    void Start()
    {
        option1 = GameObject.Find("Choice_1_Text").GetComponent<TextMeshProUGUI>();
        option2 = GameObject.Find("Choice_2_Text").GetComponent<TextMeshProUGUI>();

        Name = GameObject.Find("Script_Panel").GetComponent<Sprite>();
        Text = GameObject.Find("Character_Text").GetComponent<TextMeshProUGUI>();
        Choice_Panel = GameObject.Find("Choice_Panel");
        Choice_Panel.SetActive(false);
        OnClick();

        roki = GameObject.Find("roki");
        boss = GameObject.Find("boss");
        roki_true_form = GameObject.Find("roki_true_form");


        branch.Add(first_branch_1);
        branch.Add(first_branch_2);
        branch.Add(second_branch_1);
        branch.Add(second_branch_2);
    }
    public void OnCreate(int next_choice)
    {
        Debug.Log("choice your options");
        option1.text = options[next_choice * 2];
        option2.text = options[next_choice * 2 + 1];
    }

    public void Choiced(int order)
    {
        Debug.Log(order);
        what_choiced = order;
        Choice_Flag = true;
        Choice_Panel.SetActive(false);

        //점수 처리하기 
        OnClick();

    }

    public void OnClick()
    {
        if (length == Texts.Count)
        {
            Debug.Log("This Sceen is end.");
        }
        else if (length - 1 == Choices[next_Choice])
        {
            if (Choice_Flag && side_length == branch[what_choiced + next_Choice].Count)
            {
                Choice_Flag = false;
                next_text(length++);
            }
            else if (Choice_Flag)
            {

            }
            else
            {
                Debug.Log("Choice is not ended");
            }
        }
        else
        {
            next_text(length);
            if (length++ == Choices[next_Choice])
            {
                Choice_Panel.SetActive(true);
                OnCreate(next_Choice);
            }
        }
    }

    void next_text(int length)
    {
        Name = conversation_images[Names[length]];
        Text.text = Texts[length];
        Debug.Log(Texts[length]);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }
}
