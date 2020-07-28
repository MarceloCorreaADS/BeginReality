using UnityEngine;
using System.Collections;

namespace Utils
{
	public class SoundManager : MonoBehaviour {
        public AudioSource efxSource;
        public AudioSource musicSource;
        public static SoundManager instance = null;
        MapMethods mapMethods;

        public float lowPitchRange = .95f;
        public float highPitchRange = 1.05f;

        void Awake() {
            gameObject.AddComponent<MapMethods>();
            mapMethods = GetComponent<MapMethods>();
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
        public void ChangeSound(string sceneName) {
            if (musicSource.clip != mapMethods.mapMusic(sceneName)){
                musicSource.clip = mapMethods.mapMusic(sceneName);
                musicSource.Play();
            }
        }

        public void Sfx (string sfxName) {
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);
            efxSource.pitch = randomPitch;
            efxSource.clip = getClip(sfxName);
            efxSource.Play();
        }
        AudioClip getClip(string name) {
            return Resources.Load<AudioClip>("Sounds/" + name);
        }
    }
}

