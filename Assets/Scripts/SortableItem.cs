using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class SortableItem : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Rigidbody rb;
    private XRGrabInteractable grab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        Debug.Log($"{gameObject.name} start pos saved: {startPosition}");
    }

    public void ResetItem()
    {
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        Debug.Log($"RESET ITEM DIPANGGIL: {gameObject.name}");

        if (grab != null)
            grab.enabled = false;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;

        // Tunggu 1 frame
        yield return null;

        // Tunggu physics step
        yield return new WaitForFixedUpdate();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.isKinematic = false;

        if (grab != null)
            grab.enabled = true;

        Debug.Log($"AFTER RESET POS: {transform.position}");
    }
}
