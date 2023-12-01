using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSActionManager : MonoBehaviour
{
    private Dictionary<int, SSAction> actions = new Dictionary<int, SSAction>();
    private List<SSAction> waitingAdd = new List<SSAction> ();
    private List<int> waitingDelete = new List<int>();
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        // add to actions
        foreach(SSAction ac in waitingAdd){
            actions[ac.GetInstanceID()] = ac;
        }
        waitingAdd.Clear();

        //run actions
        foreach(KeyValuePair<int, SSAction> kv in actions){
            SSAction ac = kv.Value;
            if(ac.destroy){
                waitingDelete.Add(ac.GetInstanceID());
            }
            else if(ac.enable){
                ac.Update();//call action update manually
            }
        }
        //delete actions
        foreach(int key in waitingDelete){
            SSAction ac = actions[key];
            actions.Remove(key);
            Destroy(ac);
        }
        waitingDelete.Clear();
    }

    public void RunAction(GameObject gameobject, SSAction action, ISSActionCallback manager){
        action.gameobject = gameobject;//add action to gameobject
        action.transform = gameobject.transform;//ser action's transform
        action.callback = manager;//set action's callback
        waitingAdd.Add(action);
        action.Start();//not monobehavior thus call start mannually
    }
}
