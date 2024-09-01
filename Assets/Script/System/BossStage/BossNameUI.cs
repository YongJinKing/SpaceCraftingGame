using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossNameUI : MonoBehaviour
{
    public Text bossEntranceText;
    public float fadeDuration = 1.5f; // ���İ��� ��ȭ��Ű�� �ð�
    public float shineDuration = 0.5f; // ������ ȿ���� �ð�
    public Color shineColor = Color.red; // ���� ���� ����
    private Color originalColor;
    public GameObject BossIntroVFX;
    public Vector2 VFXPos;

    void OnEnable()
    {
        originalColor = bossEntranceText.color;
        bossEntranceText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        StartCoroutine(FadeInAndShine());
        Instantiate(BossIntroVFX,VFXPos,Quaternion.identity);
    }

    IEnumerator FadeInAndShine()
    {
        // 1. ���İ��� 0���� 1�� ������ ������Ű�� �κ�
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            bossEntranceText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        // 2. ���İ��� 1�� �� ��� ������ ȿ��
        elapsedTime = 0f;
        while (elapsedTime < shineDuration)
        {
            elapsedTime += Time.deltaTime;
            float lerpValue = Mathf.PingPong(elapsedTime * 2, 1); // ������ ȿ��
            bossEntranceText.color = Color.Lerp(originalColor, shineColor, lerpValue);
            yield return null;
        }

        // �������� ���� �������� Ȯ���� ����
        bossEntranceText.color = originalColor;
    }
}
