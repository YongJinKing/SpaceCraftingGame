using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipOperator : MonoBehaviour
{
    public Text explainText;
    public string operatorText;

    public string[] dialogues;
    public int talkNum;
    public void OperatorExplain()
    {
        StartCoroutine(Typing(dialogues[talkNum]));
    }

    public void StopOperator()
    {
        explainText.text = string.Empty;
    }
    void NextDialogue()
    {
        explainText.text = null;
        talkNum++;

        if (talkNum == dialogues.Length)
        {
            Debug.Log("타이핑 끝");
            talkNum = 0;
            return;
        }

        StartCoroutine(Typing(dialogues[talkNum]));
    }

    IEnumerator Typing(string talk)
    {
        explainText.text = null;

        if (talk.Contains("  ")) talk = talk.Replace("  ", "\n");

        for (int i = 0; i < talk.Length; i++)
        {
            explainText.text += talk[i];
            if (talk[i] != ' ')SoundManager.Instance.PlaySFX(SoundManager.Instance.bossSoundData.typingSFX);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);
        NextDialogue();
    }
}
