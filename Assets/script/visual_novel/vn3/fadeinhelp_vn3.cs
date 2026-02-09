using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeinhelp3 : MonoBehaviour
{
    public static fadeinhelp3 Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    public void StartFade(GameObject obj)
    {
        StartCoroutine(FadeIn(obj));
    }

    private IEnumerator FadeIn(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) yield break;

        float duration = 7f;
        float elapsed = 0f;
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