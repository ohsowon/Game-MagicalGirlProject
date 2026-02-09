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

struct sentence3

{

    string text;
    Sprite name;
    Sprite background;

    GameObject standing1;
    GameObject standing2;
    GameObject standing3;

    public string effect;

    public AudioClip bgm;
    public AudioClip sfx;

    public sentence3(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3, string effect, AudioClip bgm, AudioClip sfx)
    {

        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = effect;
        this.bgm = bgm;
        this.sfx = sfx;
    }

    public sentence3(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3)
    {

        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 추가
        this.sfx = null; // 추가
    }

    public sentence3(string text, Sprite name, Sprite background, GameObject standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 추가
        this.sfx = null; // 추가
    }



    public sentence3(string text, Sprite name, Sprite background)
    {

        this.text = text;
        this.name = name;
        this.standing1 = null;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 추가
        this.sfx = null; // 추가
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
            fadeinhelp3.Instance.StartCoroutine(FadeIn(this.standing3));
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



struct choice3
{
    public string option1 { get; set; }
    public string option2 { get; set; }
    public int type { get; set; }


    List<sentence3> option1_sentence;
    List<sentence3> option2_sentence;

    int now;
    int score1;
    int score2;
    public choice3(string option1, string option2, List<sentence3> op1_s, List<sentence3> op2_S, int score1, int score2)
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
    public bool read(TextMeshProUGUI text, UnityEngine.UI.Image name, GameObject background)
    {

        if (type == 0)

        {
            if (now == option1_sentence.Count)
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
        if (now == getLegnth())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //  추가: 선택지 점수 getter
    public int GetScore1() { return score1; }
    public int GetScore2() { return score2; }

    // 추가: 현재 문장 정보를 미리 가져오는 함수
    public sentence3 GetCurrentSentence()
    {
        // 아직 대사가 남아있을 때만 정보를 반환
        if (!isEnd())
        {
            if (type == 0)
            {
                return option1_sentence[now];
            }
            else
            {
                return option2_sentence[now];
            }
        }
        // 대사가 끝났으면 빈 정보를 반환
        return new sentence3();
    }

}

public class vn3test : MonoBehaviour

{
    List<sentence3> main_stream = new List<sentence3>();
    choice3 choice1;
    choice3 choice2;

    int timing1;
    int timing2;

    List<sentence3> choice1_1 = new List<sentence3>();
    List<sentence3> choice1_2 = new List<sentence3>();
    List<sentence3> choice2_1 = new List<sentence3>();
    List<sentence3> choice2_2 = new List<sentence3>();

    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();
    public List<GameObject> rokis = new List<GameObject>();

    int length = 0;
    int what_choiced = -1;
    bool Choice_Flag = false;

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
    public GameObject rooptopanime;
    public GameObject particle;

    TextMeshProUGUI option1;
    TextMeshProUGUI option2;

    int score;

    // --- 추가된 부분 ---
    public GameObject videoBackground; // VideoBackground 오브젝트를 연결할 변수
    private UnityEngine.Video.VideoPlayer videoPlayer; // 비디오 플레이어 컴포넌트를 담을 변수
    private bool isVideoPlaying = false; // 지금 비디오가 재생 중인지 확인하는 변수                                        
    // --- 여기까지 ---

    // --- 추가된 부분 ---
    // 시간제 영상 재생 코루틴을 관리하기 위한 변수
    private Coroutine videoCoroutine = null;
    // --- 여기까지 ---

    // --- 오디오 관련 변수 추가 ---
    [Header("Audio Sources")]
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;

    [Header("Audio Clips")]
    public List<AudioClip> bgmClips;
    public List<AudioClip> sfxClips;
    // --- 여기까지 ---

    // Start is called before the first frame update
    void Start()
    {
        set_data();
        conpoenet_connect();
        // --- 추가된 부분 ---
        // videoBackground 오브젝트에서 VideoPlayer 컴포넌트를 찾아서 videoPlayer 변수에 넣기
        if (videoBackground != null)
        {
            videoPlayer = videoBackground.GetComponent<UnityEngine.Video.VideoPlayer>();
            // 영상 재생이 끝나면 OnVideoEnd 함수를 실행해달라고 등록하기
            videoPlayer.loopPointReached += OnVideoEnd;
            // 처음에는 비디오 화면을 꺼둠
            videoBackground.SetActive(false);
        }
        // --- 여기까지 ---

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
            choice2.create(option1, option2);  // timing2 선택지 표시
        }
    }

    void set_data()
    {
        //#13
        main_stream.Add(new sentence3("이번엔 좀 만만치 않은 적이었지만, 우리가 함께 해치웠어!", names[5], backgrounds[14], null, null, null, null, bgmClips[4], null));
        main_stream.Add(new sentence3("다행입니다!", names[7], backgrounds[14], rokis[5]));

        main_stream.Add(new sentence3(null, names[8], backgrounds[14], animemove,null,black,null,null, sfxClips[2]));

        // --- 수정된 부분 ---
        //텔레포트 영상 삽입하기
        // 비디오를 재생하라는 특별한 신호를 가진, 텍스트는 비어있는 문장을 추가합니다.
        //main_stream.Add(new sentence3("", names[7], null, rokis[5], null, null, "PLAY_VIDEO_3", null, null));//3초동안 영상 지속
        // --- 여기까지 ---

        //#14
        main_stream.Add(new sentence3("그로부터 일주일이 흘렀다.", names[3], backgrounds[13], null, null, null, null, bgmClips[0], null));
        main_stream.Add(new sentence3("외계인은 나타나지 않았고, 로키와는 계속 한 집에서 지내고 있다.", names[5], backgrounds[13]));
        main_stream.Add(new sentence3("외계인을 무찌를 때 마다 내 마력은 강해졌고, 로키는 마지막 강한 적이 처들어 올 때 까지 잘 수련하라고 했다.", names[5], backgrounds[13]));
        main_stream.Add(new sentence3(null, names[8], backgrounds[0], null, null, black));
        main_stream.Add(new sentence3("여전히 난 학교에서 혼자다.", names[5], backgrounds[0]));
        main_stream.Add(new sentence3("아무에게도 말을 걸지 못하고, 나에게 다가오는 사람도 없다.", names[5], backgrounds[0]));
        main_stream.Add(new sentence3("하지만 괜찮다. 집에 가면 로키가 있으니까...", names[5], backgrounds[0]));
        main_stream.Add(new sentence3("나에게 친구를 사귈 수 있는 [ 특별한 힘 ] 같은 건 없지만.", names[5], backgrounds[0]));
        main_stream.Add(new sentence3("로키와는 마법이라는 [ 특별한 힘 ] 으로 연결된 사이니까.", names[5], backgrounds[0], null, null, null, null, null, sfxClips[3]));
        main_stream.Add(new sentence3("그래서인지, 로키와 함께라면 나쁘지 않은 기분이야.", names[5], backgrounds[0]));
        main_stream.Add(new sentence3(null, names[8], backgrounds[8], null, null, black, null, null, sfxClips[4]));

        //#15
        main_stream.Add(new sentence3("다녀왔어~", names[5], backgrounds[8],null,null,null));
        main_stream.Add(new sentence3("앗, 다녀오셨습니까?", names[7], backgrounds[8], rokis[5]));
        main_stream.Add(new sentence3("하루종일 집에만 있어 심심해 보이는 로키.", names[3], backgrounds[8], rokis[1]));
        main_stream.Add(new sentence3("그런 로키와 같이 마법 연습을 하고 싶다. 로키에게 물어볼까?", names[3], backgrounds[8], rokis[1]));
        main_stream.Add(new sentence3(".", names[7], backgrounds[13]));

        //  추가: 선택지 뜨는 위치 지정
        timing1 = main_stream.Count - 1;

        //#16
        main_stream.Add(new sentence3(null, names[8], backgrounds[5], null, null, black, null, bgmClips[5], null));
        main_stream.Add(new sentence3("그날 밤, 옥상에서 로키와 나란히 하늘을 올려다본다.", names[3], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3("오늘따라 말이 없는 로키.", names[3], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3("...", names[5], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3("그동안 적들을 잘 무찔러 줘서 고맙습니다.", names[7], backgrounds[5], rokis[1]));
        main_stream.Add(new sentence3("별말씀을.", names[5], backgrounds[5], rokis[2])); ;
        main_stream.Add(new sentence3("...", names[7], backgrounds[5], rokis[1]));
        main_stream.Add(new sentence3("이번이 마지막 싸움이 될 겁니다.", names[7], backgrounds[5], rokis[1]));
        main_stream.Add(new sentence3("...!", names[5], backgrounds[5], rokis[1], line2, null, null, null,sfxClips[5])); ;
        main_stream.Add(new sentence3("당신은 이제 강하니까, 잘 해낼 거예요.", names[7], backgrounds[5], rokis[5]));
        main_stream.Add(new sentence3("...", names[5], backgrounds[5], rokis[5]));
        main_stream.Add(new sentence3("마지막이라니...", names[3], backgrounds[5], rokis[5]));
        main_stream.Add(new sentence3("굳게 다짐한 표정이지만, 어쩐지 쓸쓸해보인다.", names[3], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3("나 또한 그렇다.", names[3], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3("위험한 일들도 많았지만, 그동안 같이 지내면서 정이 쌓였다.", names[3], backgrounds[13]));
        main_stream.Add(new sentence3("로키에게 내 진심을 전달하고 싶다.", names[3], backgrounds[5], rokis[2],null, black));
        main_stream.Add(new sentence3("진심을 전달할까?", names[3], backgrounds[5], rokis[2]));
        main_stream.Add(new sentence3(null, names[8], backgrounds[13]));

        //#17
        //  추가: 선택지 뜨는 위치 지정
        timing2 = main_stream.Count - 1;

        //순간이동 애니메이션 삽입 stage1,2보다 오래 지속되도록
        main_stream.Add(new sentence3(null, names[8], backgrounds[14], animemove, null, black, null, null, sfxClips[2]));
        main_stream.Add(new sentence3(null, names[8], backgrounds[13], null, null, black));

        //  추가: choice1 분기 데이터 설정
        choice1_1.Add(new sentence3("로키, 나랑 옥상에서 마법 연습 하자.", names[5], backgrounds[8], rokis[1]));
        choice1_1.Add(new sentence3("네, 좋습니다.", names[7], backgrounds[8], rokis[5]));
        choice1_1.Add(new sentence3("둘은 그렇게 저녁 늦게까지 마법 연습을 한다.", names[3], backgrounds[10], null, null, black));
        choice1_1.Add(new sentence3("이제는 정말, 로키와 많이 친해진 기분이다.", names[3], backgrounds[10]));
        choice1_1.Add(new sentence3("<유대감이 1 증가하였습니다.>", names[3], backgrounds[10]));

        choice1_2.Add(new sentence3("...아니다, 피곤해.", names[5], backgrounds[8]));
        choice1_2.Add(new sentence3("마법 연습은 나중에 해야지", names[3], backgrounds[8]));
        choice1_2.Add(new sentence3("일단은, 다른 할 일을 좀 하자.", names[3], backgrounds[8]));

        //일러스트 이후 다른 걸로 대체 필요
        choice2_1.Add(new sentence3(null, names[8], backgrounds[15], rooptopanime, particle, black,null, bgmClips[6],null)); //둘이 나란히 하늘을 바라보는 일러스트
        choice2_1.Add(new sentence3("아쉽네.", names[5], backgrounds[15],rooptopanime,particle,null));
        choice2_1.Add(new sentence3("여전히 하늘을 바라보는 로키.", names[3], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("...", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("감정도 마법이려나?", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("...", names[7], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("기쁨, 슬픔 같은 감정은 과학으론 설명 못 하잖아.", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("너랑 헤어지기 싫다는 이 감정도, 네 마법 때문일지도..", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("...", names[7], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("그건, 제 마법이 아닙니다.", names[7], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("그럼?", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("말씀 드렸을텐데요.", names[7], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("당신에겐 [ 특별한 힘 ] 이 있다고...", names[7], backgrounds[15], rooptopanime, particle, null,null, null, sfxClips[3]));
        choice2_1.Add(new sentence3("...?", names[5], backgrounds[15], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("...뭐?", names[5], backgrounds[5], rooptopanime, particle, null));
        choice2_1.Add(new sentence3("...", names[7], backgrounds[15],rooptopanime, particle, null, null,null, sfxClips[6])); //
        choice2_1.Add(new sentence3("놈들이 옵니다.", names[7], backgrounds[15], rooptopanime, particle, null)); //
        choice2_1.Add(new sentence3("...마지막 전투를 준비 해 주세요.", names[7], backgrounds[13]));

        choice2_2.Add(new sentence3("진심을 전하고 싶지만 용기가 나지 않는다.", names[3], backgrounds[5], rokis[1]));
        choice2_2.Add(new sentence3("...", names[5], backgrounds[5], rokis[1]));
        choice2_2.Add(new sentence3("결국 아무 말도 꺼내지 못한다.", names[3], backgrounds[5], rokis[1]));
        choice2_2.Add(new sentence3("...", names[7], backgrounds[5], rokis[1]));
        choice2_2.Add(new sentence3("...!", names[7], backgrounds[5], rokis[3], null, null, null, null, sfxClips[6]));
        choice2_2.Add(new sentence3("준비해 주세요", names[7], backgrounds[5], rokis[0], line, null)); //
        choice2_2.Add(new sentence3("곧 놈들이 옵니다", names[7], backgrounds[5], rokis[3]));
        //choice2_2.Add(new sentence3(null, names[8], backgrounds[13], null, null, black));


        //  추가: 선택지 생성 (점수 반영)
        choice1 = new choice3("1.로키에게 마법 연습을 제안한다.", " 2.제안하지 않는다.", choice1_1, choice1_2, 1, 0);
        choice2 = new choice3("1.전달한다.", "2.전달하지 않는다", choice2_1, choice2_2, 1, 0);  // score2
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

        //  Choiced 함수에서는 대사를 읽지 않고, OnClick에서 처리하도록 합니다.
    }
    public void OnClick()
    {
        Debug.Log("Current Length: " + length + ", Choice Flag: " + Choice_Flag);

        // --- 추가된 부분 ---
        // 만약 비디오가 재생 중이라면?
        if (isVideoPlaying)
        {
            // 클릭하면 비디오를 중지하고 다음으로 넘어감

            OnVideoEnd(videoPlayer);

            return; // 함수를 여기서 끝냄
        }

        // --- 여기까지 ---
        // 1. 선택지 분기 대사 진행 중인 경우
        if (Choice_Flag)
        {
            pre_read();

            // --- 수정된 부분: 선택지 분기에서도 동영상 재생 처리 ---
            sentence3 currentChoiceSentence = new sentence3();
            if (length == timing1) currentChoiceSentence = choice1.GetCurrentSentence();
            else if (length == timing2) currentChoiceSentence = choice2.GetCurrentSentence();

            // --- 추가된 부분: 선택지 분기에서 오디오 재생 ---
            PlayAudioForSentence(currentChoiceSentence);
            // --- 여기까지 ---

            if (currentChoiceSentence.effect != null && currentChoiceSentence.effect.StartsWith("PLAY_VIDEO_"))
            {
                string[] parts = currentChoiceSentence.effect.Split('_');
                if (parts.Length == 3 && float.TryParse(parts[2], out float duration))
                {
                    // 코루틴을 시작하고, 이 클릭에 대한 처리는 종료
                    videoCoroutine = StartCoroutine(PlayVideoCoroutine(duration));

                    // 비디오 문장을 소비(다음으로 넘김)하기 위해 read()를 한번 호출
                    if (length == timing1) choice1.read(Text, Name, background);
                    else if (length == timing2) choice2.read(Text, Name, background);

                    return;
                }
            }
            // --- 여기까지 ---

            // length 기준으로 선택지 분기 처리
            if (length == timing1 && (choice1.type == 0 || choice1.type == 1))
            {
                if (!choice1.read(Text, Name, background))
                {
                    Choice_Flag = false;
                    timing1 = -1;
                    length++;
                }
            }
            else if (length == timing2 && (choice2.type == 0 || choice2.type == 1))
            {
                if (!choice2.read(Text, Name, background))
                {
                    Choice_Flag = false;
                    timing2 = -2;
                    length++;
                }
            }
            return; // 대사를 읽었으니 함수 종료
        }

        // 2. 선택지 분기가 끝났거나, 메인 스트림 대사 진행
        if (length == timing1)
        {
            Choice_Panel.SetActive(true);
            choice1.create(option1, option2);

            return;
        }
        else if (length == timing2)
        {
            Choice_Panel.SetActive(true);
            choice2.create(option1, option2);

            return;
        }

        // 메인 스트림 대사 진행
        if (length < main_stream.Count)
        {
            // --- 수정된 부분: 메인 스트림에서 오디오 재생 ---
            // 영상 처리 로직보다 먼저 오디오를 처리합니다.
            sentence3 currentSentence = main_stream[length];
            PlayAudioForSentence(currentSentence);
            // --- 여기까지 ---

            // --- 수정된 부분: 메인 스트림에서 동영상 재생 처리 ---
            // effect가 "PLAY_VIDEO_"로 시작하는지 확인
            if (main_stream[length].effect != null && main_stream[length].effect.StartsWith("PLAY_VIDEO_"))
            {
                // effect 문자열을 '_' 기준으로 자름 (예: "PLAY", "VIDEO", "3")
                string[] parts = main_stream[length].effect.Split('_');
                // 문자열이 올바른 형식이고, 마지막 부분이 숫자로 변환 가능한지 확인
                if (parts.Length == 3 && float.TryParse(parts[2], out float duration))
                {
                    length++; // 다음 문장을 가리키도록 미리 증가
                    videoCoroutine = StartCoroutine(PlayVideoCoroutine(duration)); // 시간제 재생 코루틴 시작
                    return; // 비디오를 재생했으니 함수를 여기서 끝냄
                }
            }
            // --- 여기까지 ---
            next_text(length);
            length++;
        }
        else
        {
            // --- 점수(유대감) 저장 ---
            TotalScoreManager.Instance.SetScore(5, score);
            Debug.Log("비주얼 노벨3 획득 유대감: " + score);
            // -------------------------

            Debug.Log("This Scene is end.");
            SceneManager.LoadScene("Stage3Scene"); // 추가: "Stage3Scene" 씬으로 전환
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
        rooptopanime.SetActive(false);
        particle.SetActive(false);

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


    // --- 추가된 부분: 오디오 재생 전용 함수 ---
    void PlayAudioForSentence(sentence3 sentence)
    {
        // 1. BGM 재생
        // 문장에 BGM이 지정되어 있고, 현재 재생 중인 BGM과 다를 경우
        if (sentence.bgm != null && bgmPlayer.clip != sentence.bgm)
        {
            bgmPlayer.clip = sentence.bgm;
            bgmPlayer.Play();
        }

        // 2. SFX 재생
        // 문장에 SFX가 지정되어 있을 경우
        if (sentence.sfx != null)
        {
            sfxPlayer.PlayOneShot(sentence.sfx);
        }
    }
    // --- 여기까지 ---

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTouchOnButton()) return; // 세이브 버튼을 눌렀을 때는 대사가 넘어가지 않도록함

            OnClick();
        }
    }
    // --- 추가된 부분 ---
    // 비디오를 재생하는 함수
    void PlayVideo()
    {

        // 대화창을 잠시 안보이게 함

        //GameObject.Find("Script_Panel").SetActive(false); // 필요하다면 이 코드의 주석을 푸세요



        // 비디오 배경을 켜고

        videoBackground.SetActive(true);

        // 비디오를 재생!

        videoPlayer.Play();

        // 지금은 비디오 재생 중이라고 표시

        isVideoPlaying = true;

    }



    // 비디오 재생이 끝났을 때 자동으로 실행될 함수

    // VideoPlayer를 매개변수로 받아야 이벤트 시스템이 제대로 작동해요.

    void OnVideoEnd(UnityEngine.Video.VideoPlayer vp)
    {
        // 만약 시간제 재생 코루틴이 실행 중이었다면, 중지시킴
        if (videoCoroutine != null)
        {
            StopCoroutine(videoCoroutine);
            videoCoroutine = null;
        }

        // 비디오 재생이 끝났다고 표시
        isVideoPlaying = false;
        // 비디오 플레이어 정지
        videoPlayer.Stop();
        // 비디오 배경을 다시 끔
        videoBackground.SetActive(false);
        // 대화창을 다시 보이게 함
        //GameObject.Find("Script_Panel").SetActive(true); // 필요하다면 이 코드의 주석을 푸세요

        // 다음 대사를 보여주기 위해 OnClick() 함수를 한 번 호출
        OnClick();

    }
    // --- 여기까지 ---

    // 지정된 시간만큼 비디오를 재생하는 코루틴
    private IEnumerator PlayVideoCoroutine(float duration)
    {
        // 일단 비디오 재생 시작
        PlayVideo();

        // 지정된 시간(duration)만큼 기다림
        yield return new WaitForSeconds(duration);

        // 기다린 후에도 비디오가 여전히 재생 중이라면 (사용자가 클릭해서 끄지 않았다면)
        // 비디오를 종료시킴
        if (isVideoPlaying)
        {
            OnVideoEnd(videoPlayer);
        }
    }
    // --- 여기까지 ---

}