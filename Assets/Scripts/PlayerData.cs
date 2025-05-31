using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public int starshards = 100; // Beispiel Startwert

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public bool TryPurchase(int price)
    {
        if (starshards >= price)
        {
            starshards -= price;
            return true;
        }
        return false;
    }
}
