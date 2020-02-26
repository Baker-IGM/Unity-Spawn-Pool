using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
public class Player : MonoBehaviour
{
    [SerializeField]
    RectTransform rect;
    [SerializeField]
    Vector2 position;
    
    const string k_FIRE_AXIS = "Fire1";

    [SerializeField]
    float fireCoolDown;
    float fireTimer;
    
    Transform NewBullet;

    [SerializeField]
    Vector2 lookAtVect;

    #region Movement
    [SerializeField]
    float moveScalar = 1;
    [SerializeField]
    Vector2 moveDelta;
    #endregion

    // Use this for initialization
    void Start ()
    {
        rect = GetComponent<RectTransform>();
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
        rect.anchoredPosition += moveDelta;
        position = rect.anchoredPosition;

        //
        //      Rotate to aim at the pointer
        //
        rect.LookAt(Camera.main.ScreenToWorldPoint(lookAtVect));
    }

    public void OnMove(InputValue value)
    {
        moveDelta = value.Get<Vector2>();

        moveDelta *= moveScalar;

        rect.anchoredPosition += moveDelta;
    }

    public void OnLook(InputValue value)
    {
        lookAtVect = value.Get<Vector2>();
    }
}