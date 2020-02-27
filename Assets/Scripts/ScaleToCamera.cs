using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScaleToCamera : MonoBehaviour
{
    [SerializeField]
    Vector2 size;

    [SerializeField]
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        size = sprite.size;// / MenuManager.Instance.GetCanvasScale();

        //transform.localScale = new Vector3(size.x, size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
