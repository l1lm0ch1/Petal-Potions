using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SeedPouchController : MonoBehaviour
{
    [Header("Pouch Visual")]
    public SeedPouch pouchVisual;

    [Header("XR Setup")]
    public XRInteractionManager interactionManager;

    private SeedData currentSelectedSeed;
    private XRDirectInteractor handInteractor;

    public void SetSelectedSeed(SeedData seed)
    {
        currentSelectedSeed = seed;
        Debug.Log("Ausgewähltes Seed: " + seed.seedName);

        if (pouchVisual != null)
        {
            pouchVisual.UpdatePouchVisual(seed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            int count = SeedInventory.Instance.GetSeedCount(currentSelectedSeed);
            if (currentSelectedSeed == null || count == 0)
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
        if (currentSelectedSeed.seedVisualPrefab == null)
            yield break;

        Vector3 spawnPos = interactor.transform.position;
        Quaternion spawnRot = interactor.transform.rotation;

        GameObject seedInstance = Instantiate(currentSelectedSeed.seedVisualPrefab, spawnPos, spawnRot);

        XRGrabInteractable interactable = seedInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter((IXRSelectInteractor)interactor, (IXRSelectInteractable)interactable);
        }

        SeedInventory.Instance.RemoveSeed(currentSelectedSeed, 1);
    }
}