using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PetalPouchController : MonoBehaviour
{
    public Transform spawnPoint;
    public XRGrabInteractable spawnablePrefabWrapper; // Optional für erweiterte Kontrolle
    private PetalData currentSelectedPetal;
    private XRBaseInteractor handInteractor;

    public void SetSelectedPetal(PetalData petal)
    {
        currentSelectedPetal = petal;
        // Optional: Ändere hier die Farbe oder Icon des Pouch-Visuals
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentSelectedPetal == null) return;

        XRBaseInteractor interactor = other.GetComponent<XRBaseInteractor>();
        if (interactor != null)
        {
            handInteractor = interactor;
            TrySpawnPetal();
        }
    }

    private void TrySpawnPetal()
    {
        if (currentSelectedPetal.petalPrefab == null || handInteractor == null) return;

        GameObject petalInstance = Instantiate(currentSelectedPetal.petalPrefab, spawnPoint.position, spawnPoint.rotation);

        XRGrabInteractable interactable = petalInstance.GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            // Objekt direkt ins Hand geben
            interactionManager.SelectEnter(handInteractor, interactable); // Achtung: Kann deprecated sein
        }

        // Inventar reduzieren, falls notwendig
        FlowerInventory.Instance.AddPetals(currentSelectedPetal, -1);
    }

    [SerializeField] private XRInteractionManager interactionManager;
}
