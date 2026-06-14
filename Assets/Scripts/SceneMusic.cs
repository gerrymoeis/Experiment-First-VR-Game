using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    private void Start()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayLobbyMusic();
        }
    }
}
