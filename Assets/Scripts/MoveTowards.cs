using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    float speed;

    [SerializeField]
    Vector2 moveDir;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDelta = (moveDir * speed * Time.deltaTime);
        transform.position += new Vector3(moveDelta.x, moveDelta.y, 0);
    }

    public void SetTarget(Vector2 moveDir, float speed)
    {
        this.moveDir = moveDir;
        this.speed = speed;
    }
}
