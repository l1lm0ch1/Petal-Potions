using System.Collections.Generic;
using UnityEngine;

public class FlowerInventory : MonoBehaviour
{
    public static FlowerInventory Instance;
    private Dictionary<FlowerData, int> flowerPetalCounts = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPetals(FlowerData flower, int amount)
    {
        if (!flowerPetalCounts.ContainsKey(flower))
            flowerPetalCounts[flower] = 0;

        flowerPetalCounts[flower] += amount;
        Debug.Log("Blütenblätter gezählt: " + flower.flowerName + " → " + flowerPetalCounts[flower]);
    }

    public int GetPetalCount(FlowerData flower)
    {
        return flowerPetalCounts.ContainsKey(flower) ? flowerPetalCounts[flower] : 0;
    }
}
