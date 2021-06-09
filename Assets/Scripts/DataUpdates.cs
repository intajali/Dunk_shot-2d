using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataUpdates : MonoBehaviour
{
    public const string SOUND_KEY = "sound";
    public const string VIBRATION_KEY = "vibration";
    public const string SCORE_KEY = "score";


    public static DataUpdates instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnSave(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }


    public string OnRetrive(string key)
    {
        string data = "";
        if(PlayerPrefs.HasKey(key))
        {
           data = PlayerPrefs.GetString(key);
        }

        return data;
    }

}
