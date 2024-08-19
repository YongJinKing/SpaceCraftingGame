using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossNameUI : MonoBehaviour
{
    public Text bossEntranceText;
    public float fadeDuration = 1.5f; // 알파값을 변화시키는 시간
    public float shineDuration = 0.5f; // 빛나는 효과의 시간
    public Color shineColor = Color.red; // 빛날 때의 색상
    private Color originalColor;

    void OnEnable()
    {
        originalColor = bossEntranceText.color;
        StartCoroutine(FadeInAndShine());
    }

    IEnumerator FadeInAndShine()
    {
        // 1. 알파값을 0에서 1로 서서히 증가시키는 부분
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            bossEntranceText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // 2. 알파값이 1일 때 잠깐 빛나는 효과
        elapsedTime = 0f;
        while (elapsedTime < shineDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.PingPong(elapsedTime * 2, 1); // 빛나는 효과
            bossEntranceText.color = Color.Lerp(originalColor, shineColor, lerpValue);
            yield return null;
        }

        // 마지막에 원래 색상으로 확실히 고정
        bossEntranceText.color = originalColor;
    }
}
