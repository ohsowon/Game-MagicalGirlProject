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

struct sentence
{
    string text;
    Sprite name;
    Sprite background;


    GameObject standing1;
    GameObject standing2;
    GameObject standing3;

    string effect;

    public sentence(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3, string effect)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = effect;
    }

    public sentence(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = null;
    }

    public sentence(string text, Sprite name, Sprite background, GameObject standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
    }

    public sentence(string text, Sprite name, Sprite background)
    {
        this.text = text;
        this.name = name;
        this.standing1 = null;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
    }

    public void read(TextMeshProUGUI text, UnityEngine.UI.Image name, GameObject background)
    {
        text.text = this.text;
        name.sprite = this.name;
        if (this.standing1)
        {
            this.standing1.SetActive(true);
        }
        if (this.standing2)
        {
            this.standing2.SetActive(true);

        }
        if (this.standing3)
        {
            this.standing3.SetActive(true);

            fadeinhelp.Instance.StartCoroutine(FadeIn(this.standing3));
        }
        if (this.background == null)
        {
            background.SetActive(false);
        }
        else
        {
            background.SetActive(true);
            background.GetComponent<SpriteRenderer>().sprite = this.background;
        }
    }
    // 추가: 페이드 인 Coroutine
    private IEnumerator FadeIn(GameObject obj)
    {
        float duration = 1f; // 1초 동안 페이드
        float elapsed = 0f;

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        Color color = sr.color;
        color.a = 1f;
        sr.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - elapsed / duration);
            sr.color = color;
            yield return null;
        }
        color.a = 0f;
        sr.color = color;
    }
}

public struct music
{
    public AudioClip audio;
    public int timing;

    public music(AudioClip audio, int timing)
    {
        this.audio = audio;
        this.timing = timing;
    }
}

public struct musics
{
    public List<music> music;
    public int count;
    public AudioSource MusicPlayer;

    public musics(AudioSource MusicPlayer, List<music> music)
    {
        this.MusicPlayer = MusicPlayer;
        this.music = music;
        count = 0;
    }

    public void check(int length)
    {
        if (count >= music.Count)
        {
            return;
        }
        else if(length == music[count].timing)
        {
            MusicPlayer.clip = music[count].audio;
            MusicPlayer.Play();
            count++;
        }
    }
    public void reset()
    {
        count = 0;
    }
}

struct choice
{
    public string option1 { get; set; }
    public string option2 { get; set; }
    public int type { get; set; }
    
    List<sentence> option1_sentence;
    List<sentence> option2_sentence;

    int now;

    int score1;
    int score2;

    public choice(string option1, string option2, List<sentence> op1_s, List<sentence> op2_S, int score1, int score2)
    {
        this.option1 = option1;
        this.option2 = option2;
        this.option1_sentence = op1_s;
        this.option2_sentence = op2_S;
        this.score1 = score1;
        this.score2 = score2;
        this.now = 0;
        this.type = 0;
    }

    public void create(TextMeshProUGUI option1, TextMeshProUGUI option2)
    {
        option1.text = this.option1;
        option2.text = this.option2;
    }

    public void Clicked(int value)
    {
        this.type = value;
        Debug.Log("type:" + type);
    }

    public bool read(TextMeshProUGUI text, UnityEngine.UI.Image name, GameObject background, musics bgmPlayer, musics effectPlayer)
    {
        if(type == 0)
        {
            if(now == option1_sentence.Count)
            {
                return false;
            }
            option1_sentence[now++].read(text, name, background);
            return true;
        }
        else
        {
            if (now == option2_sentence.Count)
            {
                return false;
            }
            option2_sentence[now++].read(text, name, background);
            return true;
        }
    }

    public int getLegnth()
    {
        if (type == 0)
        {
            return option1_sentence.Count;
        }
        else
        {
            return option2_sentence.Count;
        }
    }

