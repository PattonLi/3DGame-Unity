using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "create score config")]
public class Score : ScriptableObject
{
    public static Dictionary<int, int> siseScore;
    public static Dictionary<int, int> speedScore;
    public static Dictionary<DiskFeature.colors, int> colorScore;

}
