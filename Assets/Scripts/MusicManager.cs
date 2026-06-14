using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Music")]
    [SerializeField] private AudioClip lobbyMusic;

    [SerializeField] private AudioClip mainMusic1;

    [SerializeField] private AudioClip mainMusic2;

    [Header("Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    private float musicVolume = 0.5f;

    [SerializeField]
    private float fadeDuration = 2f;

    private AudioSource[] sources;

    private AudioSource currentSource;

    private AudioSource nextSource;

    private Coroutine playlistCoroutine;

    private bool playingLobby = false;

    private bool playingMain = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        sources = GetComponents<AudioSource>();

        if (sources.Length < 2)
        {
            Debug.LogError("MusicManager membutuhkan 2 AudioSource.");
            return;
        }

        currentSource = sources[0];
        nextSource = sources[1];

        currentSource.playOnAwake = false;
        nextSource.playOnAwake = false;

        currentSource.loop = false;
        nextSource.loop = false;

        currentSource.spatialBlend = 0;
        nextSource.spatialBlend = 0;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LobbyScene")
        {
            PlayLobbyMusic();
        }
        else if (scene.name == "MainVRScene")
        {
            PlayMainPlaylist();
        }
    }

    //-------------------------------------------------------

    public void PlayLobbyMusic()
    {
        if (playingLobby)
            return;

        playingLobby = true;
        playingMain = false;

        if (playlistCoroutine != null)
            StopCoroutine(playlistCoroutine);

        playlistCoroutine =
            StartCoroutine(FadeToClip(lobbyMusic, true));
    }

    //-------------------------------------------------------

    public void PlayMainPlaylist()
    {
        if (playingMain)
            return;

        playingMain = true;
        playingLobby = false;

        if (playlistCoroutine != null)
            StopCoroutine(playlistCoroutine);

        playlistCoroutine =
            StartCoroutine(MainPlaylistRoutine());
    }

    //-------------------------------------------------------

    private IEnumerator MainPlaylistRoutine()
    {
        bool startWithFirst = Random.value > 0.5f;

        AudioClip current =
            startWithFirst ? mainMusic1 : mainMusic2;

        AudioClip next =
            startWithFirst ? mainMusic2 : mainMusic1;

        while (true)
        {
            // Mulai lagu
            currentSource.clip = current;
            currentSource.loop = false;
            currentSource.volume = musicVolume;
            currentSource.Play();

            // Tunggu hingga 2 detik sebelum lagu selesai
            float waitTime = Mathf.Max(0f, current.length - fadeDuration);

            yield return new WaitForSeconds(waitTime);

            // Crossfade ke lagu berikutnya
            yield return FadeToClip(next, false);

            // Tukar urutan lagu
            Swap(ref current, ref next);
        }
    }

    //-------------------------------------------------------

    private IEnumerator FadeToClip(AudioClip clip, bool loop)
    {
        if (clip == null)
        {
            yield break;
        }

        nextSource.clip = clip;

        nextSource.loop = loop;

        nextSource.volume = 0f;

        nextSource.Play();

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;

            float t = timer / fadeDuration;

            nextSource.volume = t * musicVolume;

            currentSource.volume = (1f - t) * musicVolume;

            yield return null;
        }

        currentSource.Stop();

        currentSource.volume = 1f;

        AudioSource temp = currentSource;

        currentSource = nextSource;

        nextSource = temp;
    }

    private void Swap(ref AudioClip a, ref AudioClip b)
    {
        AudioClip temp = a;
        a = b;
        b = temp;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);

        currentSource.volume = musicVolume;

        if (nextSource.isPlaying)
        {
            nextSource.volume = musicVolume;
        }
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }
}
