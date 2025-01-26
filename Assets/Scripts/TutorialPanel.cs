using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private List<GameObject> contentGroups;

    [Title("Debug")]
    [ReadOnly] [SerializeField] private int contentIndex = 0;

    public void ButtonEvent_OpenPanel()
    {
        anim.SetTrigger("Open");

        contentIndex = 0;
        UpdateContent();
    }

    public void ButtonEvent_ClosePanel()
    {
        anim.SetTrigger("Close");
    }

    public void ButtonEvent_Next()
    {
        contentIndex++;
        UpdateContent();
    }
    public void ButtonEvent_Prev()
    {
        contentIndex--;
        UpdateContent();
    }

    private void UpdateContent()
    {
        if(contentIndex < 0)
        {
            contentIndex = contentGroups.Count - 1;
        }
        if(contentIndex >= contentGroups.Count)
        {
            contentIndex = 0;
        }

        for(int i=0;i< contentGroups.Count; i++)
        {
            if(i == contentIndex)
            {
                contentGroups[i].SetActive(true);
            }
            else
            {
                contentGroups[i].SetActive(false);
            }
        }
    }
}
