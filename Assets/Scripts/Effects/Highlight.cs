using System.Collections.Generic;
using UnityEngine;

public class Hightlight : MonoBehaviour
{
    [SerializeField]
    private Color color = Color.white;

    private Renderer[] renderers;
    //helper list to cache all the materials ofd this object
    private List<Material> materials;
    private const float INTENSITY = .15f;

    //Gets all the materials from each renderer
    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            //A single child-object might have mutliple materials on it
            //that is why we need to all materials with "s"
            materials.AddRange(new List<Material>(renderer.materials));
        }
        color = color * INTENSITY;
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                //We need to enable the EMISSION
                material.EnableKeyword("_EMISSION");
                //before we can set the color
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                //we can just disable the EMISSION
                //if we don't use emission color anywhere else
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
