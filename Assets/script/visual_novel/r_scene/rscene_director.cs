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

struct sentence_r
{
    string text;
    Sprite name;
    Sprite background;


    GameObject standing1;
    GameObject standing2;
    GameObject standing3;

    string effect;

    // 추가된 오디오 클립 변수
    public AudioClip bgm;
    public AudioClip sfx;

    public sentence_r(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3, string effect, AudioClip bgm, AudioClip sfx)
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

    public sentence_r(string text, Sprite name, Sprite background, GameObject standing1, GameObject standing2, GameObject standing3)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = standing2;
        this.standing3 = standing3;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 오디오 없음
        this.sfx = null; // 오디오 없음
    }

    public sentence_r(string text, Sprite name, Sprite background, GameObject standing1)
    {
        this.text = text;
        this.name = name;
        this.standing1 = standing1;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 오디오 없음
        this.sfx = null; // 오디오 없음
    }

    public sentence_r(string text, Sprite name, Sprite background)
    {
        this.text = text;
        this.name = name;
        this.standing1 = null;
        this.standing2 = null;
        this.standing3 = null;
        this.background = background;
        this.effect = null;
        this.bgm = null; // 오디오 없음
        this.sfx = null; // 오디오 없음
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

            fadeinhelp_r.Instance.StartCoroutine(FadeIn(this.standing3));
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




public class rscene_director : MonoBehaviour
{
    // --- 오디오 관련 변수 추가 ---
    [Header("Audio Sources")]
    public AudioSource bgmPlayer;  // BGM을 재생할 오디오 소스
    public AudioSource sfxPlayer;  // SFX를 재생할 오디오 소스

    [Header("Audio Clips")]
    public List<AudioClip> bgmClips;   // BGM 오디오 클립 목록
    public List<AudioClip> sfxClips;   // SFX 오디오 클립 목록
    // ----------------------------

    List<sentence_r> main_stream = new List<sentence_r>();

    public List<Sprite> names = new List<Sprite>();
    public List<Sprite> backgrounds = new List<Sprite>();
    public List<GameObject> rokis = new List<GameObject>();

    int length = 0;

    UnityEngine.UI.Image Name;
    TextMeshProUGUI Text;
    GameObject Script_Panel;

    public GameObject roki;
    public GameObject boss;
    public GameObject roki_true_form;
    public GameObject background;
    public GameObject line;
    public GameObject black;
    public GameObject line2;
    public GameObject fire;
    public GameObject discardanime;

    // Start is called before the first frame update
    void Start()
    {
        set_data();
        conpoenet_connect();

        OnClick();

        roki.SetActive(false);
        boss.SetActive(false);
        roki_true_form.SetActive(false);
    }

    public void conpoenet_connect()
    {

        Name = GameObject.Find("Script_Panel").GetComponent<UnityEngine.UI.Image>();
        Text = GameObject.Find("Character_Text").GetComponent<TextMeshProUGUI>();

        roki = GameObject.Find("roki");
        boss = GameObject.Find("boss");
        roki_true_form = GameObject.Find("roki_true_form");

        // --- 오디오 플레이어 연결 코드 추가 ---
        // 만약 Inspector에서 직접 할당하지 않았을 경우를 대비해 코드로도 연결합니다.
        if (bgmPlayer == null)
            bgmPlayer = GameObject.Find("bgm").GetComponent<AudioSource>();
        if (sfxPlayer == null)
            sfxPlayer = GameObject.Find("effectMusic").GetComponent<AudioSource>();
        // ---------------------------------
    }

    void set_data()
    {
        // main_stream.Add(...) 부분을 수정해야 합니다.
        // new sentence_r(..., [BGM 클립], [SFX 클립]); 형식으로 추가합니다.
        // BGM을 바꾸고 싶을 때: bgmClips[인덱스]
        // SFX를 재생하고 싶을 때: sfxClips[인덱스]
        // 오디오 변경/재생이 필요 없으면: null


        main_stream.Add(new sentence_r("...해치웠나!", names[5], backgrounds[13], null, null, null, null, bgmClips[2], null));//0
        main_stream.Add(new sentence_r("크크큭...", names[0], backgrounds[13], null, null, null, null, null, sfxClips[5]));//0
        main_stream.Add(new sentence_r("...!?", names[5], backgrounds[13]));//0
        main_stream.Add(new sentence_r("아직 끝나지 않았다.", names[0], backgrounds[13], null, null, null, null, bgmClips[1], null));//0

        main_stream.Add(new sentence_r("우주선들의 잔해 속에서, 최종 보스의 모습이 드러난다.", names[3], backgrounds[13], null, null, black));
        main_stream.Add(new sentence_r("아하하하하!!", names[0], backgrounds[6], boss, null, black, null, null, sfxClips[6])); //5
        main_stream.Add(new sentence_r("나는 이 지구를 점령하러 온, '앙골 모아' 다!", names[1], backgrounds[6], boss, line2, null, null, null, sfxClips[3]));
        main_stream.Add(new sentence_r("지금까지는 잘 싸워 왔겠지만, 너희들의 힘으로 나를 무찌르는 건 무리다.", names[1], backgrounds[6], boss, line2, null));
        main_stream.Add(new sentence_r("얌전히 패배를 받아들여라!", names[1], backgrounds[6], boss, line2, null));//    8

        main_stream.Add(new sentence_r("...!", names[5], backgrounds[6], boss, null, black, null, bgmClips[2], sfxClips[4]));
        main_stream.Add(new sentence_r("전부 다 무찌른 줄 알았는데, 아직 최종 보스가 남아있었다니...", names[5], backgrounds[6], boss));
        main_stream.Add(new sentence_r("저걸 혼자 상대하긴... 무리야. 누가, 도와줘야...!", names[5], backgrounds[6], boss));
        main_stream.Add(new sentence_r("후후훗...", names[1], backgrounds[6], null, null, null, null, null, sfxClips[5]));//0
        main_stream.Add(new sentence_r("수고했다. 로키.", names[1], backgrounds[6], boss));
        //main_stream.Add(new sentence_r("", names[8], backgrounds[6], roki_true_form));
        main_stream.Add(new sentence_r("...!?", names[5], backgrounds[6], roki_true_form, line2, null, null, null, sfxClips[0]));
        main_stream.Add(new sentence_r("로키...? 그게 네 원래 모습이야?", names[5], backgrounds[6], roki_true_form));
        main_stream.Add(new sentence_r("그보다 수고했다니, 무슨 말이야...?", names[5], backgrounds[6], roki_true_form, null, null, null, null, sfxClips[7]));
        main_stream.Add(new sentence_r("계속 같이 지내던 녀석에게 배신당하다니, 불쌍하게 됐군", names[1], backgrounds[6], boss, null, null));
        main_stream.Add(new sentence_r("네게 마지막으로 진실을 알려주지.", names[1], backgrounds[6], boss, null, null));
        main_stream.Add(new sentence_r("", names[8], backgrounds[7], null, null, black, null, bgmClips[1],null));
        main_stream.Add(new sentence_r("네 녀석은 분명 ‘지구엔 마법이 없다’고 들었겠지?", names[1], backgrounds[7]));
        main_stream.Add(new sentence_r("로키가 그렇게 전달했으니까 말이야.", names[1], backgrounds[7]));
        main_stream.Add(new sentence_r("하지만 사실, 지구인에게도 마법이 있다!", names[1], backgrounds[7], null, null, null, null, null, sfxClips[4]));
        main_stream.Add(new sentence_r("그건 바로...‘마법을 전달하는 마법’!", names[1], backgrounds[7], line2, null, null, null, null,null));
        //불이 일렁이는 이미지 삽입
        main_stream.Add(new sentence_r("그 힘을 이용하면, 난 세상 모든 마법을 전달받고 세계 최고의 마법을 손에 넣을 수 있지", names[1], backgrounds[7], fire,line2,null));
        main_stream.Add(new sentence_r("그래서 널 이용한 거다.", names[1], backgrounds[7], null, null, fire));
        main_stream.Add(new sentence_r("이것이 '진짜' '마법소녀 프로젝트'다!! 크하하!!!", names[1], backgrounds[7], boss, line2, null, null, null, sfxClips[3]));
        //화면이 깨지는 연출 삽입
        main_stream.Add(new sentence_r("그럴수가...", names[5], backgrounds[6], boss, line2, black, null, bgmClips[2], null));
        main_stream.Add(new sentence_r("로키! 정말이야?", names[5], backgrounds[6], boss));
        main_stream.Add(new sentence_r("...", names[7], backgrounds[6], roki_true_form));
        main_stream.Add(new sentence_r("... ...", names[7], backgrounds[6], roki_true_form));
        main_stream.Add(new sentence_r("나를 속이다니...!", names[5], backgrounds[6],null, null, roki_true_form, null, null, sfxClips[4]));
        main_stream.Add(new sentence_r("으하하, 이것으로 마법소녀 놀이는 끝이다. 네 힘을 모조리 흡수해주마.", names[1], backgrounds[6], boss, null, null, null, null, sfxClips[5]));


        main_stream.Add(new sentence_r("그리고, 고맙게 생각해라.", names[1], backgrounds[6], boss));
        main_stream.Add(new sentence_r("우리의 마법이 없으면 아무 쓸모도 없는 지구의 마법을, 사용 해 준 것에 말이야...", names[1], backgrounds[6], boss, null, null, null, null, sfxClips[9]));
        main_stream.Add(new sentence_r("크윽, 이렇게 끝인가...", names[5], backgrounds[13], boss, null, null, null, null, sfxClips[9]));
        main_stream.Add(new sentence_r(null, names[8], backgrounds[14], null, null, black, null,null, sfxClips[13]));
        main_stream.Add(new sentence_r("...그건 틀렸습니다.", names[7], backgrounds[14], line2, null, null, null, bgmClips[3], sfxClips[8]));
        main_stream.Add(new sentence_r("!!", names[5], backgrounds[14], null, null, null, null, null, sfxClips[14]));
        main_stream.Add(new sentence_r(null, names[8], backgrounds[16], line2, null, black, null, null, sfxClips[7])); //로키가 막는 일러스트 삽입
        main_stream.Add(new sentence_r("...!?", names[5], backgrounds[16], null, null, null, null, null, sfxClips[14]));
        main_stream.Add(new sentence_r("로, 로키...?", names[5], backgrounds[16]));


        main_stream.Add(new sentence_r("우리의 마법이 없으면 쓸모 없다는 그 말, 틀렸습니다.", names[7], backgrounds[6], null, null, black, null, bgmClips[3], null));
        main_stream.Add(new sentence_r("지구인들은 감정이라는 마법을 서로 전달할 수 있어요.", names[7], backgrounds[6], rokis[0], line2, null));
        main_stream.Add(new sentence_r("뭐, 뭐라고? 이 자식이...", names[1], backgrounds[6], boss, null, null));
        main_stream.Add(new sentence_r("싸우고, 파괴하고. 권력만 쟁취하는 우리 외계 종족과는 다르다는 말입니다!", names[7], backgrounds[6], null, rokis[0], black, null, null, sfxClips[3]));
        main_stream.Add(new sentence_r("미소님!", names[7], backgrounds[6], rokis[7]));
        main_stream.Add(new sentence_r("저랑 동시에 '매지컬 리디엠션'을 외치면, 보스를 무찌를 수 있습니다!", names[7], backgrounds[6], rokis[0]));
        main_stream.Add(new sentence_r("뭐, 뭣이? 로키, 너 나를 배신하겠다는 거냐?", names[1], backgrounds[6], boss));
        main_stream.Add(new sentence_r("로키...!", names[5], backgrounds[6], rokis[2]));
        main_stream.Add(new sentence_r("그, 그래도 되겠어?", names[5], backgrounds[6], rokis[2]));
        main_stream.Add(new sentence_r("네. 저만 믿으세요.", names[7], backgrounds[6], rokis[5]));
        main_stream.Add(new sentence_r("같이 외치는 겁니다.", names[7], backgrounds[6], rokis[7]));
        main_stream.Add(new sentence_r("하나,", names[7], backgrounds[14]));
        main_stream.Add(new sentence_r("둘,", names[7], backgrounds[14]));
        main_stream.Add(new sentence_r("셋...!", names[7], backgrounds[14]));
        main_stream.Add(new sentence_r("매지컬 리디엠션...!", names[3], backgrounds[14], null, line2, black, null, null, sfxClips[2]));
        main_stream.Add(new sentence_r("로키, 감히 나를 배신하다니...", names[1], backgrounds[14], boss, null, null, null, null, sfxClips[11]));
        main_stream.Add(new sentence_r("으아아아아악!!!", names[1], backgrounds[14], discardanime, null, boss, null, null, sfxClips[3]));
        main_stream.Add(new sentence_r(null, names[8], backgrounds[14]));


    }


    public void OnClick()
    {
        // 메인 스트림 대사 진행
        if (length < main_stream.Count)
        {
            next_text(length);
            length++;
        }
        else
        {
            Debug.Log("This Scene is end.");
            SceneManager.LoadScene("VNEpilogue"); // 추가: 에필로그 씬으로 전환 -> 추후 변경
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
        fire.SetActive(false);
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
        //main_stream[length].read(Text, Name, background);
        // --- 오디오 재생 로직 추가 ---
        sentence_r currentSentence = main_stream[length];

        // 1. BGM 재생
        // 현재 문장에 BGM이 지정되어 있고, 현재 재생 중인 BGM과 다른 BGM일 경우
        if (currentSentence.bgm != null && bgmPlayer.clip != currentSentence.bgm)
        {
            bgmPlayer.clip = currentSentence.bgm; // 클립 교체
            bgmPlayer.Play(); // 재생
        }

        // 2. SFX 재생
        // 현재 문장에 SFX가 지정되어 있을 경우
        if (currentSentence.sfx != null)
        {
            // PlayOneShot은 기존에 재생되던 효과음을 멈추지 않고 겹쳐서 재생해줍니다. 효과음에 적합합니다.
            sfxPlayer.PlayOneShot(currentSentence.sfx);
        }
        // --------------------------

        // 기존의 화면 처리 로직 호출
        currentSentence.read(Text, Name, background);
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