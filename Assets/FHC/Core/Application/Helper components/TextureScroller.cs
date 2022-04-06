using UnityEngine;
using System.Collections;

namespace FH.Core.HelperComponent
{
    [RequireComponent(typeof(Renderer))]
    public class TextureScroller : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        Renderer targetRenderer;

        [SerializeField]
        Vector2 speed = Vector2.left;

        Material mainMaterial;

        public void Awake()
        {
            mainMaterial = targetRenderer.material;
        }

        public void Update()
        {
            Vector2 newOffset = mainMaterial.mainTextureOffset + speed * Time.deltaTime;
            newOffset.x = Mathf.Repeat(newOffset.x, 1);
            newOffset.y = Mathf.Repeat(newOffset.y, 1);
            mainMaterial.mainTextureOffset = newOffset;
        }

        public void Reset()
        {
            targetRenderer = GetComponent<Renderer>();
        }
    }

}