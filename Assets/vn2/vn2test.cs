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

struct sentence2
{
    string text;
    Sprite name;
    Sprite background;
 

    GameObject standing1;
    GameObject standing2;
    GameObject standing3;

    string effect;

    public sentence2(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3, string effect)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = effect;
    }

    public sentence2(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = null;
    }

    public sentence2(string text, Sprite name, Sprite background, GameObject standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
    }

    public sentence2(string text, Sprite name, Sprite background)
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
            color.a = Mathf.Clamp01(1-elapsed / duration);
            sr.color = color;
            yield return null;
        }
        color.a = 0f;
        sr.color = color;
    }
}
public struct music200
{
    public AudioClip audio;
    public int timing;

    public music200(AudioClip audio, int timing)
    {
        this.audio = audio;
        this.timing = timing;
    }
}

public struct musics200
{
    public List<music> music;
    public int count;
    public AudioSource MusicPlayer;

    public musics200(AudioSource MusicPlayer, List<music> music)
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
        else if (length == music[count].timing)
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
   
struct choice2
{
    public string option1 { get; set; }
    public string option2 { get; set; }
    public int type { get; set; }

    List<sentence2> option1_sentence;
    List<sentence2> option2_sentence;

    int now;

    int score1;
    int score2;

    public choice2(string option1, string option2, List<sentence2> op1_s, List<sentence2> op2_S, int score1, int score2)
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


public class vn2test : MonoBehaviour
{
    List<sentence2> main_stream = new List<sentence2>();

    choice2 choice1;
    choice2 choice2;

    int timing1;
    int timing2;

    List<sentence2> choice1_1 = new List<sentence2>();
    List<sentence2> choice1_2 = new List<sentence2>();

    List<sentence2> choice2_1 = new List<sentence2>();
    List<sentence2> choice2_2 = new List<sentence2>();

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
        main_stream.Add(new sentence2("휴, 얼렁뚱땅이었지만...", names[5], backgrounds[8]));
        main_stream.Add(new sentence2("외계인을 전부 처리해서 다행이다.", names[5], backgrounds[8]));
        main_stream.Add(new sentence2("자, 그럼 이제...", names[5], backgrounds[8], rokis[3]));
        main_stream.Add(new sentence2("이 녀석의 정체부터 알아야겠지.", names[5], backgrounds[8], rokis[3]));
        main_stream.Add(new sentence2("“네 녀석, 이게 대체 무슨 상황이야?", names[5], backgrounds[8], rokis[3])); //5
        main_stream.Add(new sentence2("전부 설명해줘.", names[5], backgrounds[8], rokis[2]));
        main_stream.Add(new sentence2("...", names[7], backgrounds[8], rokis[2]));
        main_stream.Add(new sentence2("네, 제가 다 설명 해 드리겠습니다.", names[7], backgrounds[8], rokis[0]));
        main_stream.Add(new sentence2("이 녀석, 어째서 이렇게 비장한 표정을 하는 거야.", names[5], backgrounds[8], rokis[3]));//10
        main_stream.Add(new sentence2("녀석은 천천히 입을 연다. ", names[5], backgrounds[8], rokis[3]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[6],null, null, black));
        main_stream.Add(new sentence2("이 우주엔 수많은 행성이 존재하고, 대부분은 고유한 마법을 지니고 있답니다.", names[7], backgrounds[6]));
        main_stream.Add(new sentence2("불, 중력 시간 등... 각기 다른 방식으로 말이죠.", names[7], backgrounds[6]));
        main_stream.Add(new sentence2("하지만 지구는 어떤 마법도 가지지 못한 유일한 행성.", names[7], backgrounds[7],null,null,black));//15
        main_stream.Add(new sentence2("그런 지구를 침략하기 위해 호시탐탐 노리는 세력들이 많아요.", names[7], backgrounds[7]));
        main_stream.Add(new sentence2("따라서 저는, 지구를 지키기 위해 파견된 요원, 로키입니다.", names[7], backgrounds[7]));
        main_stream.Add(new sentence2("그리고 이 ‘마법소녀 프로젝트’ 에 선택된 사람이 바로 당신입니다!", names[7], backgrounds[7],line2));
        main_stream.Add(new sentence2("뭐!?", names[5], backgrounds[8], rokis[0],line,black));
        main_stream.Add(new sentence2("그렇다면, 내가 앞으로도 계속 마법소녀를 해야 한다고?", names[3], backgrounds[8], rokis[3]));//20
        main_stream.Add(new sentence2("이마를 짚으며 생각한다. 마법소녀라니, 나에겐 너무 버거운 임무라고...", names[3], backgrounds[8]));
        main_stream.Add(new sentence2("지금이라도 거절할까?", names[3], backgrounds[8]));
        



