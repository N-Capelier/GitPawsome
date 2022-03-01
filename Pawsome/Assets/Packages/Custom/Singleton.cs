using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{

    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;
    
    /// <summary>
    /// Gets the instance of the Singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Debug.LogWarning($"Instance of type {typeof(T)} not found.");
                    return null;
                }
            }
            return instance;
        }
    }

    /// <summary>
    /// Makes the object a Singleton.
    /// </summary>
    protected bool CreateSingleton()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        else
        {
            Debug.LogWarning("Destroyed a non-unique gameObject named " + gameObject.name);
            Destroy(gameObject);
            return false;


        }
    }

    /// <summary>
    /// Makes the object a non-destroyable on load Singleton.
    /// </summary>
    protected bool CreateSingleton(bool isUndestroyableOnLoad)
    {
        if(CreateSingleton() && isUndestroyableOnLoad)
        {
            DontDestroyOnLoad(gameObject);
            return true;
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        instance = null;
        Destroy(gameObject);
    }
}