using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LookAt : MonoBehaviour
{
    RectTransform rect;

    [SerializeField]
    Vector2 targetPosition;

    public Vector2 TargetPosition
    {
        set
        {
            targetPosition = value;
        }
    }

    [SerializeField]
    Vector2 position;

    public Vector2 Position
    {
        get
        {
            return position;
        }
    }

    [SerializeField]
    float angle;

    [SerializeField]
    Vector2 lookAtDelta;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        position = rect.anchoredPosition;

        //
        //      Rotate to aim at the pointer
        //
        lookAtDelta = position - targetPosition;

        if (lookAtDelta.y < 0)
        {
            //      1
            if (lookAtDelta.x < 0)
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
}
