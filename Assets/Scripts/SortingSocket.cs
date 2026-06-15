using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SortingSocket : MonoBehaviour
{
    private XRSocketInteractor socket;
    private bool counted = false;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void Update()
    {
        if (socket == null)
            return;

        if (!counted && socket.hasSelection)
        {
            counted = true;

            SortingManager.Instance.SocketFilled();
        }
    }
}
