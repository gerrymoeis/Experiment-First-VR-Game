using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sourceA;
    [SerializeField] private AudioSource sourceB;

    [Header("Music")]
    [SerializeField] private AudioClip lobbyMusic;
    [SerializeField] private AudioClip mainMusic1;
    [SerializeField] private AudioClip mainMusic2;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayLobbyMusic()
    {
        if (sourceA.clip == lobbyMusic &&
            sourceA.isPlaying)
        {
            return;
        }

        sourceA.clip = lobbyMusic;
        sourceA.loop = true;
        sourceA.volume = 1f;
        sourceA.Play();
    }
}
