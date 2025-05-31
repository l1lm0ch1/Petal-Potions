using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShardManager : MonoBehaviour
{
    public const string StarShard = "StarShard";
    public static int starShard = 0;
    // Start is called before the first frame update
    void Start()
    {
        starShard = PlayerPrefs.GetInt("StarShard");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateStarShards()
    {
        PlayerPrefs.SetInt(StarShard, starShard);
    }
}
