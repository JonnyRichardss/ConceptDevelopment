using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPersistence : MonoBehaviour
{
    public static BuildingPersistence instance;
    private void Awake()
    {
        //makes sure gamemanager only exists once
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
