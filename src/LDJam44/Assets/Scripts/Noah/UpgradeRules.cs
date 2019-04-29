using UnityEngine;

[CreateAssetMenu(menuName = "Upgrade")]
public class UpgradeRules : ScriptableObject
{
    public string Name;
    public string Description;
    public int[] Costs;
    public float[] Effects;
}
