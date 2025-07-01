using UnityEngine;

public class SeedSelectionUI : MonoBehaviour
{
    public SeedPouchController pouch;

    private SeedSelectButton[] allButtons;

    private void Start()
    {
        allButtons = GetComponentsInChildren<SeedSelectButton>(true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (allButtons == null)
            allButtons = GetComponentsInChildren<SeedSelectButton>(true);

        foreach (var button in allButtons)
        {
            button.UpdateCount();
        }
    }

    public void SelectSeed(SeedData selectedSeed)
    {
        if (pouch != null)
        {
            pouch.SetSelectedSeed(selectedSeed);
        }
    }
}
