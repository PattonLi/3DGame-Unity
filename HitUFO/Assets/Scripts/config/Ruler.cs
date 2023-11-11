
using System;
using UnityEngine;

[CreateAssetMenu(menuName ="create ruler config")]
public class Ruler : ScriptableObject
{
    // 对局属性
    public int trailIndex;
    public int roundIndex;
    
    public int SumRoundNum;
    public int SumTrailNum;

    public int[] diskNumPerRound;//avg disk num per round
    public float[] shootDeltaTimePerRound;//发射间隔

    // 飞碟属性
    public DiskFeature diskFeature;

    [NonSerialized]
    public Vector3 startPos;
    [NonSerialized]
    public Vector3 startDir;

}
