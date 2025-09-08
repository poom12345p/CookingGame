using UnityEngine;
using UnityEngine.UI;

public class pageMark : MonoBehaviour, IPoolable
{
    public GameObject OriginPref { get; set; }
    public Toggle Toggle;
    public void onGet()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        Toggle.isOn = false;
    }

    public void onRelease()
    {
        gameObject.SetActive(false);
        transform.SetParent(null);
    }

    public void onDestroy()
    {
    }
}
