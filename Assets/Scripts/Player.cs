using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(LookAt))]
public class Player : MonoBehaviour
{
    RectTransform rect;

    [SerializeField]
    Vector2 position;
    public Vector2 Position
    {
        get
        {
            return position;
        }
    }

    #region Look at vars
    LookAt lookAtScript;

    Vector2 lookAtVect;
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

        lookAtScript = GetComponent<LookAt>();
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(MenuManager.Instance.MainGameRect, value.Get<Vector2>(), Camera.main, out lookAtVect);

        lookAtScript.TargetPosition = lookAtVect;
    }
}