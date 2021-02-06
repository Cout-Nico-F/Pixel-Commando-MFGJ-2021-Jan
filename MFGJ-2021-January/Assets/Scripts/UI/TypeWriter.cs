using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    //For multiple dialogues
    [TextArea]
    public string[] sentences;
    public int index;
    //Text Speed
    public float typingDelay;
    //For Text Pop up´s
    public bool isDialogueEnd = false; 

    private void Start()
    {
        textDisplay.text = "";
        StartCoroutine(Type());
    }

    //Type Writer -> Dialogue
    IEnumerator Type()
    {
        isDialogueEnd = false;
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingDelay);
        }
        Invoke("WaitSecond", 1.5f);
    }

    void WaitSecond()
    {
        isDialogueEnd = true;
    }
}
