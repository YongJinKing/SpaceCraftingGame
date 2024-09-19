using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogue : MonoBehaviour
{
    [Header("보스 스테이지 연출용 이펙트 프리펩 배열"), Space(0.5f)]
    public GameObject[] bossEntranceVFX;

    [Header("보스 스테이지 연출용 이펙트 프리펩 배열 위치 오프셋"), Space(0.5f)]
    public Vector3[] bossEntranceVFXOffsets;

    public Text openningText;
    public Transform backgroundImg;
    public Transform myTarget;
    
    string dialogue;

    public string[] openningDialogues;
    public string[] dialogues;

    public int talkNum;
    int VFXNum;
    Vector2 screenPos;

    [SerializeField] protected Vector2 offSet;
    public void StartDialogue(string[] talks)
    {
        dialogues = talks;
        screenPos = Camera.main.WorldToScreenPoint(myTarget.transform.position);
        openningText.transform.position = screenPos + offSet;
        backgroundImg.position = screenPos + offSet;

        StartCoroutine(Typing(dialogues[talkNum]));
    }

    void NextDialogue()
    {
        openningText.text = null;
        talkNum++;

        if(talkNum == dialogues.Length)
        {
            talkNum = 0;
            return;
        }

        StartCoroutine(Typing(dialogues[talkNum]));
    }

    
    IEnumerator Typing(string talk)
    {
        openningText.text = null;

        if (VFXNum < bossEntranceVFX.Length)
        {
            Instantiate(bossEntranceVFX[VFXNum], myTarget.transform.position + bossEntranceVFXOffsets[VFXNum], Quaternion.identity, null);
            VFXNum++;
        }

        if (talk.Contains("  ")) talk = talk.Replace("  ", "\n");

        for(int i = 0; i < talk.Length; i++)
        {
            openningText.text += talk[i];
            SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.typingSFX);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);
        NextDialogue();
    }
}
