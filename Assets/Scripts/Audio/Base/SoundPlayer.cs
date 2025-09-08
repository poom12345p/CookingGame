using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;

    public void PlaySound()
    {
        SoundManager.Instance.PlaySound(_clip);
    }
}
