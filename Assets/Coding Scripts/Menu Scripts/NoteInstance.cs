using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NoteInstance : MonoBehaviour
{
    [SerializeField] public TMP_InputField noteFieldTMP;
    [SerializeField] Image imageBG;
    [SerializeField] int charCheckNum;
    [SerializeField] float amountToResize;
    private void Start() 
    {
        noteFieldTMP.onValueChanged.AddListener(OnInputValueChanged);

        float preferredHeight = noteFieldTMP.textComponent.preferredHeight;
        RectTransform rect = noteFieldTMP.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, preferredHeight);
    }
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

    private void OnInputValueChanged(string newText)
    {
        // noteFieldTMP.text = newText;

        float preferredHeight = noteFieldTMP.textComponent.preferredHeight;
        RectTransform rect = noteFieldTMP.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, preferredHeight);
    }

}
