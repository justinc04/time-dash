using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource[] sounds;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySound(int index)
    {
        sounds[index].Play();
    }
}
