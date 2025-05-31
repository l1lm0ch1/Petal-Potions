using UnityEngine;
using System;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private int starshards;
    public int Starshards
    {
        get { return starshards; }
        private set
        {
            if (starshards != value)
            {
                starshards = value;
                PlayerPrefs.SetInt("Starshards", starshards);
                PlayerPrefs.Save();
                OnStarshardsChanged?.Invoke(starshards); // Event auslösen
            }
        }
    }

    public event Action<int> OnStarshardsChanged;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        starshards = PlayerPrefs.GetInt("Starshards", 0);
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
