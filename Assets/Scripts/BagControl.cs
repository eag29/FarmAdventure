using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BagControl : MonoBehaviour
{
    [SerializeField] List<ProductData> pdList;
    [SerializeField] Transform bag;

    Vector3 productSize;
    int maxBagCapacity = 5;
    [SerializeField] TextMeshPro maxtxt;

    private void Start()
    {
        maxBagCapacity = LoadBagCapacity();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shopPoint") & pdList.Count > 0)
        {
            for (int i = pdList.Count - 1; i >= 0; i--)
            {
                GameManager.instance.sounds[2].Play();
                SellProduct(pdList[i]);
                Destroy(bag.GetChild(i).gameObject);
                pdList.RemoveAt(i);
                GameManager.instance.Missions();
                SavedProduct();
            }
            ControlBagCapacity();
        }
        if (other.CompareTag("unlockBakery"))
        {
            UnlockBakeryControl ubc = other.GetComponent<UnlockBakeryControl>();

            ProductType pt = ubc.GetNeedProductType();

            for (int i = pdList.Count - 1; i >= 0; i--)
            {
                if (pdList[i].productType == pt)
                {
                    if (ubc.StoreProduct() == true)
                    {
                        Destroy(bag.transform.GetChild(i).gameObject);
                        pdList.RemoveAt(i);
                    }
                }
            }
            StartCoroutine(PutProductsInOrder());
            ControlBagCapacity();
        }
    }
    public bool IsEmptySpace()
    {
        if (pdList.Count < maxBagCapacity)
        {
            return true;
        }
        return false;
    }
    float NewPozs()
    {
        float newPozs = productSize.y * pdList.Count;
        return newPozs;
    }
    void CalculateObjectSize(GameObject obje)
    {
        if (productSize == Vector3.zero)
        {
            MeshRenderer mr = obje.GetComponent<MeshRenderer>();
            productSize = mr.bounds.size;
        }
    }
    public void AddProductTheBag(ProductData pd)
    {
        GameObject box = Instantiate(pd.product, Vector3.zero, Quaternion.identity);
        box.transform.SetParent(bag, true);

        CalculateObjectSize(box);
        float YPozs = NewPozs();
        box.transform.localPosition = Vector3.zero;
        box.transform.localRotation = Quaternion.identity;
        box.transform.localPosition = new Vector3(0, YPozs, 0);
        pdList.Add(pd);
        ControlBagCapacity();
    }
    void ControlBagCapacity()
    {
        if (pdList.Count == maxBagCapacity)
        {
            SetMaxtTextOn();
        }
        else
        {
            SetMaxtTextOff();
        }
    }
    void SetMaxtTextOn()
    {
        if (!maxtxt.isActiveAndEnabled)
        {
            maxtxt.gameObject.SetActive(true);
        }
    }
    void SetMaxtTextOff()
    {
        if (maxtxt.isActiveAndEnabled)
        {
            maxtxt.gameObject.SetActive(false);
        }
    }
    void SellProduct(ProductData PD)
    {
        GameManager.instance.ExchangeProduct(PD);
    }
    public IEnumerator PutProductsInOrder()
    {
        yield return new WaitForSeconds(0.15f);
        float totalHeight = 0f;
        float Gap = -0.3f;
        for (int i = 0; i < bag.childCount; i++)
        {
            Transform product = bag.GetChild(i);
            float newPozs = totalHeight;
            product.localPosition = new Vector3(0, newPozs, 0);
            MeshRenderer mr = product.GetComponent<MeshRenderer>();

            if (mr != null)
            {
                totalHeight += mr.bounds.size.y + Gap;
            }
        }
    }
    public void BoostBagCapacity(int boostCount)
    {
        maxBagCapacity += boostCount;
        PlayerPrefs.SetInt("maxCapacity", maxBagCapacity);
        ControlBagCapacity();
    }
    int LoadBagCapacity()
    {
        int maxBag = PlayerPrefs.GetInt("maxCapacity", 5);
        return maxBag;
    }
    void SavedProduct()
    {
        if (GameManager.instance.level1Mission)
        {
            PlayerPrefs.SetInt("Level1SellProduct", PlayerPrefs.GetInt("Level1SellProduct") + 1);
        }
        if (GameManager.instance.level2Mission)
        {
            PlayerPrefs.SetInt("Level2SellProduct", PlayerPrefs.GetInt("Level2SellProduct") + 1);
        }
        if (GameManager.instance.level3Mission)
        {
            PlayerPrefs.SetInt("Level3SellProduct", PlayerPrefs.GetInt("Level3SellProduct") + 1);
        }
        if (GameManager.instance.level4Mission)
        {
            PlayerPrefs.SetInt("Level4SellProduct", PlayerPrefs.GetInt("Level4SellProduct") + 1);
        }
        if (GameManager.instance.level5Mission)
        {
            PlayerPrefs.SetInt("Level5SellProduct", PlayerPrefs.GetInt("Level5SellProduct") + 1);
        }
        if (GameManager.instance.level6Mission)
        {
            PlayerPrefs.SetInt("Level6SellProduct", PlayerPrefs.GetInt("Level6SellProduct") + 1);
        }
        if (GameManager.instance.level7Mission)
        {
            PlayerPrefs.SetInt("Level7SellProduct", PlayerPrefs.GetInt("Level7SellProduct") + 1);
        }
        if (GameManager.instance.level8Mission)
        {
            PlayerPrefs.SetInt("Level8SellProduct", PlayerPrefs.GetInt("Level8SellProduct") + 1);
        }
        if (GameManager.instance.level9Mission)
        {
            PlayerPrefs.SetInt("Level9SellProduct", PlayerPrefs.GetInt("Level9SellProduct") + 1);
        }
        if (GameManager.instance.level10Mission)
        {
            PlayerPrefs.SetInt("Level10SellProduct", PlayerPrefs.GetInt("Level10SellProduct") + 1);
        }
    }
}
