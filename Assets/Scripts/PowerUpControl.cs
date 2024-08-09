using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpControl : MonoBehaviour
{
    public PowerUpData pud;
    [SerializeField] int LockedUnitid;
    bool isPowerUpused;
    public bool productsPower;
    public bool charactersPower;
    void Start()
    {
        isPowerUpused = GetPowerUpStatus();
        productsPower = SpeedPowerUpStatus();
        charactersPower = CharactersPowerUpStatus();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pud.powerUpType == PowerUpType.bagBoost & !isPowerUpused)
            {
                isPowerUpused = true;
                BagControl bc = other.GetComponent<BagControl>();
                bc.BoostBagCapacity(pud.maxBagCapacity);
                PlayerPrefs.SetString("powerUpStatus", "used");
            }
            else if (pud.powerUpType == PowerUpType.productsTime & !productsPower)
            {
                productsPower = true;
                PlayerPrefs.SetString("productsSpeedUpStatus", "used");
            }
            else if (pud.powerUpType == PowerUpType.characterSpeed & !charactersPower)
            {
                charactersPower = true;
                PlayerPrefs.SetString("characterssSpeedUpStatus", "used");
            }
        }
    }
    bool GetPowerUpStatus()
    {
        string status = PlayerPrefs.GetString("powerUpStatus", "ready");

        if (status.Equals("ready"))
        {
            return false;
        }
        return true;
    }
    bool SpeedPowerUpStatus()
    {
        string status = PlayerPrefs.GetString("productsSpeedUpStatus", "readyy");

        if (status.Equals("readyy"))
        {
            return false;
        }
        return true;
    }
    bool CharactersPowerUpStatus()
    {
        string status = PlayerPrefs.GetString("characterssSpeedUpStatus", "readyyy");

        if (status.Equals("readyyy"))
        {
            return false;
        }
        return true;
    }
}
