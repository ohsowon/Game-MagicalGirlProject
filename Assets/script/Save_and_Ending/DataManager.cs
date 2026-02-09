using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;



public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public PlayData nowdata;

    public static string currentScene;
    
    string path;

    public TextMeshProUGUI NotiText; 
    public float visibleTime = 2f;   
    public float fadeDuration = 1f;  

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        path = Application.persistentDataPath + "/";

        currentScene = SceneManager.GetActiveScene().name;

        nowdata = new PlayData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public class PlayData
    {
        public string LastScene;
        public int Stage1Score;
        public int Stage2Score;
        public int Stage3Score;
        public int VN2Score;
        public int VN3Score;

        public PlayData()
        {
            LastScene = DataManager.currentScene;

            if (TotalScoreManager.Instance != null)
            {
                Stage1Score = TotalScoreManager.Instance.GetScore(1);
                Stage2Score = TotalScoreManager.Instance.GetScore(2);
                Stage3Score = TotalScoreManager.Instance.GetScore(3);
                VN2Score = TotalScoreManager.Instance.GetScore(4);
                VN3Score = TotalScoreManager.Instance.GetScore(5);
            }
            else
            {
                Debug.Log("TotalScoreManager.Instance == null");
            }
        }
    }

    public void SaveData1()
    {
        string data = JsonUtility.ToJson(nowdata);
        File.WriteAllText(path + "save1", data);

        NotiText.text = "첫 번째 슬롯에 진행 상황이 저장되었습니다.";
        StartCoroutine(ShowAndFadeText());

        Debug.Log("첫번째 슬롯에 저장");
    }

    public void SaveData2()
    {
        string data = JsonUtility.ToJson(nowdata);
        File.WriteAllText(path + "save2", data);

        NotiText.text = "두 번째 슬롯에 진행 상황이 저장되었습니다.";
        StartCoroutine(ShowAndFadeText());

        Debug.Log("두번째 슬롯에 저장");
    }

    public void SaveData3()
    {
        string data = JsonUtility.ToJson(nowdata);
        File.WriteAllText(path + "save3", data);

        NotiText.text = "세 번째 슬롯에 진행 상황이 저장되었습니다.";
        StartCoroutine(ShowAndFadeText());

        Debug.Log("세번째 슬롯에 저장");
    }

    public void LoadData1()
    {
        Debug.Log("첫번째 슬롯 데이터 불러오기");

        string filePath = path + "save1";

        if (!File.Exists(filePath))
        {
            NotiText.text = "세이브 파일이 존재하지 않습니다.";
            StartCoroutine(ShowAndFadeText());

            Debug.LogWarning("첫번째 슬롯 세이브 파일이 존재하지 않습니다.");
            return; // 파일 없으면 아무 일도 안 함
        }

        string data = File.ReadAllText(filePath);
        nowdata = JsonUtility.FromJson<PlayData>(data);

        TotalScoreManager.Instance.SetScore(1, nowdata.Stage1Score);
        TotalScoreManager.Instance.SetScore(2, nowdata.Stage2Score);
        TotalScoreManager.Instance.SetScore(3, nowdata.Stage3Score);
        TotalScoreManager.Instance.SetScore(4, nowdata.VN2Score);
        TotalScoreManager.Instance.SetScore(5, nowdata.VN3Score);

        SceneManager.LoadScene(nowdata.LastScene);
    }

    public void LoadData2()
    {
        Debug.Log("두번째 슬롯 데이터 불러오기");

        string filePath = path + "save2";

        // 먼저 파일 존재 확인
        if (!File.Exists(filePath))
        {
            NotiText.text = "세이브 파일이 존재하지 않습니다.";
            StartCoroutine(ShowAndFadeText());

            Debug.LogWarning("두번째 슬롯 세이브 파일이 존재하지 않습니다.");
            return; // 파일 없으면 아무 일도 안 함
        }

        // 파일이 있을 때만 읽기
        string data = File.ReadAllText(filePath);
        nowdata = JsonUtility.FromJson<PlayData>(data);

        TotalScoreManager.Instance.SetScore(1, nowdata.Stage1Score);
        TotalScoreManager.Instance.SetScore(2, nowdata.Stage2Score);
        TotalScoreManager.Instance.SetScore(3, nowdata.Stage3Score);
        TotalScoreManager.Instance.SetScore(4, nowdata.VN2Score);
        TotalScoreManager.Instance.SetScore(5, nowdata.VN3Score);

        SceneManager.LoadScene(nowdata.LastScene);
    }

    public void LoadData3()
    {
        Debug.Log("세번째 슬롯 데이터 불러오기");

        string filePath = path + "save3";

        if (!File.Exists(filePath))
        {
            NotiText.text = "세이브 파일이 존재하지 않습니다.";
            StartCoroutine(ShowAndFadeText());

            Debug.LogWarning("첫번째 슬롯 세이브 파일이 존재하지 않습니다.");
            return; // 파일 없으면 아무 일도 안 함
        }

        string data = File.ReadAllText(filePath);
        nowdata = JsonUtility.FromJson<PlayData>(data);

        TotalScoreManager.Instance.SetScore(1, nowdata.Stage1Score);
        TotalScoreManager.Instance.SetScore(2, nowdata.Stage2Score);
        TotalScoreManager.Instance.SetScore(3, nowdata.Stage3Score);
        TotalScoreManager.Instance.SetScore(4, nowdata.VN2Score);
        TotalScoreManager.Instance.SetScore(5, nowdata.VN3Score);

        SceneManager.LoadScene(nowdata.LastScene);
    }

    IEnumerator ShowAndFadeText() // 안내문구 나타났다가 fade out
    {
        Color color = NotiText.color;
        color.a = 1f;
        NotiText.color = color;

        yield return new WaitForSeconds(visibleTime);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            color.a = alpha;
            NotiText.color = color;
            yield return null;
        }

        color.a = 0f;
        NotiText.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
