using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance { get; set; }
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }
            if (_instance == null)
            {
                Debug.LogError("_instance is null");
            }
            return _instance;
        }
    }
}
