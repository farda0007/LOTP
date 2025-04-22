using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip AudioPistolClip;
    public AudioClip Coin;
    public AudioClip Die;

    private AudioSource AudioSource;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    protected void PlayClip(AudioClip clip)
    {
        AudioSource.Stop();
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    public void PlayPistolClip()
    {
        PlayClip(AudioPistolClip);
    }
    public void PlayCoinClip()
    {
        PlayClip(Coin);
    }
    public void PlayDieClip()
    {
        PlayClip(Die);
    }
}
