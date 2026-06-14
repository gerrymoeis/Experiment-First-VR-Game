using UnityEngine;

public class WarehouseTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered)
            return;

        if (!other.CompareTag("MainCamera"))
            return;

        triggered = true;

        GameFlowController.Instance.ArriveWarehouse();
    }
}
