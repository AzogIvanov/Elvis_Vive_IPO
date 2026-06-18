using UnityEngine;

public class DeactivateObjects : MonoBehaviour
{
    [SerializeField] public GameObject[] objectsToDeactivate;

    public void DeactivateObjectsMethod()
    {
        foreach (GameObject obj in objectsToDeactivate)
        {
            if (obj != null)
                obj.SetActive(false);
        }
    }
}
