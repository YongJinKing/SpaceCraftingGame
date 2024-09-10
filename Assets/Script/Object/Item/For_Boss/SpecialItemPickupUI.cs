using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemPickupUI : MonoBehaviour
{
    public RectTransform uiPanel;  // UI �г��� RectTransform
    public CanvasGroup canvasGroup;  // UI �г��� CanvasGroup
    public float animationDuration = 1f;  // �ִϸ��̼� �ð�
    public Vector2 startPosition;  // UI �г��� ���� ��ġ
    public Vector2 endPosition;  // UI �г��� �� ��ġ

    public Image itemImage; // �гο� ������ ������ �̹���
    public Text itemNameText; // �гο� ������ ������ �̸� �ؽ�Ʈ
    public Text itemDescText; // �гο� ������ ������ ���� �ؽ�Ʈ
    public Button itemPickupBT; // �гο� ���� ��ư

    private void Start()
    {
        // UI �г��� ó���� ����� ���� ����
        uiPanel.anchoredPosition = startPosition;
        itemPickupBT.interactable = false;
        canvasGroup.alpha = 0f;
    }

    public void SetUpPannel(Sprite sprite, string name, string desc)
    {
        itemImage.sprite = sprite;
        itemNameText.text = name;
        itemDescText.text = desc;

        ShowPanel();
    }

    void ShowPanel()
    {
        // �г��� �����ֱ� ���� �ִϸ��̼� ����
        StartCoroutine(AnimatePanel());
    }

    private IEnumerator AnimatePanel()
    {
        float elapsedTime = 0f;
        Vector2 initialPosition = uiPanel.anchoredPosition;
        float initialAlpha = canvasGroup.alpha;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            // �г��� ��ġ�� ���� ���� ����
            uiPanel.anchoredPosition = Vector2.Lerp(initialPosition, endPosition, t);
            canvasGroup.alpha = Mathf.Lerp(initialAlpha, 1f, t);

            yield return null;
        }

        // �ִϸ��̼��� ���� �� ���� ���·� ����
        uiPanel.anchoredPosition = endPosition;
        canvasGroup.alpha = 1f;
        itemPickupBT.interactable = true;
    }

    

    public void PressSpecialItemPannelButton()
    {
        itemPickupBT.interactable = false;
        canvasGroup.alpha = 0f;
        uiPanel.anchoredPosition = startPosition;
    }
}
