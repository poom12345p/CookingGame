using UnityEngine;

public class AudioSourcePlayer : MonoBehaviour,IPoolable
{
    [SerializeField] private AudioSource _audioSource;

    public GameObject OriginPref { get; set; }
    public void onGet()
    {

    }

    public void onRelease()
    {

    }

    public void onDestroy()
    {
    }

    public void PlaySound(AudioClip clip,float volume)
    {
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(clip);
    }
}
