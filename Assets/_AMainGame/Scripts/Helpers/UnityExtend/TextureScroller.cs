using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TextureScroller : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;

    new private Renderer renderer;

    public void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        renderer.material.mainTextureOffset += speed * Time.deltaTime;
    }
}
