using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] protected UIControl _uiControl;
    

    public virtual void Show()
    {
        _uiControl.Show();
    }

    public virtual void Hide()
    {
        _uiControl.Hide();
    }
    
    public virtual void HideImmediately()
    {
        _uiControl.HideImmediately();
    }
    
    public virtual void Close()
    {
        _uiControl.Close();
    }
}
