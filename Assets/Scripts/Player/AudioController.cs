using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("Background Music")]
    public AudioClip backgroundAudioClip;
    public AudioClip chatter;

    [Header("SFX")]
    public AudioClip doorOpenAudioClip;
    public AudioClip correctQuestion;
    public AudioClip wrongQuestion;
    public AudioClip bookPageFlip;

    private AudioSource backgroundSource;
    private AudioSource chatterSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Singleton
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Create AudioSources immediately
        backgroundSource = gameObject.AddComponent<AudioSource>();
        backgroundSource.loop = true;
        backgroundSource.volume = 0.35f;

        chatterSource = gameObject.AddComponent<AudioSource>();
        chatterSource.loop = true;
        chatterSource.volume = 0.3f;

        sfxSource = gameObject.AddComponent<AudioSource>();

        // Scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        // 🔥 IMPORTANT: scene 0 fix
        PlaySceneMusic();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlaySceneMusic();
    }

    void PlaySceneMusic()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        if (backgroundAudioClip != null)
        {
            if (backgroundSource.clip != backgroundAudioClip)
            {
                backgroundSource.clip = backgroundAudioClip;
                backgroundSource.Play();
            }
        }

        if (index == 2 && chatter != null)
        {
            if (!chatterSource.isPlaying)
            {
                chatterSource.clip = chatter;
                chatterSource.Play();
            }
        }
        else
        {
            if (chatterSource.isPlaying)
                chatterSource.Stop();
        }
    }

    public void PlaySound(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayDoor() => PlaySound(doorOpenAudioClip, 0.75f);
    public void CorrectAnswer() => PlaySound(correctQuestion, 0.75f);
    public void WrongAnswer() => PlaySound(wrongQuestion, 0.75f);
    public void BookFlip() => PlaySound(bookPageFlip, 0.5f);
}