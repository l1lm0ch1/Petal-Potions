using UnityEngine;

public class PetalSelectionUI : MonoBehaviour
{
    public PetalPouchController pouch; // Referenz zur Pouch

    public void SelectPetal(PetalData selectedPetal)
    {
        pouch.SetSelectedPetal(selectedPetal);
    }
}
