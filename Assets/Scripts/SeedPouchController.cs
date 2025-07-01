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

        //yield return null; // 1 Frame warten

        XRGrabInteractable interactable = seedInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactionManager.SelectEnter(interactor, interactable);
        }

        SeedInventory.Instance.RemoveSeed(currentSelectedSeed, 1);
    }
}
