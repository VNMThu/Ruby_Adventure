using UnityEngine;

// <> denotes this is a generic class
public class GenericSingleton<T> : MonoBehaviour where T : Component
{
    // create a private reference to T instance
    private static T _instance;

    public static T Instance
    {
        get
        {
            // if instance is null
            if (_instance != null) return _instance;
            // find the generic instance
            Debug.Log("Singleton1");
            _instance = FindObjectOfType<T>();

            // if it's null again create a new object
            // and attach the generic instance
            if (_instance != null) return _instance;
            GameObject obj = new GameObject
            {
                name = typeof(T).Name
            };
            Debug.Log("Singleton2");

            _instance = obj.AddComponent<T>();
            return _instance;
        }
    }

    public virtual void Awake()
    {
        Debug.Log("Singleton:Call Awake "+gameObject.name);

        // create the instance
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Singleton:Call Destroy "+gameObject.name);
            Destroy(gameObject);
        }
    }
}