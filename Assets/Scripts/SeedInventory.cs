using System.Collections.Generic;
using UnityEngine;

public class SeedInventory : MonoBehaviour
{
    public static SeedInventory Instance;
    private SeedSelectionUI SeedSelectionUI;
    private Dictionary<SeedData, int> seedCounts = new();
    public event System.Action OnInventoryChanged;

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
        FindObjectOfType<SeedSelectionUI>()?.UpdateUI();
    }

    public void AddSeed(SeedData seed, int amount)
    {
        if (!seedCounts.ContainsKey(seed))
            seedCounts[seed] = 0;

        seedCounts[seed] += amount;
        FindObjectOfType<SeedSelectionUI>()?.UpdateUI();

        Debug.Log("Ein Seed hinzugefügt");
    }

    public int GetSeedCount(SeedData seed)
    {
        return seedCounts.ContainsKey(seed) ? seedCounts[seed] : 0;
    }
}
