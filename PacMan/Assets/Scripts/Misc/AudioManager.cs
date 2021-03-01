using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : Singleton<AudioManager>
{

    public enum Soundtypes
    {
        Ambient,
        Dialogue,
        Music,
        SoundEffects
    }

    public enum AudioClips
    {
        Beginning,
        Chomp,
        Death,
        EatGhost

    }

    

    private AudioSource m_AmbientAudioSource;
    private AudioSource m_DialogueAudioSource;
    private AudioSource m_MusicAudioSource;
    private AudioSource m_SoundEffectsAudioSource;
    public Dictionary<int, AudioSource> m_AudioSources = new Dictionary<int, AudioSource>();
    public void Intialize()
    {
        AudioSource[] audioSources = gameObject.GetComponents<AudioSource>();
        
        m_AmbientAudioSource = audioSources[0];
        m_AudioSources.Add((int)Soundtypes.Ambient,m_AmbientAudioSource );
        
        m_DialogueAudioSource = audioSources[1];
        m_AudioSources.Add((int)Soundtypes.Dialogue,m_DialogueAudioSource );
        
        m_MusicAudioSource = audioSources[2];
        m_AudioSources.Add((int)Soundtypes.Music,m_MusicAudioSource );
        
        m_SoundEffectsAudioSource = audioSources[3];
        m_AudioSources.Add((int)Soundtypes.SoundEffects,m_SoundEffectsAudioSource );

  
    }

    public AudioClip GetAudioClip(AudioClips aAudioClips)
    {
        string audioClipPath =  "";
        
        if (aAudioClips == AudioClips.Beginning)
        {
            audioClipPath = "Sound/pacman_beginning";
        }

        if (aAudioClips == AudioClips.Chomp)
        {
            audioClipPath = "Sound/pacman_chomp";
        }
        
        if (aAudioClips == AudioClips.Death)
        {
            audioClipPath = "Sound/pacman_death";
        }
        
        if (aAudioClips == AudioClips.EatGhost)
        {
            audioClipPath = "Sound/pacman_eatghost";
        }
        
 
        
        return  Resources.Load<AudioClip>(audioClipPath);;
    }

    public void PlaySoundRepeating(AudioClips aAudioClip,Soundtypes aSoundType)
    {
        
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        
        TempAudioSource.loop = true;
        TempAudioSource.clip = GetAudioClip(aAudioClip);
        TempAudioSource.Play();
        
    }

    public void PlaySoundOneShot(AudioClips aAudioClip,Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        

        
        TempAudioSource.loop = false;
        TempAudioSource.PlayOneShot(GetAudioClip(aAudioClip));
    }

    public void PlaySoundDelayed(AudioClips aAudioClip, float delay,Soundtypes aSoundType)
    {
        
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        
        TempAudioSource.loop = false;
        TempAudioSource.clip = GetAudioClip(aAudioClip);
        TempAudioSource.PlayDelayed(delay);
        
    }

    public void PlaySoundScheduled(AudioClips aAudioClip, double time,Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }

        TempAudioSource.loop = false;
        TempAudioSource.clip = GetAudioClip(aAudioClip);
         TempAudioSource.PlayScheduled(time);
        
    }

    public void StopSound(Soundtypes aSoundType)
    {
        AudioSource TempAudioSource = m_AudioSources[(int)aSoundType];

        if (TempAudioSource == null)
        {
            Debug.Log("Audio source: " + aSoundType + " is Null");
            return;
        }
        TempAudioSource.Stop();
    }
}