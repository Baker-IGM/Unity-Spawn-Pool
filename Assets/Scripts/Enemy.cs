using UnityEngine;

[RequireComponent(typeof(LookAt))]
public class Enemy : MonoBehaviour
{
    LookAt lookAtScript;

    // Start is called before the first frame update
    void Start()
    {
        lookAtScript = GetComponent<LookAt>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAtScript.TargetPosition = GameManager.Instance.GetPlayerLocation();
    }
}
