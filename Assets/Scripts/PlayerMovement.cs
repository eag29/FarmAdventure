using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PowerUpControl puc;
    [SerializeField] JoystickController jc;
    [SerializeField] FixedJoystickController fjc;
    [SerializeField] RectTransform rtjc;
    [SerializeField] RectTransform rtfjc;
    [SerializeField] Camera cm;
    [SerializeField] Camera gardenShoopcm;
    [SerializeField] Camera Shoopcm;
    public CharacterController cc;
    Vector3 moveVector;
    [SerializeField] int moveSpeed;
    public Animator anm;
    [SerializeField] GameObject gardenShoppnl;
    [SerializeField] GameObject shoppnl;

    public bool gardenShopping, shop;
    float gravity = -9.981f;
    float gravityMultiply = 3f;
    float gravityVelovity;

    void Start()
    {
        if (!GameManager.instance.missionexit)
        {
            cc.enabled = false;
            rtjc.gameObject.SetActive(false);
            rtfjc.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (GameManager.instance.missionexit)
        {
            cc.enabled = true;

            if (GameManager.instance.movedJoystick)
            {
                rtjc.gameObject.SetActive(true);
            }
            else if (GameManager.instance.fixedJoystick)
            {
                rtfjc.gameObject.SetActive(true);
            }
        }

        MovePlayer();

        if (gardenShopping)
        {
            cm.gameObject.SetActive(false);
            gardenShoopcm.gameObject.SetActive(true);
        }
        else
        {
            gardenShoopcm.gameObject.SetActive(false);
            cm.gameObject.SetActive(true);
        }

        if (shop)
        {
            cm.gameObject.SetActive(false);
            Shoopcm.gameObject.SetActive(true);
        }
        else
        {
            shoppnl.SetActive(false);
            Shoopcm.gameObject.SetActive(false);
            cm.gameObject.SetActive(true);

            if (GameManager.instance.movedJoystick)
            {
                rtjc.gameObject.SetActive(true);
            }
            else if (GameManager.instance.fixedJoystick)
            {
                rtfjc.gameObject.SetActive(true);
            }
        }
        if (GameManager.instance.productsBuyed)
        {
            gameObject.SetActive(false);
            cc.enabled = false;
        }
    }
    private void LateUpdate()
    {
        if (GameManager.instance.productsBuyed & !GameManager.instance.oyunAksin)
        {
            cc.enabled = true;
        }
    }
    public void MovePlayer()
    {
        if (GameManager.instance.movedJoystick)
        {
            moveVector = jc.GetPozition() * moveSpeed * Time.deltaTime / Screen.width;

            if (puc.charactersPower)
            {
                moveSpeed = puc.pud.charactersSpeed;
            }
        }
        else
        {
            moveVector = fjc.GetPozition() * moveSpeed * Time.deltaTime / Screen.width;

            if (puc.charactersPower)
            {
                moveSpeed = puc.pud.charactersSpeed;
            }
        }
        moveVector.z = moveVector.y;
        moveVector.y = 0;

        ManageAnimations(moveVector);
        ApplyGravity();
        cc.Move(moveVector);
    }
    public void ManageAnimations(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            anm.Play("run");
            anm.transform.forward = move.normalized;
        }
        else
        {
            anm.Play("idle");
        }
    }
    public void ApplyGravity()
    {
        if (cc.isGrounded & gravityVelovity < 0.0f)
        {
            gravityVelovity = -1f;
        }
        else
        {
            gravityVelovity = gravity * gravityMultiply * Time.deltaTime;
        }
        moveVector.y = gravityVelovity;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("gardenShop"))
        {
            Time.timeScale = 0;
            GameManager.instance.gardenShop = true;
            gardenShopping = true;
            gardenShoppnl.SetActive(true);
            GameManager.instance.txts[8].text = GameManager.instance.coin.ToString();
        }
        else if (other.CompareTag("store"))
        {
            Time.timeScale = 0;
            shop = true;
            shoppnl.SetActive(true);
            GameManager.instance.txts[9].text = GameManager.instance.coin.ToString();

            if (!shop)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - 2f, transform.position.y, transform.position.z - 2f), 1f);
            }
        }
        else if (other.CompareTag("alangecis"))
        {
            GameManager.instance.backtomainGarden = true;
            GameManager.instance.pm2.transform.position = new Vector3(-62.7f, 0f, -9.5f);
        }
    }
}
