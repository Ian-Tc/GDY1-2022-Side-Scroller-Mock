using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public SoundData[] sounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        InitializeSound();
        PlaySound("BGMusic");
    }

    void InitializeSound()
    {
        foreach (SoundData s in sounds)
        {
            if (s.associatedGameObject == null)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.soundClip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.playOnAwake = s.isPlayingOnAwake;
                s.source.loop = s.isLooping;

                s.source.spatialBlend = 0;
                s.source.minDistance = 0;
                s.source.maxDistance = 0;
            }
            else
            {
                s.source = s.associatedGameObject.AddComponent<AudioSource>();
                s.source.clip = s.soundClip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.playOnAwake = s.isPlayingOnAwake;
                s.source.loop = s.isLooping;

                s.source.spatialBlend = s.spatialBlend;
                s.source.minDistance = s.minHearingDistance;
                s.source.maxDistance = s.maxHearingDistance;
            }
            
        }
    }

    public void PlaySound(string name)
    {
        SoundData s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    public void StopSound(string name)
    {
        SoundData s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        SoundData s = Array.Find(sounds, sounds => sounds.name == name);

        if (s.source.isPlaying)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
