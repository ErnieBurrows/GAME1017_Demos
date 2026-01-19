using System.Collections.Generic;
using UnityEngine;

public class DataHoldersDemo : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("=== DATA HOLDERS DEMO START ===");

        DemoArray();
        DemoList();
        DemoDictionary();

        Debug.Log("=== DATA HOLDERS DEMO END ===");
    }

    private void DemoArray()
    {
        Debug.Log("--- ARRAY (fixed size) ---");

        // Array: fixed size once created
        int[] scores = new int[3];
        scores[0] = 10;
        scores[1] = 25;
        scores[2] = 40;

        Debug.Log("scores length = " + scores.Length);
        Debug.Log("scores[1] = " + scores[1]);

        // Loop through array
        for (int i = 0; i < scores.Length; i++)
        {
            Debug.Log("scores[" + i + "] = " + scores[i]);
        }

        // Teaching note: you cannot Add() to an array
        Debug.Log("Arrays are best when you know the exact number of items.");
    }

    private void DemoList()
    {
        Debug.Log("--- LIST (dynamic size) ---");

        string forList = "Platform_A";
        // List: can grow/shrink
        List<string> activePlatforms = new List<string>();

        activePlatforms.Add(forList);
        activePlatforms.Add("Platform_B");
        activePlatforms.Add("Platform_C");

        Debug.Log("platform count = " + activePlatforms.Count);
        Debug.Log("platform[0] = " + activePlatforms[0]);

        // Remove example
        activePlatforms.Remove("Platform_B");
        Debug.Log("After removing Platform_B, count = " + activePlatforms.Count);

        // Loop through list
        foreach (string platform in activePlatforms)
        {
            Debug.Log("platform: " + platform);
        }

        Debug.Log("Lists are great for spawned objects, enemies, platform segments.");
    }

    private void DemoDictionary()
    {
        Debug.Log("--- DICTIONARY (key → value lookup) ---");

        // Dictionary: use keys to get values fast
        Dictionary<string, int> itemPrices = new Dictionary<string, int>();

        itemPrices.Add("Potion", 50);
        itemPrices.Add("Sword", 200);
        itemPrices.Add("Shield", 150);

        Debug.Log("price of Sword = " + itemPrices["Sword"]);

        // Safe lookup example (no crash if missing)
        if (itemPrices.TryGetValue("Bow", out int bowPrice))
        {
            Debug.Log("price of Bow = " + bowPrice);
        }
        else
        {
            Debug.Log("Bow not found in dictionary (TryGetValue prevents errors).");
        }

        // Loop through dictionary
        foreach (KeyValuePair<string, int> pair in itemPrices)
        {
            Debug.Log(pair.Key + " costs " + pair.Value);
        }

        Debug.Log("Dictionaries are best for lookups: name → clip, id → data, type → prefab.");
    }
}
