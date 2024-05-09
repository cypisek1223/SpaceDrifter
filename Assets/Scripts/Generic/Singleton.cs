using UnityEngine;

namespace SpaceDrifter2D
{
    public abstract class SingleInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected virtual void OnApplicationQuit() // Consider nullifying it on destroy aswell
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    public abstract class Singleton<T> : SingleInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            base.Awake();
        }
    }

    public abstract class PersistentSingleton<T> : Singleton<T> where T:MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}

