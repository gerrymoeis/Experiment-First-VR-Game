using UnityEngine;

public class WarehouseTrigger : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger terkena oleh : " + other.name);

        if (triggered)
            return;

        if (!other.CompareTag("Player"))
            return;

        triggered = true;

        Debug.Log("Player masuk gudang!");

        GameFlowController.Instance.ArriveWarehouse();
    }
}
