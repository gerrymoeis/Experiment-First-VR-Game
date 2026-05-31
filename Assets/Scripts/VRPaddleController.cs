using UnityEngine;
using UnityEngine.XR;

public class VRPaddleController : MonoBehaviour
{
    [Tooltip("Pilih controller yang akan melacak raket ini")]
    public XRNode controllerNode = XRNode.RightHand;

    private Vector3 lastPosition;
    public Vector3 Velocity { get; private set; }

    void FixedUpdate()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (device.isValid && device.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 currentPos))
        {
            // Hitung kecepatan sebelum update posisi (penting untuk fisika)
            Velocity = (currentPos - lastPosition) / Time.fixedDeltaTime;
            lastPosition = currentPos;

            // Update posisi & rotasi raket mengikuti controller
            transform.position = currentPos;

            if (device.TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion currentRot))
            {
                transform.rotation = currentRot;
            }
        }
    }
}
