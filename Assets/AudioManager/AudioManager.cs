using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    protected static AudioManager instance;
    private Dictionary<string, AudioSource> sounds;

    // Use this for initialization
    void Start()
    {
        instance = this;
        sounds = new Dictionary<string, AudioSource>();

        AudioSource[] soundObjects = Resources.FindObjectsOfTypeAll(typeof(AudioSource)) as AudioSource[];

        foreach (AudioSource sound in soundObjects)
        {
            if (!sound.gameObject.activeSelf)
                continue;
            if (sound.gameObject.transform.parent != null && !sound.gameObject.transform.parent.gameObject.activeSelf)
                continue;
            Debug.Assert(!sounds.ContainsKey(sound.name), $"Sound key is already present in dictionnary: {sound.name}");
            if(sound != null && !sounds.ContainsKey(sound.name))
            {
                sounds.Add(sound.name, sound);
            }
        }
    }

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void PlaySound(string _name)
    {
        AudioSource source = sounds[_name];
        if(source)
        {
            source.Play();
        } else
        {
            throw new System.Exception("Audio Source not found");
        }
    }

    public void PlaySoundRandom(string _name, int _random)
    {
        int rand = Random.Range(0, _random);
        PlaySound(_name + rand.ToString());
    }

    public void StopSound(string _name)
    {
        AudioSource source = sounds[_name];
        if (source)
        {
            source.Stop();
        }
        else
        {
            throw new System.Exception("Audio Source not found");
        }
    }

    public void StopAllSounds ()
    {
        foreach (KeyValuePair<string, AudioSource> entry in sounds)
        {
            entry.Value.Stop();
        }
    }

}