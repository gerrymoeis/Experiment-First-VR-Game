using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SortingSocket : MonoBehaviour
{
    private XRSocketInteractor socket;
    private bool isFilled = false;

    private void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    private void OnEnable()
    {
        socket.selectEntered.AddListener(OnItemInserted);
        socket.selectExited.AddListener(OnItemRemoved);
    }

    private void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnItemInserted);
        socket.selectExited.RemoveListener(OnItemRemoved);
    }

    private void OnItemInserted(SelectEnterEventArgs args)
    {
        if (isFilled)
            return;

        isFilled = true;
        SortingManager.Instance.SocketFilled();
    }

    private void OnItemRemoved(SelectExitEventArgs args)
    {
        if (!isFilled)
            return;

        isFilled = false;
        SortingManager.Instance.SocketEmptied();
    }

    public void ForceClear()
    {
        isFilled = false;
    }
}
