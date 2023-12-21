using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _sInstance;
    public static T Instance
    {
        get
        {
            if (_sInstance == null)
            {
                _sInstance = FindObjectOfType<T>();
            }

            return _sInstance;
        }
    }
}