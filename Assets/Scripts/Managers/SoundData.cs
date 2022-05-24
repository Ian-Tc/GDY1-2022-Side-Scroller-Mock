using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string name;
    [HideInInspector]public AudioSource source;
    public GameObject associatedGameObject;

    public AudioClip soundClip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool isLooping;
    public bool isPlayingOnAwake;

    [Range(0f, 1f)]
    public float spatialBlend; //0 is 2D, 1 is 3D, 3D has near and far effects

    [Range(0f, 100f)]
    public float minHearingDistance; //in units

    [Range(0f, 100f)]
    public float maxHearingDistance; //in units
}
