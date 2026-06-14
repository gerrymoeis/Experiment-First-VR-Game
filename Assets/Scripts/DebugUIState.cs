using UnityEngine;

public class DebugUIState : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("UI Gagal Interview ENABLED");
        Debug.Log(System.Environment.StackTrace);
    }
}
