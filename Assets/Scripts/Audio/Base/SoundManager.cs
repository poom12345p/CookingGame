using System.Collections;
using ObjectPooling;
using PrimeTween;
using Sigleton;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : BaseSingleton<SoundManager>
{
    public float Volume => _volume;
    public float BGMVolume => _bgmVolume;
    
    [SerializeField]  AudioSourcePlayer  AudioSource;
    [SerializeField] private AudioSource BGMAudioSource;
    private float _volume;
    private float _bgmVolume;
    private Tween _tweenBGM;
    
    private const string SOUND_V_BGM = "BGM";
    private const string SOUND_V_SFX = "SFX";
    protected override void OnInitialize()
    {
        base.OnInitialize();
        SetBGMVolume(PlayerPrefs.HasKey(SOUND_V_BGM)? PlayerPrefs.GetFloat(SOUND_V_BGM, _bgmVolume):1f);
        SetVolume(PlayerPrefs.HasKey(SOUND_V_SFX)? PlayerPrefs.GetFloat(SOUND_V_SFX, _volume):1f);
      
    }

    public void PlayBGM(AudioClip clip)
    {
        _tweenBGM.Stop();
        
        if(!BGMAudioSource.isPlaying)BGMAudioSource.Play();
        
        _tweenBGM = Tween.AudioVolume(BGMAudioSource, 0f, 0.5f).OnComplete(() =>
             {
                 BGMAudioSource.clip = clip;
                 BGMAudioSource.Play();
                 Tween.AudioVolume(BGMAudioSource, _volume, 0.5f);
             }
         );
        
    }
    
    public void PlaySound(AudioClip clip)
    {
        StartCoroutine(IEPlaySound(clip));
    }
    
    private IEnumerator IEPlaySound(AudioClip clip)
    {
        var source = ObjectPoolManager.Instance.Get(AudioSource);
        source.PlaySound( clip,_volume);
        yield return new WaitForSeconds(clip.length);
        ObjectPoolManager.Instance.Release(source);
        
    }

    public void SetBGMVolume(float volume)
    {
        _bgmVolume = volume;
        BGMAudioSource.volume = volume;
        PlayerPrefs.SetFloat(SOUND_V_BGM, _bgmVolume);
    }

    public void SetVolume(float volume)
    {
        this._volume = volume;
        PlayerPrefs.SetFloat(SOUND_V_SFX, this._volume);
    }
}
