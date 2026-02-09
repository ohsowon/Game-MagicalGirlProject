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

struct sentence29
{
    string text;
    Sprite name;
    Sprite background;


    GameObject standing1;
    GameObject standing2;
    GameObject standing3;

    string effect;

    public sentence29(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3, string effect)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = effect;
    }

    public sentence29(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = null;
    }

    public sentence29(string text, Sprite name, Sprite background, GameObject standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
    }

    public sentence29(string text, Sprite name, Sprite background)
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


struct choice29
{
    public string option1 { get; set; }
    public string option2 { get; set; }
    public int type { get; set; }

    List<sentence29> option1_sentence;
    List<sentence29> option2_sentence;

    int now;

    int score1;
    int score2;

    public choice29(string option1, string option2, List<sentence29> op1_s, List<sentence29> op2_S, int score1, int score2)
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
        this.now = 0;
    }

    public bool read(TextMeshProUGUI text, UnityEngine.UI.Image name, GameObject background, musics bgmPlayer, musics effectPlayer)
    {
        if (type == 0)
        {
            if (now == option1_sentence.Count)
            {
                return false;
            }
            bgmPlayer.check(now);
            effectPlayer.check(now);
            option1_sentence[now++].read(text, name, background);
            return true;
        }
        else
        {
            if (now == option2_sentence.Count)
            {
                return false;
            }
            bgmPlayer.check(now);
            effectPlayer.check(now);
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
        if (now == getLegnth())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //  추가: 선택지 점수 getter
    public int GetScore1() { return score1; }
    public int GetScore2() { return score2; }

}


public class vn1replace : MonoBehaviour
{
    List<sentence29> main_stream = new List<sentence29>();

    choice2 choice1;
    choice2 choice2;

    int timing1;
    int timing2;

    List<sentence29> choice1_1 = new List<sentence29>();
    List<sentence29> choice1_2 = new List<sentence29>();

    List<sentence29> choice2_1 = new List<sentence29>();
    List<sentence29> choice2_2 = new List<sentence29>();

    public List<AudioClip> forBgm = new List<AudioClip>();
    public List<AudioClip> forEffectMusic = new List<AudioClip>();

    musics bgms;
    musics effectMusics;
    musics choice1_1_bgms;
    musics choice1_2_bgms;
    musics choice2_1_bgms;
    musics choice2_2_bgms;

    musics choice1_1_effects;
    musics choice1_2_effects;
    musics choice2_1_effects;
    musics choice2_2_effects;


    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();
    public List<GameObject> rokis = new List<GameObject>();

    int length = 0;
    int what_choiced = -1;
    bool Choice_Flag = false;
    int bgmLength = 0;

    UnityEngine.UI.Image Name;
    TextMeshProUGUI Text;
    GameObject Choice_Panel;
    GameObject Script_Panel;

    public GameObject roki;
    public GameObject boss;
    public GameObject roki_true_form;
    public GameObject background;
    public GameObject line;
    public GameObject black;
    public GameObject line2;
    public GameObject animemove;
    public GameObject rr;
    public GameObject rr2;
    public GameObject road;
    public GameObject errorr;
    public GameObject magic;
    public GameObject magic2;
    public GameObject magic3;
    public GameObject magicanime;

    public AudioSource bgm;
    public AudioSource effectMusic;

    TextMeshProUGUI option1;
    TextMeshProUGUI option2;

    int score;

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
        // timing1 선택지
        if (length + 1 == timing1)
        {
            Debug.Log("choice your options");
            Choice_Panel.SetActive(true);
            choice1.create(option1, option2);
        }
        // timing2 선택지
        if (length + 1 == timing2)
        {
            Debug.Log("choice your options");
            Choice_Panel.SetActive(true);
            choice2.create(option1, option2);  // timing2 선택지 표시
        }
    }

    void set_data()
    {
        main_stream.Add(new sentence29("그래... 지구에서도 마법이 발견됐단 말이지?", names[0], backgrounds[13], null, null, black));
        main_stream.Add(new sentence29("후후후...", names[0], backgrounds[15], null, null, black));
        main_stream.Add(new sentence29("아하하하하!!", names[0], backgrounds[15]));
        main_stream.Add(new sentence29("좋다... 그 힘이라면... 가능하겠군.", names[0], backgrounds[15]));
        main_stream.Add(new sentence29("네 녀석에게 임무를 맡기마....", names[0], backgrounds[16], null, null, black));
        main_stream.Add(new sentence29(null, names[8], backgrounds[16], rr2, null, null));
        main_stream.Add(new sentence29("...", names[6], backgrounds[13], rr, null, null));

        main_stream.Add(new sentence29(null, names[8], backgrounds[0], null, null, black));//8
        main_stream.Add(new sentence29("하아... 오늘도 또야.", names[4], backgrounds[0]));

        main_stream.Add(new sentence29("누구랑 말 한마디 못하고 오늘 하루도 끝이 났다...", names[4], backgrounds[2],null,null,black));
        main_stream.Add(new sentence29("앞으로 남은 고등학교 생활도 이렇게 보내야 하는 건 아니겠지..?", names[4], backgrounds[2]));

        main_stream.Add(new sentence29("내 이름은 김미소. 고등학교 1학년이다.", names[5], backgrounds[1]));//12
        main_stream.Add(new sentence29("고등학교에 입학한 지도 꽤 지났지만, 아직 친구를 사귀지 못했다.", names[5], backgrounds[1]));
        main_stream.Add(new sentence29("이런 매일이 똑같고 외로운 일상은 지겹다구.", names[5], backgrounds[1]));
        main_stream.Add(new sentence29("앞으로 남은 고등학교 생활도 이렇게 보내야 하는 건 아니겠지..?", names[5], backgrounds[1]));
        main_stream.Add(new sentence29("도수 높은 안경, 주눅 들어보이는 인상, 화장끼 없는 얼굴...", names[3], backgrounds[17],null,null,black));
        main_stream.Add(new sentence29("나였어도, 나같은 평범 이하인 애랑 친구하기 싫을 거야.", names[5], backgrounds[17]));
        main_stream.Add(new sentence29("내게도 [ 특별한 힘 ]이 있으면 좋겠어...!", names[5], backgrounds[17]));//18

        main_stream.Add(new sentence29("특별한 힘을 원하십니까?", names[6], backgrounds[13], null, null, black));
        main_stream.Add(new sentence29("!?", names[5], backgrounds[13]));//18
        main_stream.Add(new sentence29("어디서 나는 소리지...?", names[5], backgrounds[13]));
        main_stream.Add(new sentence29("제가 그 소원을 들어드리겠습니다.", names[6], backgrounds[13]));
        main_stream.Add(new sentence29("누, 누구야?", names[5], backgrounds[1], null, null, black));//21 23
        main_stream.Add(new sentence29("콰당-!!!", names[3], backgrounds[1], road,line, null));
        main_stream.Add(new sentence29("바로 그 때, 하늘에서 무언가가 뚝 떨어진다.", names[3], backgrounds[13]));

        main_stream.Add(new sentence29("아야야, 엉덩이야.", names[6], backgrounds[1], rokis[4],null,black));//25
        main_stream.Add(new sentence29("!?!?", names[5], backgrounds[1], rokis[4], line, null));
        main_stream.Add(new sentence29("...으아악! 고양이가 말을...!?", names[5], backgrounds[1], rokis[4], road, null));//28
        main_stream.Add(new sentence29("놀라지 마세요! 해치려는 게 아니에요.", names[6], backgrounds[1], rokis[6], line, null));//28
        main_stream.Add(new sentence29("...!", names[5], backgrounds[1], rokis[6]));//29
        main_stream.Add(new sentence29("특별한 힘을 갖고 싶다고 한 게 당신인가요?", names[6], backgrounds[1], rokis[5]));
        main_stream.Add(new sentence29("뭐? 그, 그렇긴 한데...", names[5], backgrounds[1], rokis[5]));
        main_stream.Add(new sentence29("삐빅- 삐빅-", names[3], backgrounds[1], rokis[6],errorr,black));//32
        main_stream.Add(new sentence29("이 때, 어디선가 경보음이 울린다.", names[3], backgrounds[1], rokis[6]));
        main_stream.Add(new sentence29("...!", names[6], backgrounds[1], rokis[3],road,null));
        main_stream.Add(new sentence29("특별한 힘을 갖고 싶다고 했죠. 그렇다면 마침 잘 됐습니다!", names[6], backgrounds[1], rokis[3]));
        main_stream.Add(new sentence29("설명은 나중에 해 드릴테니, 저와 함께 외계인을 무찔러 주세요!", names[6], backgrounds[1], rokis[0],line,null));
        main_stream.Add(new sentence29("뭐? 잠깐만, 갑자기 무슨 소린지 모르겠는데...!", names[5], backgrounds[1], rokis[0]));
        main_stream.Add(new sentence29("상황 파악을 할 겨를도 없이, 정체 불명의 생명체는 마법 주문을 읊는다.", names[3], backgrounds[1], rokis[3]));
        main_stream.Add(new sentence29("매지컬 리디엠션...!", names[6], backgrounds[1], rokis[0]));

        main_stream.Add(new sentence29("이 모습은... 설마 나?", names[5], backgrounds[14],magic,magicanime,black));
        main_stream.Add(new sentence29("그렇지만 이 복장... 아무리 봐도 '마법 소녀'라고 밖에 설명이 안 되잖아.", names[5], backgrounds[14], magic, magicanime,null));
        main_stream.Add(new sentence29("혼란스럽지만... 일단은 그 녀석이 말 한 대로 외계인들을 무찔러 보자!", names[5], backgrounds[14], magic2, magicanime,null));
        main_stream.Add(new sentence29(null, names[8], backgrounds[14], animemove,null,black));

        List<music> bgmList = new List<music>();
        bgmList.Add(new music(forBgm[0], 0));
        
        bgmList.Add(new music(forBgm[1], 7));
        bgmList.Add(new music(null, 18));
        bgmList.Add(new music(forBgm[2], 19));
        bgmList.Add(new music(forBgm[3], 27));
       
        bgmList.Add(new music(forBgm[2], 40));

        bgms = new musics(bgm, bgmList);

        List<music> effectMusicList = new List<music>();
        effectMusicList.Add(new music(forEffectMusic[11], 2));
        effectMusicList.Add(new music(forEffectMusic[1], 8));
        effectMusicList.Add(new music(forEffectMusic[2], 12));
        effectMusicList.Add(new music(forEffectMusic[3], 17));
        effectMusicList.Add(new music(forEffectMusic[6], 19));
        effectMusicList.Add(new music(forEffectMusic[7], 22));
        effectMusicList.Add(new music(forEffectMusic[8], 23));

        effectMusicList.Add(new music(forEffectMusic[10], 26));
        effectMusicList.Add(new music(forEffectMusic[7], 28));
        effectMusicList.Add(new music(forEffectMusic[0], 30));
        effectMusicList.Add(new music(forEffectMusic[9], 32));
        effectMusicList.Add(new music(forEffectMusic[6], 34));
        effectMusicList.Add(new music(forEffectMusic[7], 37));
        effectMusicList.Add(new music(forEffectMusic[0], 40));
        effectMusicList.Add(new music(forEffectMusic[4], 43));

        effectMusics = new musics(effectMusic, effectMusicList);
    }
    public void Choiced(int order)
    {
        Debug.Log(order + "/" + length + "/" + timing1);
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
        background.SetActive(false);
        line.SetActive(false);
        line2.SetActive(false);
        black.SetActive(false);
        animemove.SetActive(false);
        rr.SetActive(false);
        rr2.SetActive(false);
        road.SetActive(false);
        errorr.SetActive(false);
        magic.SetActive(false);
        magic2.SetActive(false);
        magic3.SetActive(false);
        magicanime.SetActive(false);

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