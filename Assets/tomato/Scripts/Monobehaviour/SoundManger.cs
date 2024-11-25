using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public AudioSource fightAudioSource;
    public AudioSource menuAudioSource;
    [Range(0f, 1f)] public float masterVolume = 1f;

    public void ChangeVolume(float volume)
    {
        fightAudioSource.volume = volume;
        menuAudioSource.volume = volume;
    }
    public void PlayBGM(bool playFight )
    {
        // 停止所有音频源
        StopAllBGM();
       
        
        if (!menuAudioSource.isPlaying && !playFight)
        {
            menuAudioSource.Play();
        }else if (!fightAudioSource.isPlaying && playFight)
        {
            fightAudioSource.Play();
        }
    }

    /// <summary>
    /// 停止所有背景音乐
    /// </summary>
    private void StopAllBGM()
    {
        if (fightAudioSource != null && fightAudioSource.isPlaying)
        {
            fightAudioSource.Stop();
        }

        if (menuAudioSource != null && menuAudioSource.isPlaying)
        {
            menuAudioSource.Stop();
        }
    }
}
