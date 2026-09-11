using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    // 배경음악 변경 시 사용
    public void ChangeBGM(AudioClip newClip)
    {
        bgmSource.Stop();
        bgmSource.clip = newClip;
        bgmSource.Play();
    }

    // 효과음 재생 시 사용
    public void PlayDamagedSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.time = 0f;
        sfxSource.volume = 0.5f;
        // PlayOneShot을 쓰면 하나의 소스에서 여러 효과음을 중첩해서 낼 수 있습니다.
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void PlayAhSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.time = 0.5f;
        sfxSource.volume = 1.5f;
        // PlayOneShot을 쓰면 하나의 소스에서 여러 효과음을 중첩해서 낼 수 있습니다.
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void PlayPeeSFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.PlayOneShot(sfxSource.clip);
    }
}
