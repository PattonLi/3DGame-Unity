using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ArrowFactory : MonoBehaviour
{
    public GameObject diskPrefab;

    private List<GameObject> used;
    private List<GameObject> free;
    private int diskIndex;

    public GameObject GetDisk(Vector3 startPos)
    {
        GameObject disk;

        // 如果free没有空闲就创建
        if (free.Count==0)
        {
            disk = GameObject.Instantiate(diskPrefab, Vector3.zero, Quaternion.identity);
            disk.name = "UFO" + diskIndex;
            Debug.LogFormat("factory create a disk, index is {0}",diskIndex);
            disk.AddComponent(typeof(ArrowFeature));
            diskIndex++;
        }
        else
        {
            int freeNum = free.Count;
            disk = free[freeNum-1];
            free.Remove(free[freeNum-1]);
        }

        // cache
        used.Add(disk);

        // set disk position and scale
        disk.transform.position = startPos;
        disk.SetActive(true);

        return disk;
    }

    public void FreeDisk(GameObject dd)
    {
        foreach (GameObject d in used)
        {
            if (d.GetInstanceID()==dd.GetInstanceID())
            {
                Debug.LogFormat("factory free a disk, name is {0}", d.name);
                d.SetActive(false);
                used.Remove(d);
                free.Add(d);
                break;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        diskPrefab = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/UFO"));
        diskPrefab.SetActive(false);
        Debug.Log("DiskFactory initial the diskPrefab");
        used = new List<GameObject>();
        free = new List<GameObject>();
        diskIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
