using UnityEngine;

public class SeedBox : MonoBehaviour
{
    public SeedData seedType; // z.B. Rose, Tulpe, etc.

    // Methode, die beim "Anklicken" mit dem Controller aufgerufen wird:
    public void SelectSeed()
    {
        SeedManager.Instance.SetCurrentSeed(seedType);
        Debug.Log("Samen ausgewählt: " + seedType.flowerName);
    }
}
