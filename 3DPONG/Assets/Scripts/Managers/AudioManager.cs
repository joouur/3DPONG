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

    public bool playMS;
    private bool p;

    private bool[] songs;
    private bool[] ambient;

    public void Start()
    {
        //Allocate clip arrays
        ballAudioClips.ballCollision = new AudioClip[3];
        musicAudioClips.playList = new AudioClip[3];
        musicAudioClips.ambient = new AudioClip[4];

        songs = new bool[musicAudioClips.playList.Length];
        ambient = new bool[musicAudioClips.ambient.Length];
        //load Clips
        ballAudioClips.ballCollision[0] = Resources.Load("Audio/SoundFX/scifi002") as AudioClip;
        ballAudioClips.ballCollision[1] = Resources.Load("Audio/SoundFX/scifi003") as AudioClip;
        ballAudioClips.ballCollision[2] = Resources.Load("Audio/SoundFX/scifi034") as AudioClip;

        musicAudioClips.playList[0] = Resources.Load("Audio/AmbientSound/ActualSongs/Cartoon feat. Jüri Pootsmann - I Remember U") as AudioClip;
        musicAudioClips.playList[1] = Resources.Load("Audio/AmbientSound/ActualSongs/Major Look - Legacy (feat. Alex Phillips)") as AudioClip;
        musicAudioClips.playList[2] = Resources.Load("Audio/AmbientSound/ActualSongs/Lupe Fiasco - Daydreamin") as AudioClip;

        musicAudioClips.ambient[0] = Resources.Load("Audio/AmbientSound/bennybomstaerk - b39 bibbeat number 8") as AudioClip;
        musicAudioClips.ambient[1] = Resources.Load("Audio/AmbientSound/bennybomstaerk - Fourteenth of September (adapted C-major)") as AudioClip;
        musicAudioClips.ambient[2] = Resources.Load("Audio/AmbientSound/enoe - Burned") as AudioClip;
        musicAudioClips.ambient[3] = Resources.Load("Audio/AmbientSound/enoe - What to do") as AudioClip;

        audioSource.clip = musicAudioClips.ambient[UnityEngine.Random.Range(0, musicAudioClips.ambient.Length)] as AudioClip;
        audioSource.Play();
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
        playMS = false;
        p = false;

        masterVolume = 100;
        musicVolume = 0.50f;
        soundVolume = 75;

    }

    public void LateUpdate()
    {
        if (!audioSource.isPlaying && !p)
        {
            PlayClip(playMS);
            audioSource.Play();
        }
    }

    #region Volume
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
    #endregion

    public AudioClip Playlist()
    {
        int i = UnityEngine.Random.Range(0, musicAudioClips.playList.Length);
        if (allSongs(songs))
        {
            songs = new bool[musicAudioClips.playList.Length];
            Playlist();
        }
        else
        {
            if (songs[i])
                Playlist();
            else
                songs[i] = true;
        }
        AudioClip a = musicAudioClips.playList[UnityEngine.Random.Range(0, musicAudioClips.playList.Length)] as AudioClip;
        return a;
    }
    
    public AudioClip AmbientMu()
    {
        int i = UnityEngine.Random.Range(0, musicAudioClips.ambient.Length);
        if (allSongs(ambient))
        {
            ambient = new bool[musicAudioClips.ambient.Length];
            AmbientMu();
        }
        else
        {
            if (ambient[i])
                AmbientMu();
            else
                ambient[i] = true;
        }
        AudioClip a = musicAudioClips.ambient[UnityEngine.Random.Range(0, musicAudioClips.ambient.Length)] as AudioClip;
        return a;
    }

    public void PlayClip(bool pL)
    {
        if (pL)
        {
            audioSource.clip = Playlist();
            playMS = true;
        }
        else
        {
            audioSource.clip = AmbientMu();
            playMS = false;
        }
    }
    public void PlayClip()
    {
        if (playMS)
            audioSource.clip = Playlist();
        else
            audioSource.clip = AmbientMu();
    }

    public void PauseAudio()
    {
        p = !p;

        if (p)
            audioSource.Pause();
        else if (!p)
            audioSource.Play();
    }

    public static bool allSongs(bool[] a)
    {
        foreach (bool b in a)
        {
            if (!b)
                return false;
        }
        return true;
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