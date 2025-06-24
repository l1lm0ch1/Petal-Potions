using System.Collections.Generic;
using UnityEngine;

public class FlowerInventory : MonoBehaviour
{
    public static FlowerInventory Instance;
    public PetalSelectionUI PetalSelectionUI;
    private Dictionary<PetalData, int> flowerPetalCounts = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPetals(PetalData petal)
    {
        int randomAmount = Random.Range(3, 6); // random Nummer zw 3 und 5

        if (!flowerPetalCounts.ContainsKey(petal))
            flowerPetalCounts[petal] = 0;

        flowerPetalCounts[petal] += randomAmount;
        Debug.Log("Blütenblätter gezählt: " + petal.petalName + " / " + flowerPetalCounts[petal] + " (+" + randomAmount + ")");
        PetalSelectionUI.UpdateUI();
    }

    public void RemovePetal(PetalData petal, int amount)
    {
        if (!flowerPetalCounts.ContainsKey(petal))
            flowerPetalCounts[petal] = 0;

        flowerPetalCounts[petal] += amount;
        Debug.Log("Blütenblätter gezählt: " + petal.petalName + " / " + flowerPetalCounts[petal]);
        PetalSelectionUI.UpdateUI();
    }

    public int GetPetalCount(PetalData petal)
    {
        return flowerPetalCounts.ContainsKey(petal) ? flowerPetalCounts[petal] : 0;
    }
}
