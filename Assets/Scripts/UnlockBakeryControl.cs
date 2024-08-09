using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnlockBakeryControl : MonoBehaviour
{
    [SerializeField] ProductType pt;
    int StoreProcedureCount;
    [SerializeField] int maxStoreProcedureCount;
    [SerializeField] int useProductsInSeconds;
    float time;
    [SerializeField] TextMeshProUGUI bakerytxt;

    [SerializeField] Transform coinTransform;
    [SerializeField] GameObject coin;
    [SerializeField] ParticleSystem smokeFx;
    void Start()
    {
        DisplayCount();
    }
    private void Update()
    {
        if (StoreProcedureCount > 0)
        {
            time += Time.deltaTime;

            if (time >= useProductsInSeconds)
            {
                time = 0.0f;
                UseProduct();
            }
        }
    }
    void DisplayCount()
    {
        bakerytxt.text = StoreProcedureCount + "/ " + maxStoreProcedureCount.ToString();
        ControlSmokeEffect();
    }
    public ProductType GetNeedProductType()
    {
        return pt;
    }
    void CreateCoin()
    {
        Vector3 pozs = Random.insideUnitSphere * 1;
        Vector3 InstantiatePozs = coinTransform.position + pozs;

        Instantiate(coin, InstantiatePozs, Quaternion.identity);
    }
    public bool StoreProduct()
    {
        if (maxStoreProcedureCount==StoreProcedureCount)
        {
            return false;
        }
        StoreProcedureCount++;
        DisplayCount();
        return true;
    }
    void UseProduct()
    {
        StoreProcedureCount--;
        DisplayCount();
        CreateCoin();
    }
    void ControlSmokeEffect()
    {
        if (StoreProcedureCount ==0)
        {
            if (smokeFx.isPlaying)
            {
                smokeFx.Stop();
            }
        }
        else
        {
            if (smokeFx.isStopped)
            {
                smokeFx.Play();
            }
        }
    }
}
