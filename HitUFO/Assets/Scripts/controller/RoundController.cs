using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum PhysiclaType
{
    pysical,
    noPhysical
}

public class RoundController : MonoBehaviour
{
    DiskActionManager actionManager;
    ScoreRecorder scoreRecorder;
    FirstController firstController;
    Ruler ruler;
    float time = 0;

    public PhysiclaType phyType;

    public void LaunchDisk()
    {
        // ʹ�ɵ�����λ�þ����ֿܷ����Ӳ�ͬλ�÷���ʹ�õ�����
        int[] beginPosY = new int[10];
        float beginPosX;

        // shoot the disks for each trail
        int diskNum = ruler.diskNumPerRound[ruler.roundIndex] / ruler.SumTrailNum;
        diskNum += Random.Range(0, 5);// random num per trail


        for (int i = 0; i < diskNum; ++i)
        {
            // ��ȡ�����
            int randomNum = Random.Range(1, 4);
            // �ɵ��ٶ���غ������Ӷ����
            ruler.diskFeature.speed = randomNum * (ruler.roundIndex + 2);

            // ���������ѡ��ɵ���ɫ
            randomNum = Random.Range(1, 4);
            if (randomNum == 1)
            {
                ruler.diskFeature.color = DiskFeature.colors.red;
            }
            else if (randomNum == 2)
            {
                ruler.diskFeature.color = DiskFeature.colors.green;
            }
            else
            {
                ruler.diskFeature.color = DiskFeature.colors.blue;
            }

            // ����ѡȡ������������������ѡ��ɵ��Ĵ�С
            ruler.diskFeature.size = Random.Range(1, 4);

            // ����ѡȡ������������������ѡ��ɵ�����ķ���
            randomNum = Random.Range(0, 2);
            Camera mainCamera = Camera.main;
            float cameraW = mainCamera.orthographicSize * 2f * Camera.main.aspect;

            if (randomNum == 1)
            {
                ruler.startDir = new Vector3(3, 1, 0);
                beginPosX= mainCamera.transform.position.x - cameraW / 2f;
            }
            else
            {
                ruler.startDir = new Vector3(-3, 1, 0);
                beginPosX = mainCamera.transform.position.x + cameraW / 2f;
            }

            // ����ѡȡ���������ʹ��ͬ�ɵ��ķ���λ�þ����ֿܷ�
            int iterNum = 0;
            
            do
            {
                // case if infinite iter
                iterNum++;
                if (iterNum>=100)
                {
                    int ii;
                    for (ii = 0; ii < beginPosY.Length; ii++)
                    {
                        beginPosY.SetValue(0, ii);
                    }
                }
                randomNum = Random.Range(0, 10);
            } while (beginPosY[randomNum] != 0);
            beginPosY[randomNum] = 1;
            ruler.startPos = new Vector3(beginPosX, -0.2f * randomNum, 0);

            // ����ruler�ӹ���������һ���ɵ�
            GameObject disk = Singleton<DiskFactory>.Instance.GetDisk(ruler);

            // ���÷ɵ��ķ��ж���
            actionManager.ShootDisk(disk, ruler.diskFeature.speed, ruler.startDir,phyType);
        }
    }

    /// <summary>
    /// free the gameobject that loaded the action
    /// </summary>
    /// <param name="obj">obj is the one load the action</param>
    public void FreeDisk(GameObject disk)
    {
        // call factory to free the disk
        Singleton<DiskFactory>.Instance.FreeDisk(disk);
    }

    // �û������ײ
    public void Hit(Vector3 position)
    {
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(position);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(ray);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject != null)
            {
                // �ѻ��еķɵ��Ƴ���Ļ�������ص��ͷ�
                hit.collider.gameObject.transform.position = new Vector3(0, -6, 0);
                // ��¼�ɵ��÷�
                scoreRecorder.Record(hit.collider.gameObject.GetComponent<Disk>());
                // ��ʾ��ǰ�÷�
                UserGUI userGUI = Singleton<UserGUI>.Instance;
                userGUI.SetScore(scoreRecorder.GetScore());
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("gameobject of roundcontroller is:" + this.gameObject);

        actionManager = gameObject.AddComponent<DiskActionManager>();
        scoreRecorder = gameObject.AddComponent<ScoreRecorder>();
        firstController = Director.GetInstance().CurrentSceneController as FirstController;
        ruler = firstController.rulerConfig;
        gameObject.AddComponent<DiskFactory>();
        
        
        
        ruler.roundIndex= 0;
        ruler.trailIndex= 0;

        Debug.Log("ruler config:");
        Debug.Log(JsonUtility.ToJson(ruler));

        phyType = PhysiclaType.noPhysical;
        Debug.Log("round controller's phytype is:"+phyType);
  
    }

    // Update is called once per frame
    void Update()
    {
        // when playing
        if (firstController.gameState == GameState.Playing)
        {
            //update usergui round index
            UpdateRoundInfo();
            //Debug.Log("round cont is playing update--");
            //Debug.Log("roundIndex" + ruler.roundIndex + "trailIndex" + ruler.trailIndex);
            time += Time.deltaTime;
            float shootDeltaTime = ruler.shootDeltaTimePerRound[ruler.roundIndex];

            // ����һ�ηɵ�(trial)
            if (time > shootDeltaTime)
            {
                // reset time
                time = 0;

                if (ruler.trailIndex<ruler.SumTrailNum && ruler.roundIndex<ruler.SumRoundNum)
                {
                    // ����ɵ�
                    Debug.LogFormat("call LaunchDisk at round controller--at round {0} trail {1}"
                        , ruler.roundIndex, ruler.trailIndex);
                    LaunchDisk();
                    ruler.trailIndex++;
                    // �غϼ�һ���������ɷɵ�����
                    if (ruler.trailIndex == ruler.SumTrailNum)
                    {
                        ruler.trailIndex = 0;
                        ruler.roundIndex++;
                    }
                }
                // ������Ϸ��������ʾ���½�����Ϸ
                else
                {
                    firstController.GameOver();
                }
                
            }
        }
    }

    private void UpdateRoundInfo()
    {
        Singleton<UserGUI>.Instance.SetIndex(ruler.roundIndex, ruler.trailIndex);
        Singleton<UserGUI>.Instance.SetRTNum(ruler.SumRoundNum, ruler.SumTrailNum);
        Singleton<UserGUI>.Instance.SetPhyMode(phyType);
    }

    
}
