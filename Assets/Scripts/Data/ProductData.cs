using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProductType { tomato, cabbage, potato, strawberry, kiwi, melon}
[CreateAssetMenu(fileName = "Product Data", menuName = "Product Data / Scriptable Object", order = 0)]
public class ProductData : ScriptableObject
{
    public ProductType productType;
    public GameObject product;
    public int productPrice;
}
