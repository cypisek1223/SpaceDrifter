using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDrifter2D
{
    public class SoundManager : PersistentSingleton<SoundManager>
    {
        [SerializeField]private AudioSource sfxAudioSource;

        protected override void Awake()
        {
            base.Awake(); //Fullfill PersistentSingleton functionality...
        }

        public void PlaySfx(AudioClip clip, float volume)
        {
            sfxAudioSource.PlayOneShot(clip, volume);
        }
    } 
}
