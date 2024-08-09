using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMovement : MonoBehaviour
{
    [SerializeField] JoystickController jc;
    [SerializeField] FixedJoystickController fjc;
    [SerializeField] RectTransform rtjc;
    [SerializeField] RectTransform rtfjc;
    [SerializeField] PlayerMovement pm;
    [SerializeField] Camera cm;
    CharacterController cc;

    [SerializeField] GameObject[] fieldAreas;

    Vector3 moveVector;
    int moveSpeed = 50;
    public bool choosed;
    public bool isOpencm;

    private void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }
    public void MovePlayer()
    {
        if (!isOpencm)
        {
            if (GameManager.instance.movedJoystick)
            {
                rtjc.gameObject.SetActive(true);
            }
            else if (GameManager.instance.fixedJoystick)
            {
                rtfjc.gameObject.SetActive(true);
            }

            if (GameManager.instance.movedJoystick)
            {
                moveVector = jc.GetPozition() * moveSpeed * Time.deltaTime / Screen.width;
            }
            else
            {
                moveVector = fjc.GetPozition() * moveSpeed * Time.deltaTime / Screen.width;
            }
            moveVector.z = moveVector.y;
            moveVector.y = 0;
            cc.Move(moveVector);
        }
    }
    private void Update()
    {
        MovePlayer();

        if (Input.GetMouseButtonUp(0))
        {
            if (choosed)
            {
                cc.enabled = false;
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 0, transform.position.z), 1f);

                for (int i = 0; i < fieldAreas.Length; i++)
                {
                    fieldAreas[i].SetActive(false);
                }

                rtjc.gameObject.SetActive(false);
                rtfjc.gameObject.SetActive(false);
                isOpencm = true;
            }
        }
    }
    private void LateUpdate()
    {
        if (isOpencm)
        {
            cm.gameObject.SetActive(false);
            pm.shop = false;
            pm.gameObject.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fieldarea"))
        {
            choosed = true;
        }
    }
}
