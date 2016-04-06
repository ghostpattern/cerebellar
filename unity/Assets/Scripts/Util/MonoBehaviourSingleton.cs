using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract bool DestroyOnLoad { get; }

    private static T _instance;

    private static readonly object _lock = new object();

    protected void Awake()
    {
        if(DestroyOnLoad == false)
        {
            DontDestroyOnLoad(gameObject);
        }

        _instance = Instance;

        InitSingletonInstance();
    }

    protected abstract void InitSingletonInstance();
    protected abstract void DestroySingletonInstance();

    public static T Instance
    {
        get
        {

            lock (_lock)
            {
                if(_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

#if DEBUG
                    if(_instance != null && FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                    }
#endif

                    if(_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T);
                    }
                }

                return _instance;
            }
        }
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    protected void OnDestroy()
    {
        DestroySingletonInstance();
    }
}