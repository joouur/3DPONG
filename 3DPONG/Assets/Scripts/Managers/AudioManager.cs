using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;
    public static BallAudioClips ballAudioClips;
    public static MusicAudioClips musicAudioClips;

    public int masterVolume;
    public float musicVolume;
    public int soundVolume;

    private AudioSource audioSource;
    public void Start()
    {
        //Allocate clip arrays
        ballAudioClips.ballCollision = new AudioClip[3];
        musicAudioClips.playList = new AudioClip[2];
        musicAudioClips.ambient = new AudioClip[5];
        //load Clips
        ballAudioClips.ballCollision[0] = Resources.Load("Audio/SoundFX/scifi002") as AudioClip;
        ballAudioClips.ballCollision[1] = Resources.Load("Audio/SoundFX/scifi003") as AudioClip;
        ballAudioClips.ballCollision[2] = Resources.Load("Audio/SoundFX/scifi034") as AudioClip;

        musicAudioClips.playList[0] = Resources.Load("Audio/AmbientSound/ActualSongs/Cartoon feat. Jüri Pootsmann - I Remember U") as AudioClip;
        musicAudioClips.playList[1] = Resources.Load("Audio/AmbientSound/ActualSongs/Major Look - Legacy (feat. Alex Phillips)") as AudioClip;


        masterVolume = 100;
        musicVolume = 0.75f;
        soundVolume = 75;

        audioSource.PlayOneShot(musicAudioClips.playList[0], musicVolume);
    }

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            Debug.Log("Audio Manager is already in play. Deleting old Instantiating new.");
        }
        else
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();        
    }

    public void MasterVolumeChange(Slider v)
    {
        masterVolume = Convert.ToInt32(v.value);
        AudioListener.volume = (float)(masterVolume / 100.0f);
    }

    public void MusicVolumeChange(Slider v)
    {
        musicVolume = v.value;
        audioSource.volume = musicVolume;
    }

    public void SoundVolumeChange(Slider v)
    {
        soundVolume = Convert.ToInt32(v.value);
    }
}

public struct BallAudioClips
{
    public AudioClip[] ballCollision;
}

public struct MusicAudioClips
{
    public AudioClip[] playList;
    public AudioClip[] ambient;
}