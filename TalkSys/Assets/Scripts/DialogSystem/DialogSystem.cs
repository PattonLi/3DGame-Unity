using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField, Header("目前逐字打印速度")]
    private float textSpeed;
    private float startTextSpeed;//开始逐字打印速度
    private Dialogue dialogue;//对话内容
    //索引
    private int index;

    //对话内容框
    TextMeshProUGUI dialogueContent;
    //名称框
    TextMeshProUGUI dialogueName;
    //头像框
    Image dialogueImage;
    bool isDialogue;//是否正在对话
    private void OnEnable()
    {
        dialogue = TalkTrigger.instance.dialogue;
        dialogueContent = transform.Find("内容").GetComponent<TextMeshProUGUI>();
        dialogueName = transform.Find("名字").GetComponent<TextMeshProUGUI>();
        dialogueImage = transform.Find("头像").GetComponent<Image>();

        //设置人物头像保持宽高比，防止压缩变形
        dialogueImage.preserveAspect = true;

        isDialogue = false;

        startTextSpeed = textSpeed;
        index = 0;
        Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && dialogue != null)
        {
            //如果正在对话，再次按下R，快速显示所有对话
            if (isDialogue)
            {
                textSpeed = 0;
            }
            else
            {
                //回复文本速度
                textSpeed = startTextSpeed;
                //对话播放完，关闭对话
                if (index == dialogue.dialogNodes.Length)
                {
                    gameObject.SetActive(false);
                    index = 0;
                }
                else
                {
                    //开始对话
                    Play();
                }
            }

        }
    }

    // Play 函数用于开始播放对话。
    private void Play()
    {
        // 获取当前对话节点，并更新索引值。
        DialogNode node = dialogue.dialogNodes[index++];

        // 设置对话内容、角色名称和头像
        // dialogueContent.text = node.content;
        StartCoroutine(SetTextUI(node));
        dialogueName.text = node.name;
        dialogueImage.sprite = node.sprite;
    }

    //逐字打印
    IEnumerator SetTextUI(DialogNode node)
    {
        isDialogue = true;
        dialogueContent.text = "";
        for (int i = 0; i < node.content.Length; i++)
        {
            dialogueContent.text += node.content[i];
            yield return new WaitForSeconds(textSpeed);
        }
        isDialogue = false;
    }
}
