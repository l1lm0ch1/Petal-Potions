using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetalSelectionUI : MonoBehaviour
{
    [System.Serializable]
    public class PetalUIEntry
    {
        public PetalData petalData;   // Referenz auf das Petal
        public TMP_Text countText;    // Text UI
    }

    [Header("Alle UI-Einträge")]
    public List<PetalUIEntry> entries = new();

    [Header("Pouch Referenz")]
    public PetalPouchController pouch; // Referenz zur Pouch


    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (var entry in entries)
        {
            if (entry.petalData != null)
            {
                int count = FlowerInventory.Instance.GetPetalCount(entry.petalData);
                entry.countText.text = count.ToString();
            }
        }
    }

    public void SelectPetal(PetalData selectedPetal)
    {
        pouch.SetSelectedPetal(selectedPetal);
    }
}
