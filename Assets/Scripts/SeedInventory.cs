using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance;
    public PetalSelectionUI PetalSelectionUI;   // TODO: Auf Seed Selection UI umändern
    private Dictionary<SeedData, int> seedCounts = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPetals(SeedData seed)
    {
        int randomAmount = Random.Range(3, 6); // random Nummer zw 3 und 5

        if (!seedCounts.ContainsKey(seed))
            seedCounts[seed] = 0;

        seedCounts[seed] += randomAmount;
        Debug.Log("Blütenblätter gezählt: " + seed.seedName + " / " + seedCounts[seed] + " (+" + randomAmount + ")");
        PetalSelectionUI.UpdateUI();
    }

    public void RemoveSeed(SeedData seed, int amount)
    {
        if (!seedCounts.ContainsKey(seed))
            seedCounts[seed] = 0;

        seedCounts[seed] += amount;
        Debug.Log("Blütenblätter gezählt: " + seed.seedName + " / " + seedCounts[seed]);
        PetalSelectionUI.UpdateUI();
    }

    public int GetPetalCount(SeedData seed)
    {
        return seedCounts.ContainsKey(seed) ? seedCounts[seed] : 0;
    }
}
