using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip normalBGMClip;
    public AudioClip matchClip;
    public AudioClip failClip;
    public AudioClip gameOverClip;
    public AudioClip hurryUpClip;
    public AudioClip shuffleClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayMatchSFX() => PlaySFX(matchClip);
    public void PlayFailSFX() => PlaySFX(failClip);
    public void PlayGameOverSFX() => PlaySFX(gameOverClip);
    public void PlayShuffleSFX() => PlaySFX(shuffleClip);

    void PlayBGM(AudioClip clip, bool loop = true)
    {
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }

    public void StopBGM() => bgmSource.Stop();

    public void PlayNormalBGM()
    {
        if (!bgmSource.isPlaying)
            PlayBGM(normalBGMClip, true);
    }

    public void PlayHurryUpBGM()
    {
        PlayBGM(hurryUpClip, false);
    }

    public void ControlBGM(bool play) //BGM을 퍼즈, 재생하는 메서드
    {
        if (play)
        {
            bgmSource.UnPause();
        }
        else
        {
            bgmSource.Pause();
        }
    }

    //셔플 SFX 재생. 카드가 펼쳐지는 타이밍에 맞춰 두번 연속 재생
    public void PlayShuffleSound()
    {
        PlaySFX(shuffleClip);
        StartCoroutine(PlayClipDelayed(shuffleClip, 0.6f));
    }

    IEnumerator PlayClipDelayed(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        PlaySFX(clip);
    }
}
