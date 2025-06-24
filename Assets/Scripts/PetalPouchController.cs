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
    private XRBaseInteractor handInteractor;

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
            Debug.Log("Hand in Pouch");

            if (currentSelectedPetal == null || currentSelectedPetal.Inventory == 0)
                return;

            // Den XRBaseInteractor suchen (vom Controller)
            XRBaseInteractor interactor = other.GetComponentInParent<XRBaseInteractor>();
            if (interactor != null)
            {
                handInteractor = interactor;
                StartCoroutine(SpawnAndGrab(interactor));

                //TrySpawnPetalInHand(interactor);
            }
        }
    }

    private IEnumerator SpawnAndGrab(XRBaseInteractor interactor)
    {
        if (currentSelectedPetal.petalPrefab == null)
            yield break;

        Vector3 spawnPos = interactor.transform.position;
        Quaternion spawnRot = interactor.transform.rotation;

        GameObject petalInstance = Instantiate(currentSelectedPetal.petalPrefab, spawnPos, spawnRot);
        Debug.Log("Petal wurde instanziert");

        yield return null; // 1 Frame warten

        XRGrabInteractable interactable = petalInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter(interactor, interactable);
        }

        FlowerInventory.Instance.RemovePetal(currentSelectedPetal, -1);
    }


    /*
    private void TrySpawnPetalInHand(XRBaseInteractor interactor)
    {
        if (currentSelectedPetal.petalPrefab == null)
            return;

        // Spawnposition = aktuelle Position des Interactors (Controller-Hand)
        Vector3 spawnPos = interactor.transform.position;
        Quaternion spawnRot = interactor.transform.rotation;

        GameObject petalInstance = Instantiate(currentSelectedPetal.petalPrefab, spawnPos, spawnRot);

        // Stelle sicher, dass das Petal greifbar ist
        XRGrabInteractable interactable = petalInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter(interactor, interactable);
        }

        // Aus dem Inventar abziehen
        FlowerInventory.Instance.RemovePetal(currentSelectedPetal, -1);
    }*/
}
