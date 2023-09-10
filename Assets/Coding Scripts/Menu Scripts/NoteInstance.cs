using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] public TMP_InputField noteFieldTMP;
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
}
