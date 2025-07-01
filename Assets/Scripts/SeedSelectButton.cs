using UnityEngine;
using UnityEngine.UI;

public class SeedSelectButton : MonoBehaviour
{
    public SeedData SeedData;
    public SeedSelectionUI selectionUI;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickSelectSeed);
    }

    private void OnClickSelectSeed()
    {
        selectionUI.SelectSeed(SeedData);
    }
}
