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
    [SerializeField] private Slider BGMSlider;	// ������ ������ Slider
    [SerializeField] private Toggle BGMMute;	// Mute�� On / Off�� Toggle

    [SerializeField] private Slider SFXSlider;	// ������ ������ Slider
    [SerializeField] private Toggle SFXMute;	// Mute�� On / Off�� Toggle

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

    // ����� ���� ����
    public void SetBGMVolume(float _volume)
    {
        BGMSlider.value = _volume;
        bgmVolume = _volume;  // �����̴� ���� ������ ����
        if (!isBGMMuted)  // ��Ʈ ���°� �ƴ� ���� ���� ����
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(bgmVolume) * 20);
        }
    }

    // ����� ��Ʈ ���
    public void ToggleBGMMute(bool toggle)
    {
        isBGMMuted = toggle;
        BGMMute.isOn = toggle;
        if (isBGMMuted)
        {
            audioMixer.SetFloat("BGMVolume", -80f);  // ���Ұ�
        }
        else
        {
            // �����̴��� ���� ���� ����Ͽ� ���� ����
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(BGMSlider.value) * 20);
        }
    }

    // ȿ���� ���� ����
    public void SetSFXVolume(float _volume)
    {
        SFXSlider.value = _volume;
        sfxVolume = _volume;  // �����̴� ���� ������ ����
        if (!isSFXMuted)  // ��Ʈ ���°� �ƴ� ���� ���� ����
        {
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
        }
    }

    // ȿ���� ��Ʈ ���
    public void ToggleSFXMute(bool toggle)
    {
        isSFXMuted = toggle;
        SFXMute.isOn = toggle;
        if (isSFXMuted)
        {
            audioMixer.SetFloat("SFXVolume", -80f);  // ���Ұ�
        }
        else
        {
            // �����̴��� ���� ���� ����Ͽ� ���� ����
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXSlider.value) * 20);
        }
    }



    public void PlayBGM(AudioClip clip) // soundData.bgm[(int)BGM.BOSS] << ����� �̷��� ���� �� ��
    {
        bgmPlayer.clip = clip;
        //bgmPlayer.volume = bgmVolume;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySound(AudioClip clip, Vector3 pos) // ������ ������ ����� ���� ��� ���
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolume);
    }
    /// <summary>
    /// ȿ���� ��� �Լ�
    /// </summary>
    /// <param name="clip">��ũ���ͺ� ������Ʈ�� SoundData �ȿ� ����� �ҽ��� ���ε� �� SoundData.Instance.soundData.clip�̸�</param>
    /// <param name="allowDuplicate">���� Ŭ���� ���ĵ� �Ǹ� true(default), �� �Ҹ��� ������ ������ ���� �Ҹ��� �ݺ����� �����Ÿ� false</param>
    public void PlaySFX(AudioClip clip, bool allowDuplicate = true) // �Ѱ� ���� �Ҹ��� ���ĵ� �Ǵ°� allowDuplicate�� true��, �Ҹ��� ��ġ�� �ȵǴ� ȿ������ false�� �ΰ� ���� ��
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

            // AudioSource�� outputAudioMixerGroup ����
            if (mixerGroup.Length > 0)
            {
                objS.outputAudioMixerGroup = mixerGroup[0];  // ù ��° �׷��� ���
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
    /// �ش� ȿ���� ���� ���� �Լ�
    /// </summary>
    /// <param name="clip">���� �����ϰ��� �� ����� Ŭ��</param>
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
