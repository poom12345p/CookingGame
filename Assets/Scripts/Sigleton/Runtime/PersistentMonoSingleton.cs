using UnityEngine;

namespace Sigleton
{
    public class PersistentMonoSingleton<T> : BaseSingleton<T> where T : PersistentMonoSingleton<T>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            DontDestroyOnLoad(this);
        }
    }
}