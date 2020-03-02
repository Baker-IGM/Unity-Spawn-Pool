﻿using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RectGameObject : MonoBehaviour
{
    [SerializeField]
    protected RectTransform rect;

    [SerializeField]
    protected Vector2 position;
    public Vector2 Position
    {
        get
        {
            return position;
        }
    }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
