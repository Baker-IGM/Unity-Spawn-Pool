using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    const string k_FIRE_AXIS = "Fire1";

    [SerializeField]
    float fireCoolDown;
    float fireTimer;

    Transform NewBullet;

	// Use this for initialization
	void Start () {
        //Debug.Log(transform.forward);
	}
	
	// Update is called once per frame
	void Update ()
    {
        #region Firing
        if (fireTimer > 0)
        {
            fireTimer -= Time.deltaTime;
        }

		if(fireTimer <= 0 && Input.GetAxis(k_FIRE_AXIS) > 0)
        {
            BulletManager.Instance.GetBullet().Fire(transform.position, transform.rotation);

            fireTimer = fireCoolDown;
        }
        #endregion

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(mouseWorldPos, -Vector3.forward);
    }
}