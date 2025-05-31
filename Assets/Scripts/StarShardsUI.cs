using UnityEngine;
using TMPro;

public class StarshardsUI : MonoBehaviour
{
    public TMP_Text starshardsText;

    void Start()
    {
        // Initial anzeigen
        starshardsText.text = "Starshards: " + PlayerData.Instance.GetStarshards();

        // Event abonnieren
        PlayerData.Instance.OnStarshardsChanged += UpdateUI;
    }

    void OnDestroy()
    {
        // Event wieder abbestellen, falls das UI-Objekt zerstört wird
        PlayerData.Instance.OnStarshardsChanged -= UpdateUI;
    }

    void UpdateUI(int newAmount)
    {
        starshardsText.text = "Starshards: " + newAmount;
    }
}
