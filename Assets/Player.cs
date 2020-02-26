using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class Player : MonoBehaviour
{
    RectTransform rect, parentRect;

    #region Look at vars
    [SerializeField]
    Vector2 position;

    [SerializeField]
    float angle;

    [SerializeField]
    Vector2 lookAtVect;

    [SerializeField]
    Vector2 lookAtDelta;
    #endregion

    #region Firing vars
    const string k_FIRE_AXIS = "Fire1";

    [SerializeField]
    float fireCoolDown;
    float fireTimer;
    
    Transform NewBullet;
    #endregion

    #region Movement vars
    [SerializeField]
    float moveScalar = 1;

    Vector2 moveDelta;
    #endregion

    // Use this for initialization
    void Start ()
    {
        rect = GetComponent<RectTransform>();

        parentRect = transform.parent.GetComponent<RectTransform>();

        //MovePlayer(parentRect.rect.size / 2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region Firing
        //      if (fireTimer > 0)
        //      {
        //          fireTimer -= Time.deltaTime;
        //      }

        //if(fireTimer <= 0 && Input.GetAxis(k_FIRE_AXIS) > 0)
        //      {
        //          BulletManager.Instance.GetBullet().Fire(transform.position, transform.rotation);

        //          fireTimer = fireCoolDown;
        //      }
        #endregion

        //
        //      Player movement
        //
        MovePlayer(moveDelta);

        //
        //      Rotate to aim at the pointer
        //
        lookAtDelta = position - lookAtVect;

        

        if (lookAtDelta.y < 0)
        {
            //      1
            if(lookAtDelta.x < 0)
            {
                angle = Mathf.Atan(lookAtDelta.y / lookAtDelta.x);
            }
            //      2
            else
            {
                angle = Mathf.Atan(lookAtDelta.y / lookAtDelta.x) + Mathf.PI;
            }
        }
        else
        {
            //      3
            if (lookAtDelta.x >= 0)
            {
                angle = Mathf.Atan(lookAtDelta.y / lookAtDelta.x) + Mathf.PI;
            }
            //      4
            else
            {
                angle = Mathf.Atan(lookAtDelta.y / lookAtDelta.x) + (Mathf.PI * 2f);
            }
        }

        if (!float.IsNaN(angle))
        {
            angle *= Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }

    #region Movment Logic
    public void OnMove(InputValue value)
    {
        moveDelta = value.Get<Vector2>();

        moveDelta *= moveScalar;

        rect.anchoredPosition += moveDelta;
    }

    void MovePlayer(Vector2 delta)
    {
        rect.anchoredPosition += delta;

        position = rect.anchoredPosition;
    }
    #endregion

    public void OnLook(InputValue value)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, value.Get<Vector2>(), Camera.main, out lookAtVect);
    }
}