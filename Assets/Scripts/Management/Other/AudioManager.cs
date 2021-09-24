using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound S in Sounds)
        {
            S.Source = gameObject.AddComponent<AudioSource>();
            S.Source.clip = S.Clip;
            S.Source.volume = S.Volume;
            S.Source.pitch = S.pitch;
            S.Source.loop = S.Loop;
            S.Source.outputAudioMixerGroup = S.Output;
        }
    }

    public void Play(string Name)
    {
        Sound S = Array.Find(Sounds, Sound => Sound.Name == Name);
        if (S == null) return;
        S.Source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(Sounds, item => item.Name == sound);
        if (s == null) return;
        s.Source.Stop();
    }
}
