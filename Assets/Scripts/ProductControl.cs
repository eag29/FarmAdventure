using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductControl : MonoBehaviour
{
    [SerializeField] ProductData pd;
    [SerializeField] PowerUpData pud;
    [SerializeField] PowerUpControl puc;
    [SerializeField] BagControl bc;
    Vector3 originalScale;
    bool isReadyToPick;

    private void Start()
    {
        isReadyToPick = true;
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") & isReadyToPick)
        {
            if (bc.IsEmptySpace())
            {
                GameManager.instance.sounds[1].Play();
                bc.AddProductTheBag(pd);
                isReadyToPick = false;
                StartCoroutine(ProductPicked());
                GameManager.instance.Missions();

                if (pd.productType == ProductType.potato)
                {
                    GameManager.instance.levelSlider.value += 1;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 1);
                    GameManager.instance.txts[7].text = "XP+1";
                    SavedProduct();
                }
                if (pd.productType == ProductType.tomato)
                {
                    GameManager.instance.levelSlider.value += 2;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 2);
                    GameManager.instance.txts[7].text = "XP+2";
                    SavedProduct();
                }
                if (pd.productType == ProductType.cabbage)
                {
                    GameManager.instance.levelSlider.value += 3;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 3);
                    GameManager.instance.txts[7].text = "XP+3";
                    SavedProduct();
                }
                if (pd.productType == ProductType.strawberry)
                {
                    GameManager.instance.levelSlider.value += 4;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 4);
                    GameManager.instance.txts[7].text = "XP+4";
                    SavedProduct();
                }
                if (pd.productType == ProductType.kiwi)
                {
                    GameManager.instance.levelSlider.value += 5;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 5);
                    GameManager.instance.txts[7].text = "XP+5";
                    SavedProduct();
                }
                if (pd.productType == ProductType.melon)
                {
                    GameManager.instance.levelSlider.value += 6;
                    PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 6);
                    GameManager.instance.txts[7].text = "XP+6";
                    SavedProduct();
                }
            }
        }
    }
    void SavedProduct()
    {
        if (GameManager.instance.level1Mission)
        {
            PlayerPrefs.SetInt("Level1Product", PlayerPrefs.GetInt("Level1Product") + 1);
        }
        if (GameManager.instance.level2Mission)
        {
            PlayerPrefs.SetInt("Level2Product", PlayerPrefs.GetInt("Level2Product") + 1);
        }
        if (GameManager.instance.level3Mission)
        {
            PlayerPrefs.SetInt("Level3Product", PlayerPrefs.GetInt("Level3Product") + 1);
        }
        if (GameManager.instance.level4Mission)
        {
            PlayerPrefs.SetInt("Level4Product", PlayerPrefs.GetInt("Level4Product") + 1);
        }
        if (GameManager.instance.level5Mission)
        {
            PlayerPrefs.SetInt("Level5Product", PlayerPrefs.GetInt("Level5Product") + 1);
        }
        if (GameManager.instance.level6Mission)
        {
            PlayerPrefs.SetInt("Level6Product", PlayerPrefs.GetInt("Level6Product") + 1);
        }
        if (GameManager.instance.level7Mission)
        {
            PlayerPrefs.SetInt("Level7Product", PlayerPrefs.GetInt("Level7Product") + 1);
        }
        if (GameManager.instance.level8Mission)
        {
            PlayerPrefs.SetInt("Level8Product", PlayerPrefs.GetInt("Level8Product") + 1);
        }
        if (GameManager.instance.level9Mission)
        {
            PlayerPrefs.SetInt("Level9Product", PlayerPrefs.GetInt("Level9Product") + 1);
        }
        if (GameManager.instance.level10Mission)
        {
            PlayerPrefs.SetInt("Level10Product", PlayerPrefs.GetInt("Level10Product") + 1);
        }
    }
    public IEnumerator ProductPicked()
    {
        float duration = 1f;
        float timer = 0f;

        Vector3 targetScale = originalScale / 3;

        while (timer < duration)
        {
            float t = timer / duration;
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        if (pd.productType == ProductType.potato)
        {
            yield return new WaitForSeconds(7f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(7f - pud.productsSpeed);
                }
            }
        }
        if (pd.productType == ProductType.tomato)
        {
            yield return new WaitForSeconds(10f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(10f - pud.productsSpeed);
                }
            }
        }
        if (pd.productType == ProductType.cabbage)
        {
            yield return new WaitForSeconds(8.5f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(8.5f - pud.productsSpeed);
                }
            }
        }
        if (pd.productType == ProductType.strawberry)
        {
            yield return new WaitForSeconds(9f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(9f - pud.productsSpeed);
                }
            }
        }
        if (pd.productType == ProductType.kiwi)
        {
            yield return new WaitForSeconds(11f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(11f - pud.productsSpeed);
                }
            }
        }
        if (pd.productType == ProductType.melon)
        {
            yield return new WaitForSeconds(11f);
            if (pud.powerUpType == PowerUpType.productsTime)
            {
                if (puc.productsPower)
                {
                    yield return new WaitForSeconds(13f - pud.productsSpeed);
                }
            }
        }
        timer = 0f;
        float growBackDuration = 3f;

        while (timer < growBackDuration)
        {
            float t = timer / growBackDuration;
            Vector3 newScale = Vector3.Lerp(targetScale, originalScale, t);
            transform.localScale = newScale;
            timer += Time.deltaTime;
            yield return null;
        }

        isReadyToPick = true;
        yield return null;
    }
}