        main_stream.Add(new sentence2(".", names[7], backgrounds[13]));
       
        //  추가: 선택지 뜨는 위치 지정
        timing1 = main_stream.Count - 1;

        main_stream.Add(new sentence2(null, names[8], backgrounds[1],null, null, black));
        main_stream.Add(new sentence2("하... 드디어 학교 끝이다.", names[5], backgrounds[1]));//25
        main_stream.Add(new sentence2("또 아무와도 이야기하지 못 한 채 하루가 끝났어...", names[5], backgrounds[1]));
        main_stream.Add(new sentence2("특별한 힘은 개뿔. 내 일상도 바뀌지 않는데 무슨 특별한 힘이야...", names[5], backgrounds[1]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[11],null,null,black));
        main_stream.Add(new sentence2("편의점이 있네.", names[5], backgrounds[11]));
        main_stream.Add(new sentence2(" ...로키는 하루종일 내 방 옷장에 숨겨두었지", names[5], backgrounds[11]));//30
        main_stream.Add(new sentence2("다짜고짜 나를 외계인과 싸우게 시킨 배은망덕한 녀석이지만...", names[5], backgrounds[11]));
        main_stream.Add(new sentence2(" ...배고플텐데, 로키 간식이라도 사 갈까?", names[5], backgrounds[11]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[13]));

        main_stream.Add(new sentence2(null, names[8], backgrounds[13]));

        //  추가: 선택지 뜨는 위치 지정
        timing2 = main_stream.Count - 2;



        main_stream.Add(new sentence2(null, names[8], backgrounds[8], rr, null, black));//35
        main_stream.Add(new sentence2("삐빅-삐빅-", names[3], backgrounds[8], rr));
        main_stream.Add(new sentence2("어디선가, 경보음이 울린다.", names[3], backgrounds[8],rr));
        main_stream.Add(new sentence2("이, 이게 무슨 소리지?", names[5], backgrounds[8]));
        main_stream.Add(new sentence2("...!", names[7], backgrounds[8], rokis[6]));
        main_stream.Add(new sentence2("앗! 외계인입니다!", names[7], backgrounds[8], rokis[6]));
        main_stream.Add(new sentence2("뭐어!?", names[5], backgrounds[8], rokis[6],line,null));//41
        main_stream.Add(new sentence2("당장 출동해야 합니다!", names[7], backgrounds[8], rokis[3],line,null));
        main_stream.Add(new sentence2("이런, 너무 갑작스럽잖아...", names[5], backgrounds[8], rokis[3]));
        main_stream.Add(new sentence2("외계인은 준비할 시간을 주지 않습니다!", names[7], backgrounds[8], rokis[7]));
        main_stream.Add(new sentence2("윽, 또 변신해야 한다니.", names[3], backgrounds[8], rokis[7]));
        main_stream.Add(new sentence2("여전히 외계인은 무섭지만...", names[3], backgrounds[8], rokis[7]));
        main_stream.Add(new sentence2("준비 됐어!", names[5], backgrounds[8], rokis[7]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[13]));
        main_stream.Add(new sentence2(null, names[8], backgrounds[13], animemove,null,black));

        



