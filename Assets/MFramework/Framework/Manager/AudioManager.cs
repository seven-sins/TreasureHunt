using UnityEngine;

namespace MFramework
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioListener mAudioListener;
        private void CheckAudioListener()
        {
            if (!mAudioListener)
            {
                mAudioListener = FindObjectOfType<AudioListener>();
            }
            if (!mAudioListener)
            {
                mAudioListener = gameObject.AddComponent<AudioListener>();
            }
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="soundName"></param>
        public void PlaySound(string soundName)
        {
            this.CheckAudioListener();

            var sound = Resources.Load<AudioClip>(soundName);
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.Play();
        }

        private AudioSource mMusicSource;

        /// <summary>
        /// 播放音乐
        /// </summary>
        /// <param name="musicName"></param>
        /// <param name="loop"></param>
        public void PlayMusic(string musicName, bool loop)
        {
            this.CheckAudioListener();

            if (!mMusicSource)
            {
                mMusicSource = gameObject.AddComponent<AudioSource>();
            }
            var coinClip = Resources.Load<AudioClip>(musicName);
            mMusicSource.clip = coinClip;
            mMusicSource.loop = loop;
            mMusicSource.Play();
        }
        /// <summary>
        /// 停止音乐
        /// </summary>
        public void StopMusic()
        {
            mMusicSource.Stop();
        }
        /// <summary>
        /// 暂停音乐
        /// </summary>
        public void PauseMusic()
        {
            mMusicSource.Pause();
        }
        /// <summary>
        /// 继续播放
        /// </summary>
        public void ResumeMusic()
        {
            mMusicSource.UnPause();
        }
        /// <summary>
        /// 音乐静音
        /// </summary>
        public void MusicOff()
        {
            mMusicSource.Pause();
            mMusicSource.mute = true;
        }
        /// <summary>
        /// 音效静音
        /// </summary>
        public void SoundOff()
        {
            var soundSources = GetComponents<AudioSource>();
            foreach(var soundSource in soundSources)
            {
                if(soundSource != mMusicSource)
                {
                    soundSource.Pause();
                    soundSource.mute = true;
                }
            }
        }
        /// <summary>
        /// 播放音乐
        /// </summary>
        public void MusicOn()
        {
            mMusicSource.UnPause();
            mMusicSource.mute = false;
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        public void SoundOn()
        {
            var soundSources = GetComponents<AudioSource>();
            foreach (var soundSource in soundSources)
            {
                if (soundSource != mMusicSource)
                {
                    soundSource.UnPause();
                    soundSource.mute = false;
                }
            }
        }
    }

}

