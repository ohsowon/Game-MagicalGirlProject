using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scenego : MonoBehaviour
{
    public Image fadeImage;            // 전체 화면 덮는 검은 이미지
    public float fadeDuration = 1f;   // 페이드 시간


    private void Start()
    {
        // 시작 시 이미지 완전 투명하게 (혹시 유니티에서 안 설정했을 경우 대비)
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }
    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName)); // 클릭 시 페이드 아웃 후 씬 전환
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = timer / fadeDuration; // 점점 불투명해짐
            fadeImage.color = color;
            yield return null;
        }
        Debug.Log(" 씬 전환 시도 중: {sceneName}");
        SceneManager.LoadScene(sceneName); // 씬 전환
    }
}
