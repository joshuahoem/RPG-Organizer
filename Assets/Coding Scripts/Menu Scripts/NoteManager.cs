using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField] GameObject notePrefab;
    [SerializeField] Transform parentForNotes;

    List<GameObject> noteObjects = new List<GameObject>();
    List<string> notes = new List<string>();

    private void Start() 
    {
        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        foreach (string note in save.notes)
        {
            GameObject newNote = Instantiate(notePrefab, transform.position, transform.rotation);
            newNote.transform.SetParent(parentForNotes, false);

            newNote.GetComponent<NoteInstance>().Init(note);

            noteObjects.Add(newNote);
        }

        FindObjectOfType<ShopContentFitter>().FitContent(noteObjects.Count + 1);
    }

    public void NewNote()
    {
        GameObject newNote = Instantiate(notePrefab, transform.position, transform.rotation);
        newNote.transform.SetParent(parentForNotes, false);

        noteObjects.Add(newNote);

        FindObjectOfType<ShopContentFitter>().FitContent(noteObjects.Count + 1);
    }

    public void DeleteNote(string _note)
    {
        GameObject noteToDelete = null;
        foreach (GameObject go in noteObjects)
        {
            if (go.GetComponent<NoteInstance>().noteFieldTMP.text == _note)
            {
                noteToDelete = go;
            }
        }

        noteObjects.Remove(noteToDelete);
        Destroy (noteToDelete);

        SaveNotes();
    }

    public void SaveNotes()
    {
        notes = new List<string>();
        foreach (GameObject go in noteObjects)
        {
            notes.Add(go.GetComponent<NoteInstance>().noteFieldTMP.text);
        }

        SaveObject save = SaveManagerVersion3.FindCurrentSave();
        save.notes = notes;

        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
    }
}
