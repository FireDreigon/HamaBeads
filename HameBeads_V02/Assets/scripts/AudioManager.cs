using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<GameObject> Audios;
    public GameObject Manager;  

    void Start()
    {
        DontDestroyOnLoad(Manager);
    }

public void CreateAudio(int IndexSound)
    {
        GameObject NewSound = Instantiate(Audios[IndexSound]);
        Destroy(NewSound, 1);
    }
}
