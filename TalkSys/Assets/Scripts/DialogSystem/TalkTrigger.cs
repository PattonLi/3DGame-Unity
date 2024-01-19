using System;
using UnityEngine;

public class TalkTrigger : MonoBehaviour
{
    private GameObject tipsButton;//对话提示按钮
    [NonSerialized]
    public Dialogue dialogue;//对话内容
    
    [Header("对话框")]
    public GameObject dialogBox;

    public static TalkTrigger instance;
    private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}else{
			if(instance != this){
				Destroy(gameObject);
			}
		}
		DontDestroyOnLoad(gameObject);
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        tipsButton = other.transform.Find("对话提示").gameObject;
        dialogue = other.GetComponent<NPC>().dialogue;
        tipsButton.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        tipsButton.SetActive(false);
        dialogBox.SetActive(false);
    }
    private void Update()
    {
        if (tipsButton != null && tipsButton.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            dialogBox.SetActive(true);
        }
    }
}