        //  추가: choice1 분기 데이터 설정
        choice1_1.Add(new sentence2("하지만 어쩌겠는가. ", names[3], backgrounds[8], null, null, black));
        choice1_1.Add(new sentence2(" 내가 선택받았고, 지구가 위험하다는데...", names[3], backgrounds[8]));
        choice1_1.Add(new sentence2("결국, 마법소녀의 임무를 받아들인다.", names[3], backgrounds[8]));
        choice1_1.Add(new sentence2("...알겠어.", names[5], backgrounds[8], rokis[1]));
        choice1_1.Add(new sentence2("내 이름은 미소야. 김미소. 잘 부탁 해.", names[5], backgrounds[8], rokis[1]));
        choice1_1.Add(new sentence2("네, 저도 잘 부탁드립니다.", names[7], backgrounds[8], rokis[5]));
        choice1_1.Add(new sentence2("그리고, 마법소녀의 임무에 대해서라면 너무 걱정하지 마세요.", names[7], backgrounds[8], rokis[5]));
        choice1_1.Add(new sentence2("당신은 [ 특별한 힘 ] 을 가진 사람이니까요.", names[7], backgrounds[8], rokis[7]));
        choice1_1.Add(new sentence2(null, names[8], backgrounds[8], rokis[5]));
        choice1_1.Add(new sentence2("...", names[5], backgrounds[8]));
        choice1_1.Add(new sentence2("[ 특별한 힘 ] 이라...", names[5], backgrounds[8]));
        choice1_1.Add(new sentence2("무슨 소리인지, 참. 피곤하니 오늘은 이만 자자.", names[5], backgrounds[8]));
        choice1_1.Add(new sentence2("<유대감이 1 증가하였습니다.>", names[3], backgrounds[8]));




       
        choice1_2.Add(new sentence2("역시... 무리야! ", names[3], backgrounds[8], null, null, black));
        choice1_2.Add(new sentence2("마법소녀는 무리라고!", names[5], backgrounds[8], rokis[2]));
        choice1_2.Add(new sentence2("다른 사람 찾아.", names[5], backgrounds[8], rokis[2]));
        choice1_2.Add(new sentence2("... ...", names[5], backgrounds[8],rokis[2]));
        choice1_2.Add(new sentence2("이미 마법을 전달했어요.", names[7], backgrounds[8], rokis[3]));
        choice1_2.Add(new sentence2("거절은 불가능입니다.", names[7], backgrounds[8], rokis[0]));
        choice1_2.Add(new sentence2("그럴 수가...", names[5], backgrounds[8], rokis[2]));
        choice1_2.Add(new sentence2("그래도 걱정 마세요. 당신은 선택받은 특별한 힘을 가진 사람이니까요.", names[7], backgrounds[8], rokis[1]));
        choice1_2.Add(new sentence2("... ...", names[5], backgrounds[8], rokis[2]));
        choice1_2.Add(new sentence2(" ...그래, 마법 소녀라는 거. 거절한다고 되는 문제가 아니긴 하지", names[5], backgrounds[8]));
        choice1_2.Add(new sentence2("어쩔 수 없지만... 일단 받아들이는 수밖에.", names[5], backgrounds[8]));
        choice1_2.Add(new sentence2("...모르겠다.", names[5], backgrounds[8]));
        choice1_2.Add(new sentence2("...밤이 늦었으니 내일 생각하자.", names[5], backgrounds[8]));

        choice2_1.Add(new sentence2("결국 로키의 간식을 사 집으로 간다.", names[3], backgrounds[11], null, null, black));
        choice2_1.Add(new sentence2(null, names[8], backgrounds[8], null, null, black));
        choice2_1.Add(new sentence2("오셨군요!!", names[7], backgrounds[8], rokis[6]));
        choice2_1.Add(new sentence2("엇, 근데 들고 계신 건...?", names[7], backgrounds[8], rokis[6]));
        choice2_1.Add(new sentence2("로키와 함께 간식을 먹으며 도란도란 이야기를 나눈다.", names[3], backgrounds[8], rokis[4], null, black));
        choice2_1.Add(new sentence2("어쩐지... 이 녀석과 조금 친해진 기분이 든다.", names[3], backgrounds[8], rokis[5]));
        choice2_1.Add(new sentence2(" <유대감이 1 증가하였습니다.>", names[3], backgrounds[8], rokis[5]));


      

