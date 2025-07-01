using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private int starshards = 400; // Startwert

    public int Starshards
    {
        get { return starshards; }
        private set
        {
            if (starshards != value)
            {
                starshards = value;
                OnStarshardsChanged?.Invoke(starshards); // Event auslösen
            }
        }
    }

    public event Action<int> OnStarshardsChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Kein PlayerPrefs mehr – einfach mit default-Wert starten
        starshards = 400;
    }

    public bool TryPurchase(int price)
    {
        if (starshards >= price)
        {
            Starshards -= price;
            return true;
        }
        return false;
    }

    public void AddStarshards(int amount)
    {
        Starshards += amount;
    }

    public int GetStarshards()
    {
        return starshards;
    }
}
