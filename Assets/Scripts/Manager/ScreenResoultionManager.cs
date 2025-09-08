using Sigleton;
using UnityEngine;

public class ScreenResoultionManager : PersistentMonoSingleton<ScreenResoultionManager >
{
    
    #region const

    private const string screenResW = "screenWidth";
    private const string screenResH = "screenHight";
    private const string screenFullscreen  = "isFullscreen";

    #endregion

    #region public

    public int Width => width;
    public int Height => height;
    public bool FullScreen => fullScreen;

    #endregion
    
    #region private
    private int width;
    private int height;
    private bool fullScreen = true;
    #endregion
    protected override void OnInitialize()
    {
        base.OnInitialize();
        fullScreen = PlayerPrefs.GetInt(screenFullscreen,fullScreen?1:0) == 1;
        width = PlayerPrefs.GetInt(screenResW, Screen.width);
        height = PlayerPrefs.GetInt(screenResH, Screen.height);
        SetResolution(width, height, fullScreen);

    }
    
    public void SetResolution(int width, int height, bool fullscreen)
    {
        this.width = width;
        this.height = height;
        this.fullScreen = fullscreen;
        PlayerPrefs.SetInt(screenResW, width);
        PlayerPrefs.SetInt(screenResH, height);
        PlayerPrefs.SetInt( screenFullscreen, fullScreen ? 1 : 0);
        Screen.SetResolution(width, height, fullscreen);
    }
    
    public void SetResolution( bool fullscreen)
    {
        SetResolution(width, height, fullscreen);
    }
    
    public void SetResolution(int width, int height)
    {
        SetResolution(width, height,fullScreen);
    }

}
