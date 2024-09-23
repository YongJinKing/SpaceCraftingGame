using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageBGMPlayer : MonoBehaviour
{
    public BGM bgm;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.Instance.BGMSoundData.bgm[(int)bgm]);
    }

    
}
