using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockedUnitControl : MonoBehaviour
{
    [SerializeField] int lockedUnitXp;
    [SerializeField] int id;
    [SerializeField] int price;
    [SerializeField] TextMeshPro pricetxt;

    [SerializeField] GameObject lockedUnit;
    [SerializeField] GameObject unLockedUnit;

    bool isPurchased;
    string keyUnit = "keyUnit";

    private void Awake()
    {
        pricetxt.text = price.ToString();
        LoadUnit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & !isPurchased)
        {
            UnLockedUnit();
        }
    }
    void UnLockedUnit()
    {
        if (GameManager.instance.TryBuyThisUnity(price))
        {
            GameManager.instance.sounds[2].Play();
            UnLock();
            SaveUnit();
            SaveLockedUnit();
        }
    }
    void UnLock()
    {
        isPurchased = true;
        lockedUnit.SetActive(false);
        unLockedUnit.SetActive(true);
    }
    void SaveUnit()
    {
        string key = keyUnit + id.ToString();
        PlayerPrefs.SetString(key, "saved");
    }
    void LoadUnit()
    {
        string key = keyUnit + id.ToString();
        string status = PlayerPrefs.GetString(key);

        if (status.Equals("saved"))
        {
            UnLock();
        }
    }
    void SaveLockedUnit()
    {
        if (GameManager.instance.level1Mission)
        {
            PlayerPrefs.SetInt("Level1LockedUnit", PlayerPrefs.GetInt("Level1LockedUnit") + 1);
        }
        if (GameManager.instance.level2Mission)
        {
            PlayerPrefs.SetInt("Level2LockedUnit", PlayerPrefs.GetInt("Level2LockedUnit") + 1);
        }
        if (GameManager.instance.level3Mission)
        {
            PlayerPrefs.SetInt("Level3LockedUnit", PlayerPrefs.GetInt("Level3LockedUnit") + 1);
        }
        if (GameManager.instance.level4Mission)
        {
            PlayerPrefs.SetInt("Level4LockedUnit", PlayerPrefs.GetInt("Level4LockedUnit") + 1);
        }
        if (GameManager.instance.level5Mission)
        {
            PlayerPrefs.SetInt("Level5LockedUnit", PlayerPrefs.GetInt("Level5LockedUnit") + 1);
        }
        if (GameManager.instance.level6Mission)
        {
            PlayerPrefs.SetInt("Level6LockedUnit", PlayerPrefs.GetInt("Level6LockedUnit") + 1);
        }
        if (GameManager.instance.level7Mission)
        {
            PlayerPrefs.SetInt("Level7LockedUnit", PlayerPrefs.GetInt("Level7LockedUnit") + 1);
        }
        if (GameManager.instance.level8Mission)
        {
            PlayerPrefs.SetInt("Level8LockedUnit", PlayerPrefs.GetInt("Level8LockedUnit") + 1);
        }
        if (GameManager.instance.level9Mission)
        {
            PlayerPrefs.SetInt("Level9LockedUnit", PlayerPrefs.GetInt("Level9LockedUnit") + 1);
        }
        if (GameManager.instance.level10Mission)
        {
            PlayerPrefs.SetInt("Level10LockedUnit", PlayerPrefs.GetInt("Level10LockedUnit") + 1);
        }
    }
}
