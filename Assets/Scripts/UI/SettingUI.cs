using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    [SerializeField]Slider _bgmSlider;
    [SerializeField]Slider _SfxSlider;
    [SerializeField]Button closeBtn;

    private void Awake()
    {
        _bgmSlider.onValueChanged .AddListener( SoundManager.Instance.SetBGMVolume);
        _SfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetVolume);
        closeBtn.onClick.AddListener(Close);
    }

    public override void Show()
    {
        base.Show();
        _bgmSlider.SetValueWithoutNotify( SoundManager.Instance.BGMVolume);
        _SfxSlider.SetValueWithoutNotify(SoundManager.Instance.Volume);
    }

    private void OnDestroy()
    {
        _bgmSlider.onValueChanged.RemoveAllListeners();
        _SfxSlider.onValueChanged.RemoveAllListeners();
    }
}
