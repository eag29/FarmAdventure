using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        if (!PlayerPrefs.HasKey("Xp"))
        {
            PlayerPrefs.SetInt("Xp", 0);
            PlayerPrefs.SetInt("Sound", 1);
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("Joystick", 0);
            PlayerPrefs.SetInt("Garden1", 0);
            PlayerPrefs.SetInt("Garden2", 0);
            PlayerPrefs.SetInt("Garden3", 0);
            PlayerPrefs.SetInt("Garden4", 0);
            PlayerPrefs.SetInt("Garden5", 0);
            PlayerPrefs.SetInt("Garden6", 0);
            PlayerPrefs.SetInt("Garden7", 0);
            PlayerPrefs.SetInt("Garden8", 0);
            PlayerPrefs.SetInt("Garden9", 0);
            PlayerPrefs.SetInt("Garden10", 0);
            PlayerPrefs.SetInt("Garden11", 0);
            PlayerPrefs.SetInt("Garden12", 0);
            PlayerPrefs.SetInt("Garden13", 0);
            PlayerPrefs.SetInt("Garden14", 0);
            PlayerPrefs.SetInt("Garden15", 0);
            PlayerPrefs.SetInt("Garden16", 0);
            PlayerPrefs.SetInt("Garden17", 0);
            PlayerPrefs.SetInt("Garden18", 0);
            PlayerPrefs.SetInt("Garden19", 0);
            PlayerPrefs.SetInt("Garden20", 0);

            PlayerPrefs.SetInt("Level1Mission", 0);
            PlayerPrefs.SetInt("Level1Hedef", 0);
            PlayerPrefs.SetInt("Level1Hedef1", 0);
            PlayerPrefs.SetInt("Level1Hedef2", 0);
            PlayerPrefs.SetInt("Level1Product", 0);
            PlayerPrefs.SetInt("Level1SellProduct", 0);

            PlayerPrefs.SetInt("Level2Mission", 0);
            PlayerPrefs.SetInt("Level2Hedef", 0);
            PlayerPrefs.SetInt("Level2Hedef1", 0);
            PlayerPrefs.SetInt("Level2Hedef2", 0);
            PlayerPrefs.SetInt("Level2Product", 0);
            PlayerPrefs.SetInt("Level2SellProduct", 0);
            PlayerPrefs.SetInt("Level2LockedUnit", 0);

            PlayerPrefs.SetInt("Level3Mission", 0);
            PlayerPrefs.SetInt("Level3Hedef", 0);
            PlayerPrefs.SetInt("Level3Hedef1", 0);
            PlayerPrefs.SetInt("Level3Hedef2", 0);
            PlayerPrefs.SetInt("Level3Product", 0);
            PlayerPrefs.SetInt("Level3SellProduct", 0);
            PlayerPrefs.SetInt("Level3LockedUnit", 0);

            PlayerPrefs.SetInt("Level4Mission", 0);
            PlayerPrefs.SetInt("Level4Hedef", 0);
            PlayerPrefs.SetInt("Level4Hedef1", 0);
            PlayerPrefs.SetInt("Level4Hedef2", 0);
            PlayerPrefs.SetInt("Level4Product", 0);
            PlayerPrefs.SetInt("Level4SellProduct", 0);
            PlayerPrefs.SetInt("Level4LockedUnit", 0);

            PlayerPrefs.SetInt("Level5Mission", 0);
            PlayerPrefs.SetInt("Level5Hedef", 0);
            PlayerPrefs.SetInt("Level5Hedef1", 0);
            PlayerPrefs.SetInt("Level5Hedef2", 0);
            PlayerPrefs.SetInt("Level5Product", 0);
            PlayerPrefs.SetInt("Level5SellProduct", 0);
            PlayerPrefs.SetInt("Level5LockedUnit", 0);


            PlayerPrefs.SetInt("Level6Mission", 0);
            PlayerPrefs.SetInt("Level6Hedef", 0);
            PlayerPrefs.SetInt("Level6Hedef1", 0);
            PlayerPrefs.SetInt("Level6Hedef2", 0);
            PlayerPrefs.SetInt("Level6Product", 0);
            PlayerPrefs.SetInt("Level6SellProduct", 0);
            PlayerPrefs.SetInt("Level6LockedUnit", 0);

            PlayerPrefs.SetInt("Level7Mission", 0);
            PlayerPrefs.SetInt("Level7Hedef", 0);
            PlayerPrefs.SetInt("Level7Hedef1", 0);
            PlayerPrefs.SetInt("Level7Hedef2", 0);
            PlayerPrefs.SetInt("Level7Product", 0);
            PlayerPrefs.SetInt("Level7SellProduct", 0);
            PlayerPrefs.SetInt("Level7LockedUnit", 0);


            PlayerPrefs.SetInt("Level8Mission", 0);
            PlayerPrefs.SetInt("Level8Hedef", 0);
            PlayerPrefs.SetInt("Level8Hedef1", 0);
            PlayerPrefs.SetInt("Level8Hedef2", 0);
            PlayerPrefs.SetInt("Level8Product", 0);
            PlayerPrefs.SetInt("Level8SellProduct", 0);
            PlayerPrefs.SetInt("Level8LockedUnit", 0);

            PlayerPrefs.SetInt("Level9Mission", 0);
            PlayerPrefs.SetInt("Level9Hedef", 0);
            PlayerPrefs.SetInt("Level9Hedef1", 0);
            PlayerPrefs.SetInt("Level9Hedef2", 0);
            PlayerPrefs.SetInt("Level9Product", 0);
            PlayerPrefs.SetInt("Level9SellProduct", 0);
            PlayerPrefs.SetInt("Level9LockedUnit", 0);

            PlayerPrefs.SetInt("Level10Mission", 0);
            PlayerPrefs.SetInt("Level10Hedef", 0);
            PlayerPrefs.SetInt("Level10Hedef1", 0);
            PlayerPrefs.SetInt("Level10Hedef2", 0);
            PlayerPrefs.SetInt("Level10Product", 0);
            PlayerPrefs.SetInt("Level10SellProduct", 0);
            PlayerPrefs.SetInt("Level10LockedUnit", 0);

            SceneManager.LoadScene(1);
        }
        else
        {
            if (PlayerPrefs.GetInt("Xp") > 0 & PlayerPrefs.GetInt("Xp") < 900)
            {
                SceneManager.LoadScene(1);
            }
            if (PlayerPrefs.GetInt("Xp") >= 900 & PlayerPrefs.GetInt("Xp") < 2700)
            {
                SceneManager.LoadScene(2);
            }
            if (PlayerPrefs.GetInt("Xp") >= 2700 & PlayerPrefs.GetInt("Xp") < 5000)
            {
                SceneManager.LoadScene(3);
            }
            if (PlayerPrefs.GetInt("Xp") >= 5000 & PlayerPrefs.GetInt("Xp") < 10000)
            {
                SceneManager.LoadScene(4);
            }
            if (PlayerPrefs.GetInt("Xp") >= 10000 & PlayerPrefs.GetInt("Xp") < 20000)
            {
                SceneManager.LoadScene(5);
            }
            if (PlayerPrefs.GetInt("Xp") >= 20000 & PlayerPrefs.GetInt("Xp") < 35000)
            {
                SceneManager.LoadScene(6);
            }
            if (PlayerPrefs.GetInt("Xp") >= 35000 & PlayerPrefs.GetInt("Xp") < 55000)
            {
                SceneManager.LoadScene(7);
            }
            if (PlayerPrefs.GetInt("Xp") >= 55000 & PlayerPrefs.GetInt("Xp") < 70000)
            {
                SceneManager.LoadScene(8);
            }
            if (PlayerPrefs.GetInt("Xp") >= 70000 & PlayerPrefs.GetInt("Xp") < 100000)
            {
                SceneManager.LoadScene(9);
            }
            if (PlayerPrefs.GetInt("Xp") >= 100000 & PlayerPrefs.GetInt("Xp") < 140000)
            {
                SceneManager.LoadScene(10);
            }
        }
    }
}
