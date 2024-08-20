using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] List<AudioSource> sfxPlayer = null;

    public SoundData soundData;
    

    float volume = 1f;
    private void Awake()
    {
        Initialize();

        volume = 1f;
    }

    public void PlayBGM(AudioClip clip, float volumeMultiplier = 0.1f) // soundData.bgm[(int)BGM.BOSS] << ����� �̷��� ���� �� ��
    {
        bgmPlayer.clip = clip;
        bgmPlayer.volume = volumeMultiplier;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySound(AudioClip clip, Vector3 pos, float volumeMultiplier = 1f) // ������ ������ ����� ���� ��� ���
    {
        AudioSource.PlayClipAtPoint(clip, pos, volumeMultiplier * volume);
    }

    public void PlaySFX(AudioClip clip, bool allowDuplicate = true, float volumeMultiplier = 0.2f) // �Ѱ� ���� �Ҹ��� ���ĵ� �Ǵ°� allowDuplicate�� true��, �Ҹ��� ��ġ�� �ȵǴ� ȿ������ false�� �ΰ� ���� ��
    {
        bool clipPlayed = false;

        for (int i = 0; i < sfxPlayer.Count; i++)
        {
            if (!sfxPlayer[i].isPlaying)
            {
                sfxPlayer[i].clip = clip;
                sfxPlayer[i].volume = volumeMultiplier * volume;
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
            objS.volume = volumeMultiplier * volume;
            objS.clip = clip;
            objS.loop = false;
            objS.playOnAwake = false;
            objS.Play();
            sfxPlayer.Add(objS);
        }

        ResetSFX();
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
