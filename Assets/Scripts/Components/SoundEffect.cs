using System;
using SGSTools.Common;
using SGSTools.Extensions;
using UnityEngine;

namespace SGSTools.Components
{
    [Serializable]
    public class SoundEffect
    {
        public FloatRange Volume = new FloatRange(0f, 1f);
        public FloatRange Pitch = new FloatRange(1f, 1f);
        public AudioClip[] Clips;

        private int _lastPlayedClipIndex;

        public void PlayRandomSound(AudioSource soundSource, float intensity = 1f)
        {
            var clip = Clips.GetRandomElement();
            if (clip != null)
            {
                PlayClip(soundSource, clip, intensity);
            }
        }

        public void PlayNonRepeatingRandomSound(AudioSource soundSource, float intensity)
        {
            if (!Clips.IsNullOrEmpty())
            {
                _lastPlayedClipIndex = Clips.GetRandomIndexExcluding(_lastPlayedClipIndex);
                var clip = Clips[_lastPlayedClipIndex];
                PlayClip(soundSource, clip, intensity);
            }
        }

        public void PlayNonRepeatingRandomSound(AudioSource soundSource)
        {
            PlayNonRepeatingRandomSound(soundSource, 1f);
        }

        private void PlayClip(AudioSource soundSource, AudioClip clip, float intensity)
        {
            intensity = Mathf.Clamp01(intensity);
            var volume = Volume.GetValueAt(intensity);
            var pitch = Pitch.GetRandomValue();
            soundSource.pitch = pitch;
            soundSource.PlayOneShot(clip, volume);
        }

        public void PlayFirstClipWithoutOneShot(AudioSource soundSource)
        {
            soundSource.clip = Clips[0];
            soundSource.volume = Volume.Max;
            soundSource.Play();
        }
    }
}