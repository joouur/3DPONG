using UnityEngine;
using System;
using System.Collections;
using Pong.UI;
using UnityEngine.UI;

namespace Pong.Managers
{
    public class AudioManager : MonoBehaviour
    {

        public static AudioManager Instance;
        public static BallAudioClips ballAudioClips;
        public static MusicAudioClips musicAudioClips;

        [Range(0,100)]
        public int masterVolume;
        [Range(0,1)]
        public float musicVolume;
        [Range(0,100)]
        public int soundVolume;
        private int previousSoundVolume;

        private AudioSource audioSource;

        public bool playMS;
        private bool p;

        private bool[] songs;
        private bool[] ambient;

        public void Start()
        {
            //Allocate clip arrays
            ballAudioClips.ballCollision = new AudioClip[3];
            musicAudioClips.playList = new AudioClip[8];
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
            musicAudioClips.playList[3] = Resources.Load("Audio/AmbientSound/ActualSongs/Illenium - Afterlife (feat. ECHOS)") as AudioClip;
            musicAudioClips.playList[4] = Resources.Load("Audio/AmbientSound/ActualSongs/Illenium - Sleepwalker (feat. Joni Fatora)") as AudioClip;
            musicAudioClips.playList[5] = Resources.Load("Audio/AmbientSound/ActualSongs/M.I.A. - Bring The Noize") as AudioClip;
            musicAudioClips.playList[6] = Resources.Load("Audio/AmbientSound/ActualSongs/TheFatRat - No No No") as AudioClip;
            musicAudioClips.playList[7] = Resources.Load("Audio/AmbientSound/ActualSongs/TheFatRat - The Calling (feat. Laura Brehm)") as AudioClip;

            musicAudioClips.ambient[0] = Resources.Load("Audio/AmbientSound/bennybomstaerk - b39 bibbeat number 8") as AudioClip;
            musicAudioClips.ambient[1] = Resources.Load("Audio/AmbientSound/bennybomstaerk - Fourteenth of September (adapted C-major)") as AudioClip;
            musicAudioClips.ambient[2] = Resources.Load("Audio/AmbientSound/enoe - Burned") as AudioClip;
            musicAudioClips.ambient[3] = Resources.Load("Audio/AmbientSound/enoe - What to do") as AudioClip;

            audioSource.clip = musicAudioClips.ambient[UnityEngine.Random.Range(0, musicAudioClips.ambient.Length)] as AudioClip;
            audioSource.Play();
            ScoreUI.Instance.StartCoroutine(ScoreUI.Instance.SongNames());
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
            if (playMS)
                PlayClip(playMS);
            else
                PlayClip(playMS);

        }

        #region Volume

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="v"></param>
        public void MasterVolumeChange(Slider v)
        {
            masterVolume = Convert.ToInt32(v.value);
            AudioListener.volume = (float)(masterVolume / 100.0f);
        }

        public void MasterVolumeChange(float v)
        {
            masterVolume = Convert.ToInt32(v);
            AudioListener.volume = (float)(masterVolume / 100.0f);
        }

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="v"></param>
        public void MusicVolumeChange(Slider v)
        {
            musicVolume = v.value;
            audioSource.volume = musicVolume;
        }

        public void MusicVolumeChange(float v)
        {
            musicVolume = v;
            audioSource.volume = musicVolume;
        }

        /// <summary>
        /// Deprecated
        /// </summary>
        /// <param name="v"></param>
        public void SoundVolumeChange(Slider v)
        {
            soundVolume = Convert.ToInt32(v.value);
        }
        public void SoundVolumeChange(float v)
        {
            soundVolume = Convert.ToInt32(v);
        }

        #endregion

        #region MusicControl
        public AudioClip Playlist()
        {
            int i = UnityEngine.Random.Range(0, musicAudioClips.playList.Length);
            if (allSongs(songs))
            {
                songs = new bool[musicAudioClips.playList.Length];
                //Debug.Log(string.Format("{0} {1} {2}", songs[0], songs[1], songs[2]));
            }
            else
            {
                while (songs[i] == true)
                {
                    i = UnityEngine.Random.Range(0, musicAudioClips.playList.Length);
                }
                songs[i] = true;
            }
            AudioClip a = musicAudioClips.playList[i] as AudioClip;
            return a;

        }

        public AudioClip AmbientMu()
        {
            int i = UnityEngine.Random.Range(0, musicAudioClips.ambient.Length);
            if (allSongs(ambient))
            {
                ambient = new bool[musicAudioClips.ambient.Length];
                //Debug.Log(string.Format("{0} {1} {2}", songs[0], songs[1], songs[2]));
            }
            else
            {
                while (ambient[i] == true)
                {
                    i = UnityEngine.Random.Range(0, musicAudioClips.ambient.Length);
                }
                ambient[i] = true;
            }
            AudioClip a = musicAudioClips.ambient[i] as AudioClip;
            return a;
        }

        public void PlayClip(bool pL)
        {
            if (pL != playMS)
                playMS = pL;
        }

        public void PlayClip()
        {
            if (playMS)
                audioSource.clip = Playlist();
            else
                audioSource.clip = AmbientMu();
            //Debug.Log(audioSource.clip.name);

        }

        public void PauseAudio()
        {
            p = !p;

            if (p)
                audioSource.Pause();
            else if (!p)
                audioSource.Play();
        }

        public void MuteMasterVolume(bool pd)
        {
            //p = !p;

            if (pd)
                AudioListener.volume = 0;
            else if (!pd)
                AudioListener.volume = masterVolume / 100;
        }

        public void MuteMusicVolume(bool pd)
        {
            //p = !p;

            if (pd)
                audioSource.volume = 0;
            else if (!pd)
                audioSource.volume = musicVolume;
        }

        public void MuteSoundVolume(bool pd)
        {
            //p = !p;

            if (pd)
            {
                previousSoundVolume = soundVolume;
                soundVolume = 0;
            }
            else if (!pd)
            {
                soundVolume = previousSoundVolume;
            }
        }

        #endregion

        #region Helper Functions
        public static bool allSongs(bool[] a)
        {
            foreach (bool b in a)
            {
                if (!b)
                    return false;
            }
            return true;
        }

        public string GetSongName()
        {
            if (audioSource.clip.name == null)
            {
                //Debug.Log("Runnning null");
                return "Waiting for Audio.";
            }
            else
            {
                //Debug.Log("Runnning with name: " + audioSource.clip.name);
                return audioSource.clip.name;
            }
        }
        #endregion
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
}