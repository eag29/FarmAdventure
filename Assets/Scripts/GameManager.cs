using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("OBJECTS")]
    [SerializeField] PlayerMovement pm;
    public PlayerMovement pm2;
    [SerializeField] GameObject[] gardens;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject[] fields;
    [SerializeField] GameObject[] fieldarea;

    [Header("VALUABLES")]
    bool missionComplete, garden1buyed, hedefarttir;
    public bool gardenbuyed, backtomainGarden, missionexit, productsBuyed, movedJoystick, fixedJoystick, oyunAksin, gardenShop;
    public bool level1, level2, level3, level4, level5, level6, level7, level8, level9, level10;
    public bool level1Mission, level2Mission, level3Mission, level4Mission, level5Mission, level6Mission, level7Mission, level8Mission, level9Mission, level10Mission;
    [SerializeField] int ToplamlevelXp;
    public int kilitliAlanSayisi;
    public int coin;
    public int hedefUrunSayisi;
    public int satilacakUrunSayisi;
    public int gorevXp;
    public int buyedgarden;
    public AudioSource[] sounds;

    [Header("CANVAS SETTINGS")]
    [SerializeField] RectTransform MovingJoystick;
    [SerializeField] RectTransform FixedJoystick;
    public Slider levelSlider;
    [SerializeField] GameObject levelimg;
    public TextMeshProUGUI[] txts; //leveltxt, mevcutXptxt, ToplamXptxt, cointxt, joysticktxt, winlvltxt, winCointxt, xpText;
    [SerializeField] GameObject[] pnls; //pausepnl, settingspnl, exitpnl, winpnl, gardenShopPnl, shoppnl;
    [SerializeField] GameObject[] buyedpnls;
    [SerializeField] GameObject[] shopBuyedpnls;
    [SerializeField] GameObject missionpnl; //
    [SerializeField] GameObject missionpnlimg; //
    [SerializeField] GameObject missionpnlimgbtn; //
    [SerializeField] Button[] btns; //pausebtn,joystick_on, joystick_off;
    [SerializeField] Button[] pricebtns;
    [SerializeField] Button[] shopPricebtns;
    [SerializeField] Button[] gardengobtns;
    [SerializeField] Button[] shopGardengobtns;
    [SerializeField] Image[] imgs; //soundbtn, musicbtn;
    public Image[] missionimgs;
    public Image[] missionCorrectimgs;
    [SerializeField] Sprite[] sprts; //soundon, soundof, musicon, musicoff;

    private void Awake()
    {
        SahneIlkIslemler();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    void LoadCash()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
    }
    void SaveCash()
    {
        PlayerPrefs.SetInt("Coin", coin);
    }
    void SahneIlkIslemler()
    {
        if (PlayerPrefs.HasKey("LockedUnit"))
        {
            PlayerPrefs.GetInt("LockedUnit");
        }

        BahceOnIslemler();

        if (PlayerPrefs.HasKey("Sound") & PlayerPrefs.HasKey("Music"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1 & PlayerPrefs.GetInt("Music") == 1)
            {
                imgs[0].sprite = sprts[0];
                imgs[1].sprite = sprts[2];

                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].mute = false;
                }
            }
            if (PlayerPrefs.GetInt("Sound") == 1 & PlayerPrefs.GetInt("Music") == 0)
            {
                imgs[0].sprite = sprts[0];
                imgs[1].sprite = sprts[3];

                sounds[0].mute = true;
                sounds[1].mute = false;
                sounds[2].mute = false;
                sounds[3].mute = false;
            }
            if (PlayerPrefs.GetInt("Sound") == 0 & PlayerPrefs.GetInt("Music") == 1)
            {
                imgs[0].sprite = sprts[1];
                imgs[1].sprite = sprts[2];

                sounds[0].mute = false;
                sounds[1].mute = true;
                sounds[2].mute = true;
                sounds[3].mute = true;
            }
            if (PlayerPrefs.GetInt("Sound") == 0 & PlayerPrefs.GetInt("Music") == 0)
            {
                imgs[0].sprite = sprts[1];
                imgs[1].sprite = sprts[3];

                for (int i = 0; i < sounds.Length; i++)
                {
                    sounds[i].mute = true;
                }
            }

            if (PlayerPrefs.GetInt("Joystick") == 0)
            {
                movedJoystick = true;
            }
            else
            {
                movedJoystick = false;
                fixedJoystick = true;
            }
        }

        LoadCash();
        BackUpMissions();
        txts[0].text = SceneManager.GetActiveScene().name;
        txts[1].text = PlayerPrefs.GetInt("Xp").ToString();
        levelSlider.minValue = PlayerPrefs.GetInt("Xp");
        txts[2].text = levelSlider.maxValue + " XP";
        txts[3].text = coin.ToString();
    }
    private void Update()
    {
        txts[1].text = PlayerPrefs.GetInt("Xp").ToString() + " XP";

        LevelControl();

        if (backtomainGarden)
        {
            gardens[0].SetActive(false);
            gardens[1].SetActive(false);
            gardens[2].SetActive(false);
            gardens[3].SetActive(false);
            pm2.gameObject.SetActive(false);
            pm.gameObject.SetActive(true);
        }
    }
    public void ExchangeProduct(ProductData pd)
    {
        AddCoin(pd.productPrice);

        if (pd.productType == ProductType.potato)
        {
            levelSlider.value += 1;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 1);
            txts[7].text = "XP+1";
        }
        if (pd.productType == ProductType.tomato)
        {
            levelSlider.value += 2;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 2);
            txts[7].text = "XP+2";
        }
        if (pd.productType == ProductType.cabbage)
        {
            levelSlider.value += 3;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 3);
            txts[7].text = "XP+3";
        }
        if (pd.productType == ProductType.strawberry)
        {
            levelSlider.value += 4;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 4);
            txts[7].text = "XP+4";
        }
        if (pd.productType == ProductType.kiwi)
        {
            levelSlider.value += 5;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 5);
            txts[7].text = "XP+5";
        }
        if (pd.productType == ProductType.melon)
        {
            levelSlider.value += 6;
            PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + 6);
            txts[7].text = "XP+6";
        }
    }
    public void AddCoin(int price)
    {
        coin += price;
        SaveCash();
        txts[3].text = coin.ToString();
    }
    public void SpendCoin(int _price)
    {
        coin -= _price;
        SaveCash();
        txts[3].text = coin.ToString();
    }
    public int GetCoin()
    {
        return coin;
    }
    public bool TryBuyThisUnity(int Price)
    {
        if (GetCoin() >= Price)
        {
            SpendCoin(Price);
            return true;
        }
        return false;
    }
    void LevelControl()
    {
        if (level1Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level1Mission") == 1)
            {
                Win();
            }
        }
        if (level2Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level2Mission") == 1)
            {
                Win();
            }
        }
        if (level3Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level3Mission") == 1)
            {
                Win();
            }
        }
        if (level4Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level4Mission") == 1)
            {
                Win();
            }
        }
        if (level5Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level5Mission") == 1)
            {
                Win();
            }
        }
        if (level6Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level6Mission") == 1)
            {
                Win();
            }
        }
        if (level7Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level7Mission") == 1)
            {
                Win();
            }
        }
        if (level8Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level8Mission") == 1)
            {
                Win();
            }
        }
        if (level9Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level9Mission") == 1)
            {
                Win();
            }
        }
        if (level10Mission)
        {
            if (levelSlider.value >= ToplamlevelXp & PlayerPrefs.GetInt("Level10Mission") == 1)
            {
                Win();
            }
        }
    }
    public void CanvasIslemleri(string process)
    {
        switch (process)
        {
            case "paused":
                sounds[3].Play();
                Time.timeScale = 0;
                pnls[1].SetActive(false);
                pnls[0].SetActive(true);
                btns[0].gameObject.SetActive(false);
                break;
            case "resume":
                sounds[3].Play();
                pnls[0].SetActive(false);
                btns[0].gameObject.SetActive(true);
                Time.timeScale = 1;
                break;
            case "settings":
                sounds[3].Play();
                pnls[0].SetActive(false);
                pnls[1].SetActive(true);
                break;
            case "exitpnl":
                sounds[3].Play();
                pnls[0].SetActive(false);
                pnls[2].SetActive(true);
                break;
            case "yes":
                Application.Quit();
                break;
            case "no":
                pnls[2].SetActive(false);
                pnls[0].SetActive(true);
                break;
            case "sound":
                sounds[3].Play();
                if (PlayerPrefs.GetInt("Sound") == 1)
                {
                    PlayerPrefs.SetInt("Sound", 0);
                    imgs[0].sprite = sprts[1];

                    sounds[1].mute = true;
                    sounds[2].mute = true;
                    sounds[3].mute = true;
                }
                else if (PlayerPrefs.GetInt("Sound") == 0)
                {
                    PlayerPrefs.SetInt("Sound", 1);
                    imgs[0].sprite = sprts[0];

                    sounds[1].mute = false;
                    sounds[2].mute = false;
                    sounds[3].mute = false;
                }
                break;
            case "music":
                sounds[3].Play();
                if (PlayerPrefs.GetInt("Music") == 1)
                {
                    PlayerPrefs.SetInt("Music", 0);
                    imgs[1].sprite = sprts[3];
                    sounds[0].mute = true;
                }
                else if (PlayerPrefs.GetInt("Music") == 0)
                {
                    PlayerPrefs.SetInt("Music", 1);
                    imgs[1].sprite = sprts[2];
                    sounds[0].mute = false;
                }
                break;
            case "joystick_on":
                MovingJoystick.gameObject.SetActive(false);
                FixedJoystick.gameObject.SetActive(true);
                movedJoystick = false;
                fixedJoystick = true;
                btns[1].gameObject.SetActive(false);
                btns[2].gameObject.SetActive(true);
                txts[4].text = "Fixed Joystick";
                break;
            case "joystick_off":
                FixedJoystick.gameObject.SetActive(false);
                MovingJoystick.gameObject.SetActive(true);
                fixedJoystick = false;
                movedJoystick = true;
                btns[2].gameObject.SetActive(false);
                btns[1].gameObject.SetActive(true);
                txts[4].text = "Moving Joystick";
                break;
            case "nextlevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case "gardenExit":
                pnls[4].SetActive(false);
                Time.timeScale = 1;
                pm.gardenShopping = false;
                break;
            case "missionpnl":
                Time.timeScale = 0;
                missionpnl.SetActive(true);
                missionexit = false;
                levelSlider.gameObject.SetActive(false);
                inventory.gameObject.SetActive(false);
                levelimg.SetActive(false);
                missionpnlimg.SetActive(false);
                missionpnlimgbtn.SetActive(false);
                btns[0].gameObject.SetActive(false);
                txts[7].gameObject.SetActive(false);
                break;
            case "missionexit":
                Time.timeScale = 1;
                missionpnl.SetActive(false);
                missionexit = true;
                levelSlider.gameObject.SetActive(true);
                inventory.gameObject.SetActive(true);
                levelimg.SetActive(true);
                missionpnlimg.SetActive(true);
                missionpnlimgbtn.SetActive(true);
                btns[0].gameObject.SetActive(true);
                txts[7].gameObject.SetActive(true);
                break;
            case "shopexit":
                Time.timeScale = 1;
                pnls[5].SetActive(false);
                pm.shop = false;
                break;
            case "shopPrice_1440":
                if (TryBuyThisUnity(1440))
                {
                    sounds[2].Play();
                    shopPricebtns[0].gameObject.SetActive(false);
                    shopBuyedpnls[0].gameObject.SetActive(true);
                    shopGardengobtns[0].gameObject.SetActive(true);
                    txts[9].text = coin.ToString();
                }
                break;
            case "shopPrice_960":
                if (TryBuyThisUnity(960))
                {
                    sounds[2].Play();
                    shopPricebtns[1].gameObject.SetActive(false);
                    shopBuyedpnls[1].gameObject.SetActive(true);
                    shopGardengobtns[1].gameObject.SetActive(true);
                    txts[9].text = coin.ToString();
                }
                break;
            case "shopPrice_2400":
                if (TryBuyThisUnity(2400))
                {
                    sounds[2].Play();
                    shopPricebtns[2].gameObject.SetActive(false);
                    shopBuyedpnls[2].gameObject.SetActive(true);
                    shopGardengobtns[2].gameObject.SetActive(true);
                    txts[9].text = coin.ToString();
                }
                break;
            case "shopPrice_3360":
                if (TryBuyThisUnity(2400))
                {
                    sounds[2].Play();
                    shopPricebtns[3].gameObject.SetActive(false);
                    shopBuyedpnls[3].gameObject.SetActive(true);
                    shopGardengobtns[3].gameObject.SetActive(true);
                    txts[9].text = coin.ToString();
                }
                break;
            case "products1use":
                Time.timeScale = 1;
                productsBuyed = true;
                pnls[5].SetActive(false);
                pm.shop = false;
                fields[0].SetActive(true);
                fieldarea[0].SetActive(true);
                fieldarea[1].SetActive(true);
                fieldarea[2].SetActive(true);
                fieldarea[3].SetActive(true);
                fieldarea[4].SetActive(true);
                fieldarea[5].SetActive(true);
                break;
        }
    }
    public void BahceIslemleri(string process)
    {
        switch (process)
        {
            case "gardenPrice_3bin":
                if (TryBuyThisUnity(3000))
                {
                    sounds[2].Play();
                    pricebtns[0].gameObject.SetActive(false);
                    buyedpnls[0].gameObject.SetActive(true);
                    gardengobtns[0].gameObject.SetActive(true);
                    buyedgarden++;
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden1", PlayerPrefs.GetInt("Garden1") + 1);
                }
                break;
            case "gardenPrice_6bin":
                if (TryBuyThisUnity(6000))
                {
                    sounds[2].Play();
                    pricebtns[1].gameObject.SetActive(false);
                    buyedpnls[1].gameObject.SetActive(true);
                    gardengobtns[1].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden2", PlayerPrefs.GetInt("Garden2") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice_10bin":
                if (TryBuyThisUnity(10000))
                {
                    sounds[2].Play();
                    pricebtns[2].gameObject.SetActive(false);
                    buyedpnls[2].gameObject.SetActive(true);
                    gardengobtns[2].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden3", PlayerPrefs.GetInt("Garden3") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice_15bin":
                if (TryBuyThisUnity(15000))
                {
                    sounds[2].Play();
                    pricebtns[3].gameObject.SetActive(false);
                    buyedpnls[3].gameObject.SetActive(true);
                    gardengobtns[3].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden4", PlayerPrefs.GetInt("Garden4") + 1);
                    buyedgarden++;
                }
                break;

            case "garden1go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden1") == 1)
                {
                    backtomainGarden = false;
                    gardenbuyed = true;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[0].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden2go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden2") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[1].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden3go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden3") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[2].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden4go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden4") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[3].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;


            case "gardenPrice2_5bin":
                if (TryBuyThisUnity(5000))
                {
                    sounds[2].Play();
                    pricebtns[0].gameObject.SetActive(false);
                    buyedpnls[0].gameObject.SetActive(true);
                    gardengobtns[0].gameObject.SetActive(true);
                    buyedgarden++;
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden5", PlayerPrefs.GetInt("Garden5") + 1);
                }
                break;
            case "gardenPrice2_9bin":
                if (TryBuyThisUnity(9000))
                {
                    sounds[2].Play();
                    pricebtns[1].gameObject.SetActive(false);
                    buyedpnls[1].gameObject.SetActive(true);
                    gardengobtns[1].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden6", PlayerPrefs.GetInt("Garden6") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice2_15bin":
                if (TryBuyThisUnity(15000))
                {
                    sounds[2].Play();
                    pricebtns[2].gameObject.SetActive(false);
                    buyedpnls[2].gameObject.SetActive(true);
                    gardengobtns[2].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden7", PlayerPrefs.GetInt("Garden7") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice2_25bin":
                if (TryBuyThisUnity(25000))
                {
                    sounds[2].Play();
                    pricebtns[3].gameObject.SetActive(false);
                    buyedpnls[3].gameObject.SetActive(true);
                    gardengobtns[3].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden8", PlayerPrefs.GetInt("Garden8") + 1);
                    buyedgarden++;
                }
                break;

            case "garden5go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden5") == 1)
                {
                    backtomainGarden = false;
                    gardenbuyed = true;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[0].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden6go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden6") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[1].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden7go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden7") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[2].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden8go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden8") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[3].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;


            case "gardenPrice3_10bin":
                if (TryBuyThisUnity(10000))
                {
                    sounds[2].Play();
                    pricebtns[0].gameObject.SetActive(false);
                    buyedpnls[0].gameObject.SetActive(true);
                    gardengobtns[0].gameObject.SetActive(true);
                    buyedgarden++;
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden5", PlayerPrefs.GetInt("Garden5") + 1);
                }
                break;
            case "gardenPrice3_15bin":
                if (TryBuyThisUnity(15000))
                {
                    sounds[2].Play();
                    pricebtns[1].gameObject.SetActive(false);
                    buyedpnls[1].gameObject.SetActive(true);
                    gardengobtns[1].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden6", PlayerPrefs.GetInt("Garden6") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice3_20bin":
                if (TryBuyThisUnity(20000))
                {
                    sounds[2].Play();
                    pricebtns[2].gameObject.SetActive(false);
                    buyedpnls[2].gameObject.SetActive(true);
                    gardengobtns[2].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden7", PlayerPrefs.GetInt("Garden7") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice3_30bin":
                if (TryBuyThisUnity(30000))
                {
                    sounds[2].Play();
                    pricebtns[3].gameObject.SetActive(false);
                    buyedpnls[3].gameObject.SetActive(true);
                    gardengobtns[3].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden8", PlayerPrefs.GetInt("Garden8") + 1);
                    buyedgarden++;
                }
                break;

            case "garden9go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden9") == 1)
                {
                    backtomainGarden = false;
                    gardenbuyed = true;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[0].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden10go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden10") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[1].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden11go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden11") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[2].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden12go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden12") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[3].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;


            case "gardenPrice4_15bin":
                if (TryBuyThisUnity(15000))
                {
                    sounds[2].Play();
                    pricebtns[0].gameObject.SetActive(false);
                    buyedpnls[0].gameObject.SetActive(true);
                    gardengobtns[0].gameObject.SetActive(true);
                    buyedgarden++;
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden5", PlayerPrefs.GetInt("Garden5") + 1);
                }
                break;
            case "gardenPrice4_20bin":
                if (TryBuyThisUnity(20000))
                {
                    sounds[2].Play();
                    pricebtns[1].gameObject.SetActive(false);
                    buyedpnls[1].gameObject.SetActive(true);
                    gardengobtns[1].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden6", PlayerPrefs.GetInt("Garden6") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice4_25bin":
                if (TryBuyThisUnity(25000))
                {
                    sounds[2].Play();
                    pricebtns[2].gameObject.SetActive(false);
                    buyedpnls[2].gameObject.SetActive(true);
                    gardengobtns[2].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden7", PlayerPrefs.GetInt("Garden7") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice4_30bin":
                if (TryBuyThisUnity(30000))
                {
                    sounds[2].Play();
                    pricebtns[3].gameObject.SetActive(false);
                    buyedpnls[3].gameObject.SetActive(true);
                    gardengobtns[3].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden8", PlayerPrefs.GetInt("Garden8") + 1);
                    buyedgarden++;
                }
                break;

            case "garden13go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden13") == 1)
                {
                    backtomainGarden = false;
                    gardenbuyed = true;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[0].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden14go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden14") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[1].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden15go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden15") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[2].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden16go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden16") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[3].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;


            case "gardenPrice5_20bin":
                if (TryBuyThisUnity(20000))
                {
                    sounds[2].Play();
                    pricebtns[0].gameObject.SetActive(false);
                    buyedpnls[0].gameObject.SetActive(true);
                    gardengobtns[0].gameObject.SetActive(true);
                    buyedgarden++;
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden5", PlayerPrefs.GetInt("Garden5") + 1);
                }
                break;
            case "gardenPrice5_25bin":
                if (TryBuyThisUnity(25000))
                {
                    sounds[2].Play();
                    pricebtns[1].gameObject.SetActive(false);
                    buyedpnls[1].gameObject.SetActive(true);
                    gardengobtns[1].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden6", PlayerPrefs.GetInt("Garden6") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice5_35bin":
                if (TryBuyThisUnity(35000))
                {
                    sounds[2].Play();
                    pricebtns[2].gameObject.SetActive(false);
                    buyedpnls[2].gameObject.SetActive(true);
                    gardengobtns[2].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden7", PlayerPrefs.GetInt("Garden7") + 1);
                    buyedgarden++;
                }
                break;
            case "gardenPrice5_45bin":
                if (TryBuyThisUnity(45000))
                {
                    sounds[2].Play();
                    pricebtns[3].gameObject.SetActive(false);
                    buyedpnls[3].gameObject.SetActive(true);
                    gardengobtns[3].gameObject.SetActive(true);
                    txts[8].text = coin.ToString();
                    PlayerPrefs.SetInt("Garden8", PlayerPrefs.GetInt("Garden8") + 1);
                    buyedgarden++;
                }
                break;

            case "garden17go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden17") == 1)
                {
                    backtomainGarden = false;
                    gardenbuyed = true;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[0].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden18go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden18") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[1].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden19go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden19") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[2].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
            case "garden20go":
                Time.timeScale = 1;
                if (PlayerPrefs.GetInt("Garden20") == 1)
                {
                    gardenbuyed = true;
                    backtomainGarden = false;
                    pm.gardenShopping = false;
                    pnls[4].SetActive(false);
                    gardens[3].SetActive(true);
                    pm.gameObject.SetActive(false);
                    pm2.gameObject.SetActive(true);
                }
                break;
        }
    }
    void Win()
    {
        PlayerPrefs.SetInt("Garden", 0);
        PlayerPrefs.SetInt("LockedUnit", 0);
        pnls[3].SetActive(true);
        txts[5].text = SceneManager.GetActiveScene().name + " PASSED";
        txts[6].text = coin.ToString();
    }
    public void Missions()
    {
        if (level1)
        {
            if (PlayerPrefs.GetInt("Level1Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level1Hedef1", PlayerPrefs.GetInt("Level1Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level1SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level1Hedef2", PlayerPrefs.GetInt("Level1Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level1Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level1SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level1Hedef", PlayerPrefs.GetInt("Level1Hedef") + 1);
                PlayerPrefs.SetInt("Level1Mission", PlayerPrefs.GetInt("Level1Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level2)
        {
            if (PlayerPrefs.GetInt("Level2Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level2Hedef1", PlayerPrefs.GetInt("Level2Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level2SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level2Hedef2", PlayerPrefs.GetInt("Level2Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level2Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level2SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level2Hedef", PlayerPrefs.GetInt("Level2Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level2LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level2Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level2SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level2LockedUnit") == kilitliAlanSayisi)
            {
                PlayerPrefs.SetInt("Level2Mission", PlayerPrefs.GetInt("Level2Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level3)
        {
            if (PlayerPrefs.GetInt("Level3Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level3Hedef1", PlayerPrefs.GetInt("Level3Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level3SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level3Hedef2", PlayerPrefs.GetInt("Level3Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level3Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level3SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level3Hedef", PlayerPrefs.GetInt("Level3Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level3LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level3Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level3SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level3LockedUnit") == kilitliAlanSayisi)
            {
                PlayerPrefs.SetInt("Level3Mission", PlayerPrefs.GetInt("Level3Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level4)
        {
            if (PlayerPrefs.GetInt("Level4Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level4Hedef1", PlayerPrefs.GetInt("Level4Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level4SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level4Hedef2", PlayerPrefs.GetInt("Level4Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level4Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level4SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level4Hedef", PlayerPrefs.GetInt("Level4Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level4LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level4Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level4SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level4LockedUnit") == kilitliAlanSayisi)
            {
                PlayerPrefs.SetInt("Level4Mission", PlayerPrefs.GetInt("Level4Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level5)
        {
            if (PlayerPrefs.GetInt("Level5Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level5Hedef1", PlayerPrefs.GetInt("Level5Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level5SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level5Hedef2", PlayerPrefs.GetInt("Level5Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level5Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level5SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level5Hedef", PlayerPrefs.GetInt("Level5Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level5LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level5Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level5SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level5LockedUnit") == kilitliAlanSayisi)
            {
                PlayerPrefs.SetInt("Level5Mission", PlayerPrefs.GetInt("Level5Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level6)
        {
            if (PlayerPrefs.GetInt("Level6Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level6Hedef1", PlayerPrefs.GetInt("Level6Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level6SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level6Hedef2", PlayerPrefs.GetInt("Level6Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level6Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level6SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level6Hedef", PlayerPrefs.GetInt("Level6Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level6LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 4)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level6Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level6SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level6LockedUnit") == kilitliAlanSayisi & PlayerPrefs.GetInt("Garden") == 4)
            {
                PlayerPrefs.SetInt("Level6Mission", PlayerPrefs.GetInt("Level6Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level7)
        {
            if (PlayerPrefs.GetInt("Level7Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level7Hedef1", PlayerPrefs.GetInt("Level7Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level7SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level7Hedef2", PlayerPrefs.GetInt("Level7Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level7Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level7SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level7Hedef", PlayerPrefs.GetInt("Level7Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level7LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 8)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level7Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level7SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level7LockedUnit") == kilitliAlanSayisi & PlayerPrefs.GetInt("Garden") == 8)
            {
                PlayerPrefs.SetInt("Level7Mission", PlayerPrefs.GetInt("Level7Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level8)
        {
            if (PlayerPrefs.GetInt("Level8Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level8Hedef1", PlayerPrefs.GetInt("Level8Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level8SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level8Hedef2", PlayerPrefs.GetInt("Level8Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level8Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level8SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level8Hedef", PlayerPrefs.GetInt("Level8Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level8LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 12)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level8Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level8SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level8LockedUnit") == kilitliAlanSayisi & PlayerPrefs.GetInt("Garden") == 12)
            {
                PlayerPrefs.SetInt("Level8Mission", PlayerPrefs.GetInt("Level8Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level9)
        {
            if (PlayerPrefs.GetInt("Level9Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level9Hedef1", PlayerPrefs.GetInt("Level9Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level9SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level9Hedef2", PlayerPrefs.GetInt("Level9Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level9Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level9SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level9Hedef", PlayerPrefs.GetInt("Level9Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level9LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 16)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level9Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level9SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level9LockedUnit") == kilitliAlanSayisi & PlayerPrefs.GetInt("Garden") == 16)
            {
                PlayerPrefs.SetInt("Level9Mission", PlayerPrefs.GetInt("Level9Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
        if (level10)
        {
            if (PlayerPrefs.GetInt("Level10Product") == hedefUrunSayisi)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level10Hedef1", PlayerPrefs.GetInt("Level10Hedef1") + 1);
            }
            if (PlayerPrefs.GetInt("Level10SellProduct") == satilacakUrunSayisi)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                PlayerPrefs.SetInt("Level10Hedef2", PlayerPrefs.GetInt("Level10Hedef2") + 1);
            }
            if (PlayerPrefs.GetInt("Level10Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level10SellProduct") == satilacakUrunSayisi)
            {
                PlayerPrefs.SetInt("Level10Hedef", PlayerPrefs.GetInt("Level10Hedef") + 1);
            }
            if (PlayerPrefs.GetInt("Level10LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 20)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level10Product") == hedefUrunSayisi & PlayerPrefs.GetInt("Level10SellProduct") == satilacakUrunSayisi & PlayerPrefs.GetInt("Level10LockedUnit") == kilitliAlanSayisi & PlayerPrefs.GetInt("Garden") == 20)
            {
                PlayerPrefs.SetInt("Level10Mission", PlayerPrefs.GetInt("Level10Mission") + 1);

                levelSlider.value += gorevXp;
                PlayerPrefs.SetInt("Xp", PlayerPrefs.GetInt("Xp") + gorevXp);
                txts[7].text = "XP+ " + gorevXp.ToString();
            }
        }
    }
    public void BackUpMissions()
    {
        if (level1Mission)
        {
            if (PlayerPrefs.GetInt("Level1Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level1Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level1Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
        }

        if (level2Mission)
        {
            if (PlayerPrefs.GetInt("Level2Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level2Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level2LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level2Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
        }

        if (level3Mission)
        {
            if (PlayerPrefs.GetInt("Level3Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level3Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level3LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level3Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
        }

        if (level4Mission)
        {
            if (PlayerPrefs.GetInt("Level4Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level4Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level4LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level4Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
        }

        if (level5Mission)
        {
            if (PlayerPrefs.GetInt("Level5Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level5Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level5LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level5Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
        }

        if (level6Mission)
        {
            if (PlayerPrefs.GetInt("Level6Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level6Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level6LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 4)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level6Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionCorrectimgs[3].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
        }

        if (level7Mission)
        {
            if (PlayerPrefs.GetInt("Level7Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level7Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level7LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 8)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level7Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionCorrectimgs[3].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
        }

        if (level8Mission)
        {
            if (PlayerPrefs.GetInt("Level8Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level8Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level8LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 12)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level8Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionCorrectimgs[3].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
        }

        if (level9Mission)
        {
            if (PlayerPrefs.GetInt("Level9Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level9Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level9LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 16)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level9Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionCorrectimgs[3].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
        }

        if (level10Mission)
        {
            if (PlayerPrefs.GetInt("Level10Hedef1") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionimgs[0].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level10Hedef2") == 1)
            {
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level10LockedUnit") == kilitliAlanSayisi)
            {
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Garden") == 20)
            {
                missionCorrectimgs[3].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
            if (PlayerPrefs.GetInt("Level10Hedef") == 1)
            {
                missionCorrectimgs[0].gameObject.SetActive(true);
                missionCorrectimgs[1].gameObject.SetActive(true);
                missionCorrectimgs[2].gameObject.SetActive(true);
                missionCorrectimgs[3].gameObject.SetActive(true);

                missionimgs[0].gameObject.SetActive(true);
                missionimgs[1].gameObject.SetActive(true);
                missionimgs[2].gameObject.SetActive(true);
                missionimgs[3].gameObject.SetActive(true);
            }
        }
    }
    void BahceOnIslemler()
    {
        if (PlayerPrefs.GetInt("Garden1") == 1)
        {
            pricebtns[0].gameObject.SetActive(false);
            buyedpnls[0].gameObject.SetActive(true);
            gardengobtns[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden2") == 1)
        {
            pricebtns[1].gameObject.SetActive(false);
            buyedpnls[1].gameObject.SetActive(true);
            gardengobtns[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden3") == 1)
        {
            pricebtns[2].gameObject.SetActive(false);
            buyedpnls[2].gameObject.SetActive(true);
            gardengobtns[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden4") == 1)
        {
            pricebtns[3].gameObject.SetActive(false);
            buyedpnls[3].gameObject.SetActive(true);
            gardengobtns[3].gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Garden5") == 1)
        {
            pricebtns[0].gameObject.SetActive(false);
            buyedpnls[0].gameObject.SetActive(true);
            gardengobtns[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden6") == 1)
        {
            pricebtns[1].gameObject.SetActive(false);
            buyedpnls[1].gameObject.SetActive(true);
            gardengobtns[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden7") == 1)
        {
            pricebtns[2].gameObject.SetActive(false);
            buyedpnls[2].gameObject.SetActive(true);
            gardengobtns[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden8") == 1)
        {
            pricebtns[3].gameObject.SetActive(false);
            buyedpnls[3].gameObject.SetActive(true);
            gardengobtns[3].gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Garden9") == 1)
        {
            pricebtns[0].gameObject.SetActive(false);
            buyedpnls[0].gameObject.SetActive(true);
            gardengobtns[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden10") == 1)
        {
            pricebtns[1].gameObject.SetActive(false);
            buyedpnls[1].gameObject.SetActive(true);
            gardengobtns[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden11") == 1)
        {
            pricebtns[2].gameObject.SetActive(false);
            buyedpnls[2].gameObject.SetActive(true);
            gardengobtns[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden12") == 1)
        {
            pricebtns[3].gameObject.SetActive(false);
            buyedpnls[3].gameObject.SetActive(true);
            gardengobtns[3].gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Garden13") == 1)
        {
            pricebtns[0].gameObject.SetActive(false);
            buyedpnls[0].gameObject.SetActive(true);
            gardengobtns[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden14") == 1)
        {
            pricebtns[1].gameObject.SetActive(false);
            buyedpnls[1].gameObject.SetActive(true);
            gardengobtns[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden15") == 1)
        {
            pricebtns[2].gameObject.SetActive(false);
            buyedpnls[2].gameObject.SetActive(true);
            gardengobtns[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden16") == 1)
        {
            pricebtns[3].gameObject.SetActive(false);
            buyedpnls[3].gameObject.SetActive(true);
            gardengobtns[3].gameObject.SetActive(true);
        }


        if (PlayerPrefs.GetInt("Garden17") == 1)
        {
            pricebtns[0].gameObject.SetActive(false);
            buyedpnls[0].gameObject.SetActive(true);
            gardengobtns[0].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden18") == 1)
        {
            pricebtns[1].gameObject.SetActive(false);
            buyedpnls[1].gameObject.SetActive(true);
            gardengobtns[1].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden19") == 1)
        {
            pricebtns[2].gameObject.SetActive(false);
            buyedpnls[2].gameObject.SetActive(true);
            gardengobtns[2].gameObject.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Garden20") == 1)
        {
            pricebtns[3].gameObject.SetActive(false);
            buyedpnls[3].gameObject.SetActive(true);
            gardengobtns[3].gameObject.SetActive(true);
        }
    }
}