    public bool isEnd()
    {
        if(now == getLegnth())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
/*

struct stream
{
    List<sentence> mainStream;

    int branch1;
    int branch2;

    choice choice1;
    choice choice2;

    int now;

    public stream(List<sentence> mainStream, int b1, int b2, choice choice1, choice choice2)
    {
        this.mainStream = mainStream;
        this.branch1 = b1;
        this.branch2 = b2;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.now = 0;
    }

    public void read(TextMeshProUGUI text, Sprite name, Sprite standing1, Sprite standing2)
    {
        if (now == branch1)
        {
            choice1.read(text, name, standing1, standing2)
        }
        else if (now == branch2)
        {
            choice1.read(text, name, standing1, standing2)
        }
        else
        {

        }
    }
}

class sentences
{
    string text;
    Sprite name;

    Sprite standing1;
    Sprite standing2;

    string effect;

    public sentences(string text, Sprite name, Sprite standing1, Sprite standing2, string effect)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.effect = effect;
    }

    public void sentence(string text, Sprite name, Sprite standing1, Sprite standing2)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.effect = null;
    }

    public void sentence(string text, Sprite name, Sprite standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.effect = null;
    }

    public void sentence(string text, Sprite name)
    {
        this.text = text;
        this.name = name;
        this.standing1 = null;
        this.standing2 = null;
        this.effect = null;
    }

    string getText()
    {
        return text;
    }
    void setText(string text)
    {
        this.text = text;
    }
    Sprite getName()
    {
        return name;
    }
    void setName(Sprite name)
    {
        this.name = name;
    }
    Sprite getStanding1()
    {
        return standing1;
    }
    void setStanding(Sprite standing1)
    {
        this.standing1 = standing1;
    }
    Sprite getStanding2()
    {
        return standing2;
    }
    void setStanding2(Sprite standing2)
    {
        this.standing2 = standing2;
    }
    string getEffect()
    {
        return effect;
    }
    void setEffect(string effect)
    {
        this.effect = effect;
    }


}
*/

public class test_Visual_Novel_Director1 : MonoBehaviour
{
    List<sentence> main_stream = new List<sentence>();


    choice choice1;
    choice choice2;

    int timing1;
    int timing2;

    List<sentence> choice1_1 = new List<sentence>();
    List<sentence> choice1_2 = new List<sentence>();

    List<sentence> choice2_1 = new List<sentence>();
    List<sentence> choice2_2 = new List<sentence>();    

    public List<AudioClip> forBgm = new List<AudioClip>();
    public List<AudioClip> forEffectMusic = new List<AudioClip>();

    musics bgms;
    musics effectMusics;

    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();

    int length = 0;
    int bgmLength = 0;
    bool Choice_Flag = false;

    

    UnityEngine.UI.Image Name;
    TextMeshProUGUI Text;
    GameObject Choice_Panel;
    GameObject Script_Panel;

    public GameObject roki;
    public GameObject boss;
    public GameObject roki_true_form;
    public GameObject background;

    public GameObject black;
    public GameObject line2;
    public GameObject animemove;
    public GameObject rr;

    public AudioSource bgm;
    public AudioSource effectMusic;

    TextMeshProUGUI option1;
    TextMeshProUGUI option2;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        set_data();
        conpoenet_connect();

        bgms.check(length);
        effectMusics.check(length);
        OnClick();
        

        score = 0;

        

        Choice_Panel.SetActive(false);
        roki.SetActive(false);
        boss.SetActive(false);
        roki_true_form.SetActive(false);
    }

    public void conpoenet_connect()
    {
        option1 = GameObject.Find("Choice_1_Text").GetComponent<TextMeshProUGUI>();
        option2 = GameObject.Find("Choice_2_Text").GetComponent<TextMeshProUGUI>();

        Name = GameObject.Find("Script_Panel").GetComponent<UnityEngine.UI.Image>();
        Text = GameObject.Find("Character_Text").GetComponent<TextMeshProUGUI>();
        Choice_Panel = GameObject.Find("Choice_Panel");

        roki = GameObject.Find("roki");
        boss = GameObject.Find("boss");
        roki_true_form = GameObject.Find("roki_true_form");

        bgm = GameObject.Find("bgm").GetComponent<AudioSource>();
        effectMusic = GameObject.Find("effectMusic").GetComponent<AudioSource>();
    }

    public void OnCreate()
    {
        Debug.Log("choice your options");
        if(length + 1 == timing1)
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
        main_stream.Add(new sentence("그래... 지구에서도 마법이 발견됐단 말이지?", names[0], backgrounds[13]));
        main_stream.Add(new sentence("후후후...", names[0], backgrounds[13]));
        main_stream.Add(new sentence("아하하하하!!", names[0], backgrounds[13]));
        main_stream.Add(new sentence("좋다... 그 힘이라면... 가능하겠군.", names[0], backgrounds[13]));
        main_stream.Add(new sentence("네 녀석에게 임무를 맡기마....", names[0], backgrounds[13]));

        main_stream.Add(new sentence("하아... 오늘도 또야.", names[4], backgrounds[2]));
        main_stream.Add(new sentence("내 이름은 김미소. 고등학교 1학년이다.", names[5], backgrounds[2]));
        main_stream.Add(new sentence("고등학교에 입학한 지도 꽤 지났지만, 아직 친구를 사귀지 못했다.", names[5], backgrounds[2]));
        main_stream.Add(new sentence("이런 매일이 똑같고 외로운 일상은 지겹다구.", names[5], backgrounds[2]));
        main_stream.Add(new sentence("앞으로 남은 고등학교 생활도 이렇게 보내야 하는 건 아니겠지..?", names[5], backgrounds[2]));
        main_stream.Add(new sentence("도수 높은 안경, 주눅 들어보이는 인상, 화장끼 없는 얼굴...", names[3], backgrounds[2]));
        main_stream.Add(new sentence("나였어도, 나같은 평범 이하인 애랑 친구하기 싫을 거야.", names[5], backgrounds[2]));
        main_stream.Add(new sentence("내게도 [ 특별한 힘 ]이 있으면 좋겠어...!", names[5], backgrounds[2]));
        
        main_stream.Add(new sentence("특별한 힘을 원하십니까?", names[6], backgrounds[13]));
        main_stream.Add(new sentence("!?", names[5], backgrounds[13]));
        main_stream.Add(new sentence("어디서 나는 소리지...?", names[5], backgrounds[13]));
        main_stream.Add(new sentence("제가 그 소원을 들어드리겠습니다.", names[6], backgrounds[13]));
        main_stream.Add(new sentence("누, 누구야?", names[5], backgrounds[13]   ));
        main_stream.Add(new sentence("콰당-!!!", names[3], backgrounds[13]));
        main_stream.Add(new sentence("바로 그 때, 하늘에서 무언가가 뚝 떨어진다.", names[3], backgrounds[13]));

        main_stream.Add(new sentence("아야야, 엉덩이야", names[6], backgrounds[1], roki));
        main_stream.Add(new sentence("...으아악! 고양이가 말을...!?", names[5], backgrounds[1], roki));
        main_stream.Add(new sentence("특별한 힘을 갖고 싶다고 한 게 당신인가요?", names[6], backgrounds[1], roki));
        main_stream.Add(new sentence("뭐? 그, 그렇긴 한데...", names[5], backgrounds[1], roki));
        main_stream.Add(new sentence("삐빅- 삐빅-", names[3], backgrounds[1], roki));
        main_stream.Add(new sentence("어디선가 경보음이 울린다.", names[3], backgrounds[1], roki));
        main_stream.Add(new sentence("그렇다면 마침 잘 됐습니다!", names[6], backgrounds[1], roki));
        main_stream.Add(new sentence("설명은 나중에 해 드릴테니, 저와 함께 외계인을 무찔러 주세요!", names[6], backgrounds[1], roki));
        main_stream.Add(new sentence("뭐? 잠깐만, 갑자기 무슨 소린지 모르겠는데...!", names[5], backgrounds[1], roki));
        main_stream.Add(new sentence("상황 파악을 할 겨를도 없이, 정체 불명의 생명체는 마법 주문을 읊는다.", names[3], backgrounds[1], roki));
        main_stream.Add(new sentence("매지컬 리디엠션...!", names[6], backgrounds[1], roki));

        main_stream.Add(new sentence("이 모습은... 설마 나?", names[5], backgrounds[14]));
        main_stream.Add(new sentence("그렇지만 이 복장... 아무리 봐도 '마법 소녀'라고 밖에 설명이 안 되잖아.", names[5], backgrounds[14]));
        main_stream.Add(new sentence("혼란스럽지만... 일단은 그 녀석이 말 한 대로 외계인들을 무찔러 보자!", names[5], backgrounds[14]));

        List<music> bgmList = new List<music>();
        bgmList.Add(new music(forBgm[0], 0));
        bgmList.Add(new music(forBgm[1], 5));
        bgmList.Add(new music(null, 13));
        bgmList.Add(new music(forBgm[2], 20));
        bgmList.Add(new music(forBgm[3], 31));

        bgms = new musics(bgm, bgmList);

        List<music> effectMusicList = new List<music>();
        effectMusicList.Add(new music(forEffectMusic[0], 31));

        effectMusics = new musics(effectMusic, effectMusicList);

        /*
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
        Debug.Log(order + "/" + length + "/" + timing1);
        if(length + 1 == timing1)
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
        Debug.Log(length);
        if (length == main_stream.Count)
        {
            Debug.Log("This Sceen is end.");
            SceneManager.LoadScene("Stage1Scene");
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
        boss.SetActive(false);
        roki_true_form.SetActive(false);
        line2.SetActive(false);
        black.SetActive(false);
        animemove.SetActive(false);
        rr.SetActive(false);
    }

    void next_text(int length)
    {
        pre_read();
        main_stream[length].read(Text, Name, background);
    }

    // 세이브 버튼을 눌렀을 때 대사가 넘어가는 문제 때문에 만들어둠
    bool IsTouchOnButton()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.GetComponent<UnityEngine.UI.Button>() != null)
            {
                return true;
            }
        }

        return false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchOnButton()) return; // 세이브 버튼을 눌렀을 때는 대사가 넘어가지 않도록함

            OnClick();
        }
    }

}
