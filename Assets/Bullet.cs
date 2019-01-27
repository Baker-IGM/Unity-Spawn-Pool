using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    Rigidbody2D rBody;

    [SerializeField]
    float speed;
    Vector2 speedVect;

	// Use this for initialization
	void Awake () {
        rBody = GetComponent<Rigidbody2D>();

        speedVect = new Vector2(speed, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Fire(Vector2 position, Quaternion direction)
    {
        transform.position = position;

        transform.rotation = direction;

        gameObject.SetActive(true);

        rBody.AddForce(speedVect, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        if(!BulletManager.IsShuttingDown())
            BulletManager.Instance.ReturnBullet(transform);
    }
}