using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] public TMP_InputField noteFieldTMP;
    [SerializeField] Image imageBG;
    [SerializeField] int charCheckNum;
    [SerializeField] float amountToResize;
    public void Init(string note)
    {
        noteFieldTMP.text = note;
    }

    public void DeleteThisNote()
    {
        FindObjectOfType<NoteManager>().DeleteNote(noteFieldTMP.text);
    }

    public void SaveNotes()
    {
        FindObjectOfType<NoteManager>().SaveNotes();

    }

    public void BGResize()
    {
        float y = imageBG.rectTransform.localScale.y;
        float x = imageBG.rectTransform.localScale.x;

        for (int length = noteFieldTMP.text.Length; length > 0; length -= charCheckNum)
        {
            y += amountToResize;
        }

        imageBG.rectTransform.localScale = new Vector2(x,y);
    }
}
