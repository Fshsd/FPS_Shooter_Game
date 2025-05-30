using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public List<Sound> sounds;

    private Dictionary<string, Sound> soundDict = new Dictionary<string, Sound>();
    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // إعداد مصادر الصوت
            sfxSource = gameObject.AddComponent<AudioSource>();
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;

            // تحويل القائمة إلى قاموس للوصول السريع
            foreach (Sound s in sounds)
            {
                if (!soundDict.ContainsKey(s.name))
                    soundDict.Add(s.name, s);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // تشغيل موسيقى الخلفية تلقائيًا
        PlayMusic("background_music");
    }

    // لتشغيل مؤثر صوتي
    public void PlaySound(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound s = soundDict[name];
            sfxSource.PlayOneShot(s.clip, s.volume);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + name);
        }
    }

    // لتشغيل موسيقى الخلفية
    public void PlayMusic(string name)
    {
        if (soundDict.ContainsKey(name))
        {
            Sound s = soundDict[name];
            musicSource.clip = s.clip;
            musicSource.volume = s.volume;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music not found: " + name);
        }
    }

    // لإيقاف موسيقى الخلفية
    public void StopMusic()
    {
        musicSource.Stop();
    }
}
