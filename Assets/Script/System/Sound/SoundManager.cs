using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] List<AudioSource> sfxPlayer = null;

    public BossSoundData bossSoundData;
    public PlayerSoundData playerSoundData;
    public UI_Sound UISound;
    public SpaceShipSondData spaceShipSondData;
    public BGMSoundData BGMSoundData;
    public BuildingSoundData buildingSoundData;
    public ItemSoundData itemSoundData;
    public static SoundManager Instance;

    public AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;	// 볼륨을 조절할 Slider
    [SerializeField] private Toggle BGMMute;	// Mute를 On / Off할 Toggle

    [SerializeField] private Slider SFXSlider;	// 볼륨을 조절할 Slider
    [SerializeField] private Toggle SFXMute;	// Mute를 On / Off할 Toggle

    public bool isBGMMuted = false;
    public bool isSFXMuted = false;

    float bgmVolume;
    float sfxVolume;

    float volume = 1f;
    private void Awake()
    {
        Instance = this;
        volume = 1f;
    }

    private void Start()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        BGMMute.onValueChanged.AddListener(ToggleBGMMute);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
        SFXMute.onValueChanged.AddListener(ToggleSFXMute);
    }

    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    // 배경음 볼륨 설정
    public void SetBGMVolume(float _volume)
    {
        BGMSlider.value = _volume;
        bgmVolume = _volume;  // 슬라이더 값을 변수에 저장
        if (!isBGMMuted)  // 뮤트 상태가 아닐 때만 볼륨 조정
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmVolume) * 20);
        }
    }

    // 배경음 뮤트 토글
    public void ToggleBGMMute(bool toggle)
    {
        isBGMMuted = toggle;
        BGMMute.isOn = toggle;
        if (isBGMMuted)
        {
            audioMixer.SetFloat("BGMVolume", -80f);  // 음소거
        }
        else
        {
            // 슬라이더의 현재 값을 사용하여 볼륨 복원
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMSlider.value) * 20);
        }
    }

    // 효과음 볼륨 설정
    public void SetSFXVolume(float _volume)
    {
        SFXSlider.value = _volume;
        sfxVolume = _volume;  // 슬라이더 값을 변수에 저장
        if (!isSFXMuted)  // 뮤트 상태가 아닐 때만 볼륨 조정
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        }
    }

    // 효과음 뮤트 토글
    public void ToggleSFXMute(bool toggle)
    {
        isSFXMuted = toggle;
        SFXMute.isOn = toggle;
        if (isSFXMuted)
        {
            audioMixer.SetFloat("SFXVolume", -80f);  // 음소거
        }
        else
        {
            // 슬라이더의 현재 값을 사용하여 볼륨 복원
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        }
    }



    public void PlayBGM(AudioClip clip) // soundData.bgm[(int)BGM.BOSS] << 브금은 이렇게 쓰면 될 듯
    {
        bgmPlayer.clip = clip;
        //bgmPlayer.volume = bgmVolume;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySound(AudioClip clip, Vector3 pos) // 생성과 삭제가 빈번한 사운드 출력 방법
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolume);
    }
    /// <summary>
    /// 효과음 재생 함수
    /// </summary>
    /// <param name="clip">스크립터블 오브젝트인 SoundData 안에 오디오 소스를 바인딩 후 SoundData.Instance.soundData.clip이름</param>
    /// <param name="allowDuplicate">앞의 클립이 겹쳐도 되면 true(default), 이 소리가 끝나기 전까지 같은 소리는 반복하지 않을거면 false</param>
    public void PlaySFX(AudioClip clip, bool allowDuplicate = true) // 총과 같이 소리가 겹쳐도 되는건 allowDuplicate를 true로, 소리가 겹치면 안되는 효과음은 false로 두고 쓰면 됨
    {
        bool clipPlayed = false;

        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (sfxPlayer[i] == null) continue;

            if (!sfxPlayer[i].isPlaying)
            {
                sfxPlayer[i].clip = clip;
                //sfxPlayer[i].volume = sfxVoume * volume;
                sfxPlayer[i].Play();
                clipPlayed = true;
                break;
            }

            if (!allowDuplicate && sfxPlayer[i].clip == clip)
            {
                clipPlayed = true;
                break;
            }
        }

        if (!clipPlayed)
        {
            var obj = new GameObject("SFXPlayer");
            AudioSource objS = obj.AddComponent<AudioSource>();
            obj.transform.SetParent(transform);
            AudioMixerGroup[] mixerGroup = audioMixer.FindMatchingGroups("SFX");

            // AudioSource의 outputAudioMixerGroup 설정
            if (mixerGroup.Length > 0)
            {
                objS.outputAudioMixerGroup = mixerGroup[0];  // 첫 번째 그룹을 사용
            }
            //objS.volume = sfxVoume * volume;
            objS.clip = clip;
            objS.loop = false;
            objS.playOnAwake = false;
            objS.Play();
            sfxPlayer.Add(objS);
        }

        ResetSFX();
    }
    /// <summary>
    /// 해당 효과음 강제 정지 함수
    /// </summary>
    /// <param name="clip">강제 정지하고자 할 오디오 클립</param>
    public void StopSFX(AudioClip clip)
    {
        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (sfxPlayer[i].isPlaying && sfxPlayer[i].clip == clip)
            {
                sfxPlayer[i].Pause();
            }
        }
    }

    void ResetSFX()
    {
        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (!sfxPlayer[i].isPlaying)
            {
                sfxPlayer[i].clip = null;
            }
        }
    }

    

}
