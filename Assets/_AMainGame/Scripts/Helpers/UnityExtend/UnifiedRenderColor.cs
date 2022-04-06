using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class UnifiedRenderColor : MonoBehaviour
{
    private delegate void ColorSetter(Color color);

    [SerializeField]
    private Color color = Color.white;

    [Space]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Graphic graphic;
    [SerializeField]
    private Shaper2D shaper2D;
    [SerializeField]
    private ProtoShape2D protoShape2D;

    public Color Color
    {
        get
        {
            return color;
        }

        set
        {
            ///
            if (colorSetter == null)
            {
                SetColorSetter();
            }

            ///
            color = value;

            ///
            colorSetter?.Invoke(color);

            ///
            set = true;
        }
    }

    private ColorSetter colorSetter;

    private bool set = false;

#if UNITY_EDITOR
    public void Reset()
    {
        GetRenderer();
    }
#endif

    public void Awake()
    {
        if (!set)
        {
            Color = color;
        }
    }

#if UNITY_EDITOR
    public void Update()
    {
        if (!Application.isPlaying)
        {
            Color = color;
        }
    }
#endif

    private void SetColorSetter()
    {
        if (spriteRenderer != null)
        {
            colorSetter = SpriteRendererColorSetter;
        }
        else if (graphic != null)
        {
            colorSetter = GraphicColorSetter;
        }
        else if (shaper2D != null)
        {
            colorSetter = Shaper2DColorSetter;
        }
        else if (protoShape2D != null)
        {
            colorSetter = ProtoShapeColorSetter;
        }
        else
        {
            colorSetter = null;
        }
    }

    private void GetRenderer()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        graphic = GetComponent<Graphic>();
        shaper2D = GetComponent<Shaper2D>();
        protoShape2D = GetComponent<ProtoShape2D>();
    }

    private void SpriteRendererColorSetter(Color color)
    {
        spriteRenderer.color = color;
    }

    private void GraphicColorSetter(Color color)
    {
        graphic.color = color;
    }

    private void Shaper2DColorSetter(Color color)
    {
        shaper2D.innerColor = color;
        shaper2D.outerColor = color;
    }

    private void ProtoShapeColorSetter(Color color)
    {
        protoShape2D.color1 = protoShape2D.color2 = protoShape2D.outlineColor = color;
        protoShape2D.UpdateMaterialSettings();
        protoShape2D.UpdateMesh();
    }
}