        choice2_2.Add(new sentence2("...", names[5], backgrounds[11], null, null, black));
        choice2_2.Add(new sentence2("흠, 아니야...", names[5], backgrounds[11]));
        choice2_2.Add(new sentence2("걘 외계 고양이니까 음식이 없어도 살 수 있을 거야.", names[5], backgrounds[11]));
        choice2_2.Add(new sentence2("그냥 집에 가자!", names[5], backgrounds[11]));
        choice2_2.Add(new sentence2(null, names[8], backgrounds[8], null, null, black));
        choice2_2.Add(new sentence2("나 왔어.", names[5], backgrounds[8]));


        List<music> bgmList = new List<music>();
        bgmList.Add(new music(forBgm[0], 0));
        bgmList.Add(new music(forBgm[1], 6));
        bgmList.Add(new music(forBgm[2], 9));
        bgmList.Add(new music(forBgm[2], 20));
        bgmList.Add(new music(forBgm[3], 22));
        bgmList.Add(new music(forBgm[4], 33));

        bgms = new musics(bgm, bgmList);

        List<music> effectMusicList = new List<music>();
        effectMusicList.Add(new music(forEffectMusic[0], 16));
        effectMusicList.Add(new music(forEffectMusic[1], 17));
        effectMusicList.Add(new music(forEffectMusic[4], 24));
        effectMusicList.Add(new music(forEffectMusic[2], 33));
        effectMusicList.Add(new music(forEffectMusic[5], 39));
        effectMusicList.Add(new music(forEffectMusic[3], 47));

        effectMusics = new musics(effectMusic, effectMusicList);

        List<music> choice1_1_bgmList = new List<music>();
        choice1_1_bgmList.Add(new music(forBgm[0], 0));   // choice1_1 시작부터 BGM4
        choice1_1_bgmList.Add(new music(forBgm[1], 2));   // choice1_1의 5번째 대사에서 BGM5
        choice1_1_bgms = new musics(bgm, choice1_1_bgmList);

        // choice1-2 BGM 리스트
        List<music> choice1_2_bgmList = new List<music>();
        choice1_2_bgmList.Add(new music(forBgm[1], 0));
        choice1_2_bgms = new musics(bgm, choice1_2_bgmList);

        // choice2-1 BGM 리스트
        List<music> choice2_1_bgmList = new List<music>();
        choice2_1_bgmList.Add(new music(forBgm[0], 0));
        choice2_1_bgms = new musics(bgm, choice2_1_bgmList);

        // choice2-2 BGM 리스트
        List<music> choice2_2_bgmList = new List<music>();
        choice2_2_bgmList.Add(new music(forBgm[0], 0));
        choice2_2_bgms = new musics(bgm, choice2_2_bgmList);

        


        // choice1-1 효과음
        List<music> choice1_1_effectList = new List<music>();
        choice1_1_effectList.Add(new music(forEffectMusic[7], 7)); // 예시
        choice1_1_effectList.Add(new music(forEffectMusic[6], 8)); // 예시
        choice1_1_effects = new musics(effectMusic, choice1_1_effectList);

        // choice1-2 효과음
        List<music> choice1_2_effectList = new List<music>();
        choice1_2_effectList.Add(new music(null, 6)); // 예시
        choice1_2_effects = new musics(effectMusic, choice1_2_effectList);

        // choice2-1 효과음
        List<music> choice2_1_effectList = new List<music>();
        choice2_1_effectList.Add(new music(forEffectMusic[5], 4));
        choice2_1_effects = new musics(effectMusic, choice2_1_effectList);

        // choice2-2 효과음
        List<music> choice2_2_effectList = new List<music>();
        choice2_2_effectList.Add(new music(null, 0));
        choice2_2_effects = new musics(effectMusic, choice2_2_effectList);

