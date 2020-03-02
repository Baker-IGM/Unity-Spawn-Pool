using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class Enemy : RectGameObject
{
    LookAt lookAtScript;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        lookAtScript = GetComponent<LookAt>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAtScript.TargetPosition = GameManager.Instance.GetPlayerLocation();
    }
}
