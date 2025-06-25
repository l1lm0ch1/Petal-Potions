using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SeedPouchController : MonoBehaviour
{
    [Header("Pouch Visual")]
    public SeedPouch pouchVisual;

    [Header("XR Setup")]
    public XRInteractionManager interactionManager;

    private SeedData currentSelectedSeed;
    private XRBaseInteractor handInteractor;

    public void SetSelectedPetal(SeedData seed)
    {
        currentSelectedSeed = seed;
        Debug.Log("Ausgewähltes Petal: " + seed.seedName);

        if (pouchVisual != null)
        {
            pouchVisual.UpdatePouchVisual(seed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            Debug.Log("Hand in Pouch");

            if (currentSelectedSeed == null || currentSelectedSeed.Inventory == 0)
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
        if (currentSelectedSeed.seedVisualPrefab == null)
            yield break;

        Vector3 spawnPos = interactor.transform.position;
        Quaternion spawnRot = interactor.transform.rotation;

        GameObject petalInstance = Instantiate(currentSelectedSeed.seedVisualPrefab, spawnPos, spawnRot);
        Debug.Log("Petal wurde instanziert");

        yield return null; // 1 Frame warten

        XRGrabInteractable interactable = petalInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter(interactor, interactable);
        }

        SeedInventory.Instance.RemoveSeed(currentSelectedSeed, -1);
    }
}
