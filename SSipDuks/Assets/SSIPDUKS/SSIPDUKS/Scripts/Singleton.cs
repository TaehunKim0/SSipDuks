using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance;

    public static T Instance
    {
        get
        {
            if(_Instance == null)
            {
                _Instance = FindObjectOfType(typeof(T)) as T;

                if(_Instance == null)
                {
                    Debug.LogError("There's no active " + typeof(T) + "in this scene");
                }
            }
            return _Instance;
        }
    }
}
