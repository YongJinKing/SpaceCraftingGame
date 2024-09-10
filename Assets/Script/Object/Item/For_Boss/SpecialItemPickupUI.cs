using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemPickupUI : MonoBehaviour
{
    public RectTransform uiPanel;  // UI 패널의 RectTransform
    public CanvasGroup canvasGroup;  // UI 패널의 CanvasGroup
    public float animationDuration = 1f;  // 애니메이션 시간
    public Vector2 startPosition;  // UI 패널의 시작 위치
    public Vector2 endPosition;  // UI 패널의 끝 위치

    public Image itemImage; // 패널에 적용할 아이템 이미지
    public Text itemNameText; // 패널에 적용할 아이템 이름 텍스트
    public Text itemDescText; // 패널에 적용할 아이템 설명 텍스트
    public Button itemPickupBT; // 패널에 붙일 버튼

    InputController inputController;
    private void Start()
    {
        inputController = FindObjectOfType<InputController>();
        // UI 패널을 처음에 숨기기 위해 설정
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
        // 패널을 보여주기 위한 애니메이션 시작
        inputController.canMove = false;
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

            // 패널의 위치와 알파 값의 보간
            uiPanel.anchoredPosition = Vector2.Lerp(initialPosition, endPosition, t);
            canvasGroup.alpha = Mathf.Lerp(initialAlpha, 1f, t);

            yield return null;
        }

        // 애니메이션이 끝난 후 최종 상태로 설정
        uiPanel.anchoredPosition = endPosition;
        canvasGroup.alpha = 1f;
        itemPickupBT.interactable = true;
    }

    

    public void PressSpecialItemPannelButton()
    {
        inputController.canMove = true;
        itemPickupBT.interactable = false;
        canvasGroup.alpha = 0f;
        uiPanel.anchoredPosition = startPosition;
    }
}
