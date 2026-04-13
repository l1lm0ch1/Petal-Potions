using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FlowerInteraction : MonoBehaviour
{
    [Header("Blumenlogik")]
    public FlowerGrowth growthScript;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        // Deaktivieren, bis Pfl�ck-Zeit
        interactable.enabled = false;

        // Registriere das Event, wenn der Trigger gedr�ckt wird
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void Update()
    {
        if(!interactable.enabled && growthScript.getFlowerGrowth())
        {
            interactable.enabled = true;
        }
    }

    void OnDestroy()
    {
        // Wichtig: Event wieder abmelden
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (growthScript.getFlowerGrowth())
        {
            growthScript.Harvest();
            // Du kannst hier auch Animationen / Sounds abspielen lassen
        }
        else
        {
            Debug.Log("Blume ist noch nicht bereit zum Pfl�cken!");
        }
    }
}
