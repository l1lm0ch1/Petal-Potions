using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetalSelectionUI : MonoBehaviour
{
    public PetalPouchController pouch;

    private PetalSelectButton[] allButtons;

    private void Start()
    {
        allButtons = GetComponentsInChildren<PetalSelectButton>(true);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (allButtons == null)
            allButtons = GetComponentsInChildren<PetalSelectButton>(true);

        foreach (var button in allButtons)
        {
            button.UpdateCount();
        }
    }

    public void SelectPetal(PetalData selectedPetal)
    {
        if(pouch != null)
        {
            pouch.SetSelectedPetal(selectedPetal);
        }
    }
}
