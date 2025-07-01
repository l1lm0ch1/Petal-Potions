using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SeedSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class SeedUIEntry
    {
        public SeedData seedData;
        public TMP_Text countText;
    }

    [Header("Alle UI-Einträge")]
    public List<SeedUIEntry> entries = new();

    [Header("Pouch Referenz")]
    public SeedPouchController pouch; // Referenz zur Pouch


    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var entry in entries)
        {
            if (entry.seedData != null)
            {
                int count = SeedInventory.Instance.GetSeedCount(entry.seedData);
                entry.countText.text = count.ToString();
            }
        }
    }

    public void SelectSeed(SeedData selectedSeed)
    {
        pouch.SetSelectedSeed(selectedSeed);
    }
}
