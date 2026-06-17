using UnityEngine;

public class ActivationZone : MonoBehaviour
{
    private bool activated;
    public GameObject[] objectsToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        activated = true;

        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}