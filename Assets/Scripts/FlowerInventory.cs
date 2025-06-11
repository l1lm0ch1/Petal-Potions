using System.Collections.Generic;
using UnityEngine;

public class FlowerInventory : MonoBehaviour
{
    public static FlowerInventory Instance;
    private Dictionary<PetalData, int> flowerPetalCounts = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPetals(PetalData petal, int amount)
    {
        if (!flowerPetalCounts.ContainsKey(petal))
            flowerPetalCounts[petal] = 0;

        flowerPetalCounts[petal] += amount;
        Debug.Log("Blütenblätter gezählt: " + petal.petalName + " → " + flowerPetalCounts[petal]);
    }

    public int GetPetalCount(PetalData petal)
    {
        return flowerPetalCounts.ContainsKey(petal) ? flowerPetalCounts[petal] : 0;
    }
}
