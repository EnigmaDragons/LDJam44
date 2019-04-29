using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceStationSkin : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public List<Material> Materials;
    public MutableSpaceStation SpaceStation;

    void Start()
    {
        MeshRenderer.material = Materials.First(x => x.name == SpaceStation.Skin);
    }
}
