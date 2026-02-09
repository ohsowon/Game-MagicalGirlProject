using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Visual_Novel_Director_Epilogue : MonoBehaviour
{

    List<sentence2> main_stream = new List<sentence2>();

    choice choice1;
    choice choice2;

    int timing1;
    int timing2;

    public List<AudioClip> forBgm = new List<AudioClip>();
    public List<AudioClip> forEffectMusic = new List<AudioClip>();

    musics bgms;

    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();

    int length = 0;
    bool Choice_Flag = false;

    UnityEngine.UI.Image Name;
    TextMeshProUGUI Text;
    GameObject Choice_Panel;
    GameObject Script_Panel;

    public GameObject background;
    public GameObject black;


    public AudioSource bgm;
    public AudioSource effectMusic;

    TextMeshProUGUI option1;
    TextMeshProUGUI option2;

    // Start is called before the first frame update
    void Start()
    {
        set_data();
        conpoenet_connect();

        bgms.check(length);
        OnClick();

        //score = 0;

        Choice_Panel.SetActive(false);
    }

    public void conpoenet_connect()
    {
        option1 = GameObject.Find("Choice_1_Text").GetComponent<TextMeshProUGUI>();
        option2 = GameObject.Find("Choice_2_Text").GetComponent<TextMeshProUGUI>();

        Name = GameObject.Find("Script_Panel").GetComponent<UnityEngine.UI.Image>();
        Text = GameObject.Find("Character_Text").GetComponent<TextMeshProUGUI>();
        Choice_Panel = GameObject.Find("Choice_Panel");
    }

    public void OnCreate()
    {
        if (length + 1 == timing1)
        {
            Choice_Panel.SetActive(true);
            choice1.create(option1, option2);
        }
        else
        {
            Choice_Panel.SetActive(true);
            choice2.create(option1, option2);
        }
    }

    void set_data()
    {
        main_stream.Add(new sentence2(null, names[8], backgrounds[0], null, null, black));
        main_stream.Add(new sentence2("그 후로부터 며칠이 지났다.", names[5], backgrounds[0],null,null,black));
        main_stream.Add(new sentence2("로키는 할 일을 마쳐 떠났고, 내 마력도 사라졌다.", names[5], backgrounds[0]));

        main_stream.Add(new sentence2("...", names[5], backgrounds[1],null,null,black));
        main_stream.Add(new sentence2("오늘도 역시나~", names[5], backgrounds[1]));
        main_stream.Add(new sentence2("아니, 이번엔 다르다.", names[5], backgrounds[1]));
        main_stream.Add(new sentence2("이젠 안다.", names[5], backgrounds[1]));
        main_stream.Add(new sentence2("나에겐, 그리고 우리 모두에겐 [특별한 힘]이 있다는 것을....", names[5], backgrounds[1]));
        main_stream.Add(new sentence2("처음 대화해보네.", names[2], backgrounds[3]));
        main_stream.Add(new sentence2("이름이 뭐야?", names[2], backgrounds[3]));
        main_stream.Add(new sentence2("...", names[5], backgrounds[3]));
        main_stream.Add(new sentence2("응, 내 이름은 말야...", names[5], backgrounds[3]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[2],null, null, black));
        main_stream.Add(new sentence2(null, names[8], backgrounds[2]));

        List<music> bgmList = new List<music>();
        bgmList.Add(new music(forBgm[0], 0));
        

        bgms = new musics(bgm, bgmList);

        /*
        List<sentence> choice1_1 = new List<sentence>();
        List<sentence> choice1_2 = new List<sentence>();

        List<sentence> choice2_1 = new List<sentence>();
        List<sentence> choice2_2 = new List<sentence>();  
         
        choice1_1.Add(new sentence("1_1", names[3], null));
        choice1_1.Add(new sentence("1_1_2", names[3], null));
        choice1_2.Add(new sentence("1_2", names[4], null));
        choice1_2.Add(new sentence("1_2_2", names[4], null));
        choice1 = new choice("1_1", "1_2", choice1_1, choice1_2, 1, 0);

        choice2_1.Add(new sentence("2_1", names[6], null));
        choice2_1.Add(new sentence("2_1_2", names[6], null));
        choice2_2.Add(new sentence("2_2", names[7], null));
        choice2_2.Add(new sentence("2_2_2", names[7], null));
        choice2 = new choice("2_1", "2_2", choice2_1, choice2_2, 1, 0);

        timing1 = 3;
        timing2 = 6;
        */
    }

    public void Choiced(int order)
    {
        if (length + 1 == timing1)
        {
            choice1.Clicked(order);
        }
        else
        {
            choice2.Clicked(order);
        }

        Choice_Flag = true;

        Choice_Panel.SetActive(false);

        //점수 처리하기 
        length++;
        OnClick();
    }

    public void OnClick()
    {
        if (length == main_stream.Count)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
            length++;
        }
        else if(length == main_stream.Count + 1)
        {
            Debug.Log("This Sceen is end.");
            SceneManager.LoadScene("a_startscene"); 
            return;
        }
        /*
        else if (length == timing1)
        {
            if (Choice_Flag && choice1.isEnd())
            {
                Choice_Flag = false;
                Choice_Panel.SetActive(false);
                pre_read();
                next_text(length++);
            }
            else if (Choice_Flag)
            {
                pre_read();
                choice1.read(Text, Name,background);
            }
            else
            {
                Debug.Log("Choice is not ended.");
                return;
            }
        }
        else if (length == timing2)
        {
            if (Choice_Flag && choice2.isEnd())
            {
                Choice_Flag = false;
                pre_read();
                next_text(length++);
            }
            else if (Choice_Flag)
            {
                pre_read();
                choice2.read(Text, Name, background);
            }
            else
            {
                Debug.Log("Choice is not ended");
                return;
            }
        }*/
        else
        {
            next_text(length);/*
            if (length + 1 == timing1 || length + 1 == timing2)
            {
                Choice_Panel.SetActive(true);
                OnCreate();
            }
            else
            {
                length++;
            }*/
            bgms.check(length);

            length++;
        }
    }

    void pre_read()
    {
        black.SetActive(false);
    }

    void next_text(int length)
    {
        pre_read();
        main_stream[length].read(Text, Name, background);
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
