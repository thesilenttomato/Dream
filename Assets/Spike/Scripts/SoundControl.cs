using System.Collections;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip[] backgroundMusic;
    public AudioSource audioSource;
    public float fadeDuration = 5;
    public bool autoFadeInOnStart = true;
    private float time = 0;
    void Start()
    {
        if (!gameManager.bossFight[0] && !gameManager.bossFight[1] && !gameManager.bossFight[2])
        {
            int a = Random.Range(0, backgroundMusic.Length - 1);
            audioSource.clip = backgroundMusic[a];
        }
        else
        {
            audioSource.clip = backgroundMusic[3];
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartCoroutine(FadeIn(fadeDuration));
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 115)
        {
            time = 0;
            StartCoroutine(FadeOut(fadeDuration));
        }
    }
    public IEnumerator FadeIn(float duration)
    {
        audioSource.volume = 0;
        audioSource.Play();

        float startVolume = audioSource.volume;
        float targetVolume = 0.5f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public IEnumerator FadeOut(float duration)
    {
        float startVolume = audioSource.volume;
        float targetVolume = 0.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
        audioSource.Stop();
    }
}
