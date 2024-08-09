using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour
{
    [SerializeField] RectTransform joytsickOutline;
    [SerializeField] RectTransform joytsickButton;

    [SerializeField] float moveFactor;
    bool canControlJoystick;
    Vector3 move;
    Vector3 tapPztsn;

    public void ShowJoystick()
    {
        joytsickOutline.gameObject.SetActive(true);
        canControlJoystick = true;
    }
    public void HideJoystick()
    {
        joytsickOutline.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero;
    }
    public Vector3 GetPozition()
    {
        return move / 1.75f;
    }
    public void TappedOnJoystick()
    {
        tapPztsn = Input.mousePosition;
        joytsickOutline.transform.position = tapPztsn;
        ShowJoystick();
    }
    void JoystickControl()
    {
        Vector3 currentPztsn = Input.mousePosition;
        Vector3 direction = currentPztsn - tapPztsn;

        float canvasYScale = GetComponentInParent<Canvas>().GetComponentInParent<RectTransform>().localScale.y;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;

        float joytsickOutlineHalfWidth = joytsickOutline.rect.width / 2;
        float newWidth = joytsickOutlineHalfWidth * canvasYScale;

        moveMagnitude = Mathf.Min(moveMagnitude, newWidth);

        move = moveMagnitude * direction.normalized;

        Vector3 targetScale = tapPztsn + move;
        joytsickButton.position = targetScale;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }
    private void Update()
    {
        if (canControlJoystick)
        {
            JoystickControl();
        }
    }
}
