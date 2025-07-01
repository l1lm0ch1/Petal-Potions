using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetalSelectButton : MonoBehaviour
{
    public PetalData petalData;
    public TMP_Text countText;

    private PetalSelectionUI selectionUI;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickSelectPetal);
        selectionUI = FindAnyObjectByType<PetalSelectionUI>();
    }

    public void UpdateCount()
    {
        if (petalData != null && countText != null)
        {
            int count = FlowerInventory.Instance.GetPetalCount(petalData);
            countText.text = count.ToString();
        }
    }

    private void OnClickSelectPetal()
    {
        if(selectionUI != null)
        {
            selectionUI.SelectPetal(petalData);
        }
    }
}
