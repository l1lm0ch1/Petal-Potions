using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PetalPouchController : MonoBehaviour
{
    [Header("Pouch Visual")]
    public PetalPouch pouchVisual;

    [Header("XR Setup")]
    public XRInteractionManager interactionManager;

    private PetalData currentSelectedPetal;
    private XRDirectInteractor handInteractor;

    public void SetSelectedPetal(PetalData petal)
    {
        currentSelectedPetal = petal;
        Debug.Log("Ausgewähltes Petal: " + petal.petalName);

        if (pouchVisual != null)
        {
            pouchVisual.UpdatePouchVisual(petal);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Controller")
        {
            int count = FlowerInventory.Instance.GetPetalCount(currentSelectedPetal);
            if (currentSelectedPetal == null || count == 0)
                return;

            XRDirectInteractor interactor = other.GetComponent<XRDirectInteractor>();
            if (interactor != null)
            {
                handInteractor = interactor;
                StartCoroutine(SpawnAndGrab(interactor));
            }
        }
    }

    private IEnumerator SpawnAndGrab(XRDirectInteractor interactor)
    {
        if (currentSelectedPetal.petalPrefab == null)
            yield break;

        Vector3 spawnPos = interactor.transform.position;
        Quaternion spawnRot = interactor.transform.rotation;

        GameObject petalInstance = Instantiate(currentSelectedPetal.petalPrefab, spawnPos, spawnRot);

        //yield return null; // 1 Frame warten

        XRGrabInteractable interactable = petalInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter(interactor, interactable);
        }

        FlowerInventory.Instance.RemovePetal(currentSelectedPetal, 1);
    }
}
