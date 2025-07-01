using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelectButton : MonoBehaviour
{
    public SeedData seedData;
    public TMP_Text countText;

    private SeedSelectionUI selectionUI;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickSelectSeed);
        selectionUI = FindAnyObjectByType<SeedSelectionUI>();
    }

    public void UpdateCount()
    {
        if (seedData != null && countText != null)
        {
            int count = SeedInventory.Instance.GetSeedCount(seedData);
            countText.text = count.ToString();
        }
    }

    private void OnClickSelectSeed()
    {
        if (selectionUI != null)
        {
            selectionUI.SelectSeed(seedData);
        }
    }
}
