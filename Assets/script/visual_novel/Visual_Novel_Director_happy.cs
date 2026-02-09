using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Visual_Novel_Director_happy : MonoBehaviour
{

    List<sentence2> main_stream = new List<sentence2>();

    choice choice1;
    choice choice2;

    int timing1;
    int timing2;

    public List<AudioClip> forBgm = new List<AudioClip>();
    public List<AudioClip> forEffectMusic = new List<AudioClip>();

    musics bgms;
    musics effectMusics;

    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();
    public List<GameObject> rokis = new List<GameObject>();

    int length = 0;
    bool Choice_Flag = false;

    UnityEngine.UI.Image Name;
    TextMeshProUGUI Text;
    GameObject Choice_Panel;
    GameObject Script_Panel;

    public GameObject roki;
    public GameObject roki_alert;
    public GameObject roki_nervours;

    public GameObject boss;
    public GameObject background;
    public GameObject black;
    public GameObject line2;
    public GameObject discardanime;
       

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
        effectMusics.check(length);
        OnClick();

        //score = 0;

        Choice_Panel.SetActive(false);
        roki.SetActive(false);
        roki_alert.SetActive(false);
        roki_nervours.SetActive(false);
        boss.SetActive(false);
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


        main_stream.Add(new sentence2("...해치웠나!", names[5], backgrounds[0]));//0
        main_stream.Add(new sentence2("크크큭...", names[0], backgrounds[0]));//0
        main_stream.Add(new sentence2("...!?", names[5], backgrounds[0]));//0
        main_stream.Add(new sentence2("아직 끝나지 않았다.", names[0], backgrounds[0]));//0

        main_stream.Add(new sentence2("우주선들의 잔해 속에서, 최종보스가 모습을 드러낸다. ", names[3], backgrounds[0]));//0
        main_stream.Add(new sentence2("아하하하하!!", names[0], backgrounds[1], boss, null, black)); //5
        main_stream.Add(new sentence2("나는 이 지구를 점령하러 온, '앙골 모아' 다!", names[1], backgrounds[1], boss, line2, null));
        main_stream.Add(new sentence2("지금까지는 잘 싸워 왔겠지만, 너희들의 힘으로 나를 무찌르는 건 무리다.", names[1], backgrounds[1], boss, line2, null));
        main_stream.Add(new sentence2("얌전히 패배를 받아들여라!", names[1], backgrounds[1], boss, line2, null));//    8

        main_stream.Add(new sentence2("...!", names[5], backgrounds[1], boss));
        main_stream.Add(new sentence2("전부 다 무찌른 줄 알았는데, 아직 최종 보스가 남아있었다니...", names[5], backgrounds[1], boss));
        main_stream.Add(new sentence2("저걸 혼자 상대하긴... 무리야. 누가, 도와줘야...!", names[5], backgrounds[1], boss));
        main_stream.Add(new sentence2("미소님! 저랑 힘을 합칩시다!", names[7], backgrounds[1], rokis[0],line2,null));//5//11
        main_stream.Add(new sentence2("...어떻게!?", names[5], backgrounds[1], rokis[1]));
        main_stream.Add(new sentence2("저랑 동시에 매지컬 리디엠션을 외치시는 겁니다!", names[7], backgrounds[1], rokis[4]));
        main_stream.Add(new sentence2("할 수 있겠어요?", names[7], backgrounds[1], rokis[1]));
        main_stream.Add(new sentence2("...!", names[5], backgrounds[1], rokis[5]));
        main_stream.Add(new sentence2("그래... 로키는 내게 특별한 힘이 있다고 했지.", names[5], backgrounds[0], rokis[5]));//11
        main_stream.Add(new sentence2("혼자서는 모르겠지만... 로키와 함께라면, 특별한 힘이 뭔지 조금은 알 것 같아.", names[5], backgrounds[0], rokis[1]));
        main_stream.Add(new sentence2("로키를 믿어야겠어!", names[5], backgrounds[0], rokis[1]));

        main_stream.Add(new sentence2("하나, 둘, 셋!", names[3], backgrounds[2],null,null,black ));//14
        main_stream.Add(new sentence2("매지컬 리디엠션...!", names[3], backgrounds[2]));

        main_stream.Add(new sentence2("어림도 없.... 어?", names[1], backgrounds[2], boss, null, black));
        main_stream.Add(new sentence2("으, 으아아아악!!!", names[1], backgrounds[2], boss,discardanime,null));//17
        main_stream.Add(new sentence2(null, names[8], backgrounds[2]));

        List<music> bgmList = new List<music>();
        bgmList.Add(new music(forBgm[2], 0));
        bgmList.Add(new music(forBgm[0], 3));
        bgmList.Add(new music(forBgm[2],9));
        bgmList.Add(new music(forBgm[1], 13));

        bgms = new musics(bgm, bgmList);
        List<music> effectMusicList = new List<music>();
        effectMusicList.Add(new music(forEffectMusic[9], 1));
        effectMusicList.Add(new music(forEffectMusic[1], 2));
        effectMusicList.Add(new music(forEffectMusic[2], 5));
        effectMusicList.Add(new music(forEffectMusic[10], 6));
        effectMusicList.Add(new music(forEffectMusic[1], 9));
        effectMusicList.Add(new music(forEffectMusic[5], 12));
        effectMusicList.Add(new music(forEffectMusic[3], 13));
        effectMusicList.Add(new music(forEffectMusic[4], 17));
        effectMusicList.Add(new music(forEffectMusic[7], 21));
        effectMusicList.Add(new music(forEffectMusic[8], 22));
        effectMusicList.Add(new music(forEffectMusic[6], 23));


        effectMusics = new musics(effectMusic, effectMusicList);

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
            Debug.Log("This Sceen is end.");
            SceneManager.LoadScene("VNEpilogue"); // 추가: "Stage2Scene" 씬으로 전환
            return;
        }/*
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
            effectMusics.check(length);
            length++;
        }
    }

    void pre_read()
    {
        roki.SetActive(false);
        roki_alert.SetActive(false);
        roki_nervours.SetActive(false);
        boss.SetActive(false);
        black.SetActive(false);
        line2.SetActive(false);
        discardanime.SetActive(false);
        // rokis 리스트 전체 비활성화
        foreach (GameObject r in rokis)
        {
            if (r != null)
                r.SetActive(false);
        }
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
