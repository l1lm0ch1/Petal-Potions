using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance;
    public SeedSelectionUI SeedSelectionUI;
    private Dictionary<SeedData, int> seedCounts = new();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RemoveSeed(SeedData seed, int amount)
    {
        if (!seedCounts.ContainsKey(seed))
            seedCounts[seed] = 0;

        seedCounts[seed] -= amount;

        if (seedCounts[seed] < 0)
            seedCounts[seed] = 0;

        Debug.Log("Ein Seed abgezogen");
        Debug.Log("Seeds gezählt: " + seed.seedName + " / " + seedCounts[seed]);
        SeedSelectionUI.UpdateUI();
    }

    public void AddSeed(SeedData seed, int amount)
    {
        if (!seedCounts.ContainsKey(seed))
            seedCounts[seed] = 0;

        seedCounts[seed] += amount;
        SeedSelectionUI.UpdateUI();
        Debug.Log("Ein Seed hinzugefügt");
    }

    public int GetSeedCount(SeedData seed)
    {
        return seedCounts.ContainsKey(seed) ? seedCounts[seed] : 0;
    }
}
