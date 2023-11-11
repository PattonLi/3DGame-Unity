using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class DiskFactory : MonoBehaviour
{
    public GameObject diskPrefab;

    private List<GameObject> used;
    private List<GameObject> free;
    private int diskIndex;

    public GameObject GetDisk(Ruler ruler)
    {
        GameObject disk;

        // 如果free没有空闲就创建
        if (free.Count==0)
        {
            disk = GameObject.Instantiate(diskPrefab, Vector3.zero, Quaternion.identity);
            disk.name = "UFO" + diskIndex;
            Debug.LogFormat("factory create a disk, index is {0}",diskIndex);
            disk.AddComponent(typeof(Disk));
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

        // 初始化disk feature
        disk.GetComponent<Disk>().diskFeature = ruler.diskFeature;

        // initial disk color
        Renderer render = disk.GetComponent<Renderer>();
        if (ruler.diskFeature.color == DiskFeature.colors.white)
        {
            render.material.color = Color.red;
        }
        else if (ruler.diskFeature.color == DiskFeature.colors.blue)
        {
            render.material.color = Color.blue;
        }
        else if (ruler.diskFeature.color == DiskFeature.colors.black)
        {
            render.material.color = Color.black;
        }
        else if (ruler.diskFeature.color == DiskFeature.colors.yellow)
        {
            render.material.color = Color.yellow;
        }
        else if (ruler.diskFeature.color == DiskFeature.colors.green)
        {
            render.material.color= Color.green;
        }

        // set disk position and scale
        disk.transform.localScale = new Vector3(1f, (float)ruler.diskFeature.size, 1f);
        disk.transform.position = ruler.startPos;
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
