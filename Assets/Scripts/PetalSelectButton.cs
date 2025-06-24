using UnityEngine;
using UnityEngine.UI;

public class PetalSelectButton : MonoBehaviour
{
    public PetalData PetalData;
    public PetalSelectionUI selectionUI;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClickSelectPetal);
    }

    private void OnClickSelectPetal()
    {
        selectionUI.SelectPetal(PetalData);
    }
}
