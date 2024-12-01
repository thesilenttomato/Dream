using UnityEngine;
using UnityEngine.Audio;

public class enemySound : MonoBehaviour
{
    public AudioClip hitSound;
    public SoundControl soundControl;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundControl = GameObject.FindFirstObjectByType<SoundControl>();
    }
    public void Sound(float distance)
    {
        audioSource.PlayOneShot(hitSound);

        float volume = 1.0f;
        if (distance > 1)
        {
            volume = 1.0f - (distance - 1) / (25 - 1);
            volume = Mathf.Clamp(volume, 0.0f, 1.0f);
        }

        audioSource.volume = volume * 2 * soundControl.soundMult.currentVaule * 0.1f;
        //audioSource.volume = volume * 2;
        //Debug.Log("SB");
        //Destroy(gameObject, 0.5f);
    }
}