        //  추가: 선택지 생성 (점수 반영)
        choice1 = new choice2("1.마법소녀의 임무를 받아들인다.", " 2.마법소녀의 임무를 거절한다.", choice1_1, choice1_2, 1, 0);

        choice2 = new choice2("로키의 간식을 사간다.", "그냥 집에 간다.", choice2_1,choice2_2, 1,  0);           // score2
    }

    public void Choiced(int order)
    {

        if (length == timing1)
        {
            choice1.Clicked(order);
            if (order == 0)
            {
                score += choice1.GetScore1();
            }
            else if (order == 1)
            {
                score += choice1.GetScore2();
            }
            Debug.Log("choice1 선택. 현재 점수: " + score);
        }
        else if (length == timing2)
        {
            choice2.Clicked(order);
            if (order == 0)
            {
                score += choice2.GetScore1();
            }
            else if (order == 1)
            {
                score += choice2.GetScore2();
            }
            Debug.Log("choice2 선택. 현재 점수: " + score);
        }

        Choice_Panel.SetActive(false);
        Choice_Flag = true;
        //  Choiced 함수에서는 대사를 읽지 않고, OnClick에서 처리하도록 합니다.
    }

    public void OnClick()
    {
        Debug.Log("Current Length: " + length + ", Choice Flag: " + Choice_Flag);

        // 1. 선택지 분기 대사 진행 중인 경우
        // 1. 선택지 분기 대사 진행 중인 경우
        if (Choice_Flag)
        {
            pre_read();

            // 선택지1 분기
            if (length == timing1 && (choice1.type == 0 || choice1.type == 1))
            {
                musics currentChoiceBGM = choice1.type == 0 ? choice1_1_bgms : choice1_2_bgms;
                musics currentChoiceEffect = choice1.type == 0 ? choice1_1_effects : choice1_2_effects;

                // [수정] read 호출 후 반환값으로 선택지 종료 체크
                if (!choice1.read(Text, Name, background, currentChoiceBGM, currentChoiceEffect))
                {
                    Choice_Flag = false;
                    timing1 = -1;
                    length++;
                    bgms.check(length); // 메인 스트림 BGM으로 복귀
                    effectMusics.check(length);
                }
                
            }
            // 선택지2 분기
            else if (length == timing2 && (choice2.type == 0 || choice2.type == 1))
            {
                musics currentChoiceBGM = choice2.type == 0 ? choice2_1_bgms : choice2_2_bgms;
                musics currentChoiceEffect = choice2.type == 0 ? choice2_1_effects : choice2_2_effects;

                if (!choice2.read(Text, Name, background, currentChoiceBGM, currentChoiceEffect))
                {
                    Choice_Flag = false;
                    timing2 = -2;
                    length++;
                    bgms.check(length); // 메인 스트림 BGM으로 복귀
                    effectMusics.check(length);
                }
                

            }
            return;
        }

        // 2. 선택지 분기가 끝났거나, 메인 스트림 대사 진행
        if (length == timing1)
        {
            Choice_Panel.SetActive(true);
            choice1_1_effects.reset();
            choice1.create(option1, option2);

            // [추가] 선택지 분기 BGM 시작
            
            return;
        }
        else if (length == timing2)
        {
            Choice_Panel.SetActive(true);
            choice2.create(option1, option2);
            // [추가] 선택지 분기 BGM 시작
           
            return;
        }

        // 메인 스트림 대사 진행
        if (length < main_stream.Count)
        {
            next_text(length);
            // [수정] 메인 스트림 BGM 적용
            bgms.check(length);
            effectMusics.check(length);

            length++;
        }
        else
        {
            // --- 점수(유대감) 저장 ---
            TotalScoreManager.Instance.SetScore(4, score);
            Debug.Log("비주얼 노벨2 획득 유대감: " + score);
            // -------------------------

            Debug.Log("This Scene is end.");
            SceneManager.LoadScene("Stage2Scene"); // 추가: "Stage2Scene" 씬으로 전환
            return;
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