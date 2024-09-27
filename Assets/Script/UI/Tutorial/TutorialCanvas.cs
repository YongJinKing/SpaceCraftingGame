using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCanvas : MonoBehaviour
{
    [SerializeField] int curIdx;
    public List<Image> tutoImgs;
    public Button prevBT;
    public Button nextBT;

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        curIdx = 0;
    }

    public void ChangeImage(int num)
    {
        int nxtIdx = curIdx + num;
        
        Debug.Log(curIdx + "<<<<<" + nxtIdx);
        if(nxtIdx == 0)
        {
            prevBT.interactable = false;
        }
        else
        {
            prevBT.interactable = true;
        }

        if (nxtIdx == tutoImgs.Count-1)
        {
            nextBT.interactable=false;
        }
        else
        {
            nextBT.interactable = true;
        }

        tutoImgs[curIdx].gameObject.SetActive(false);
        tutoImgs[nxtIdx].gameObject.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.UISound.OpenTabSFX);
        curIdx += num;
    }

    public void ExitCanvas()
    {
        Time.timeScale = 1.0f;

        Destroy(this.gameObject);
    }
}
