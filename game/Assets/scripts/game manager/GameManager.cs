using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance
    {
        get 
        {
            if (!_instance) _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
        private set => _instance = value;
    }
    private static GameManager _instance;


    void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
