using UnityEngine;



public class ScoreRecorder : MonoBehaviour
{
    private int score;

    public ScoreRecorder()
    { score = 0; }

    public ScoreRecorder(int score)
    { this.score = score; }

    public int GetScore()
    {
        return score;
    }

    // record the score
    public void Record(Disk disk)
    {
        // 飞碟大小为1得3分，大小为2得2分，大小为3得1分
        int diskSize = disk.diskFeature.size;
        switch (diskSize)
        {
            case 1:
                score += 3;
                break;
            case 2:
                score += 2;
                break;
            case 3:
                score += 1;
                break;
            default: break;
        }

        // 速度越快分就越高
        score += disk.diskFeature.speed;

        // 颜色为红色得1分，颜色为黄色得2分，颜色为蓝色得3分
        DiskFeature.colors diskColor = disk.diskFeature.color;
        if (diskColor == DiskFeature.colors.red)
        {
            score += 1;
        }
        else if (diskColor == DiskFeature.colors.yellow)
        {
            score += 2;
        }
        else if (diskColor == DiskFeature.colors.blue)
        {
            score += 3;
        }
    }

    /* 重置分数，设为0 */
    public void Reset()
    {
        score = 0;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
