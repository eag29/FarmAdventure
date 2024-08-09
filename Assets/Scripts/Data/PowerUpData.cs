using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { bagBoost, productsTime, characterSpeed };
[CreateAssetMenu(fileName = "PowerUp Data", menuName = "PowerUp Data / Scriptable Object", order = 0)]
public class PowerUpData : ScriptableObject
{
    public PowerUpType powerUpType;
    public int maxBagCapacity;
    public int productsSpeed;
    public int charactersSpeed;
}
