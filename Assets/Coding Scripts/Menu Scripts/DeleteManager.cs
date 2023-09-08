using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.EventSystems;

public class DeleteManager : MonoBehaviour
{
    [SerializeField] GameObject deletePrefab;
    [SerializeField] Transform parentTransform;
    public List<GameObject> deleteObjects = new List<GameObject>();

    private void Start() 
    {
        LoadCharacterFiles();
    }

    private void LoadCharacterFiles()
    {
        DeleteManager deleteManager = FindObjectOfType<DeleteManager>();
        foreach (GameObject go in deleteManager.deleteObjects)
        {
            // Debug.Log(go);
            Destroy(go);
        }
        deleteManager.deleteObjects.Clear();

        SaveManagerVersion3.Init();

        Dictionary<string, SaveObject> registry = CharacterRegistry.Instance.GetDictionary();

        foreach(var kvp in registry)
        {
            GameObject newDeleteObject = Instantiate(deletePrefab, transform.position, transform.rotation);
            newDeleteObject.transform.SetParent(deleteManager.parentTransform, false);
            newDeleteObject.GetComponent<CharacterUpdate>().LoadCharacter(kvp.Value);
            deleteManager.deleteObjects.Add(newDeleteObject);
        }

        // DirectoryInfo directoryInfo = new DirectoryInfo(SaveManagerVersion3.SAVE_FOLDER);
        // FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");

        // for (int i=0; i < saveFiles.Length+2; i++)
        // {
        //     if (File.Exists(SaveManagerVersion3.SAVE_FOLDER + "/save_" + i + ".txt"))
        //     {
        //         GameObject newDeleteObject = Instantiate(deletePrefab, transform.position, transform.rotation);
        //         newDeleteObject.transform.SetParent(deleteManager.parentTransform, false);
        //         newDeleteObject.GetComponent<CharacterUpdate>().LoadCharacter(i);
        //         deleteManager.deleteObjects.Add(newDeleteObject);
        //     }   
        // }
    }

    public void DeleteCharacter()
    {
        string selectedCharacter = EventSystem.current.currentSelectedGameObject.name;

        CharacterRegistry.Instance.RemoveCharacter(selectedCharacter);

        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);

        // File.Delete(SaveManagerVersion3.SAVE_FOLDER + "/save_" + selectedCharacter + ".txt");
        // File.Delete(SaveManagerVersion3.SAVE_FOLDER + "/save_" + selectedCharacter + ".txt.meta");

        #if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
        #endif

        LoadCharacterFiles();

        SaveState saveState = SaveManagerVersion3.FindSaveState();

        saveState.numberOfCharacters--;

        SaveManagerVersion3.SaveStateOfGame(saveState);
         
    }

    private void Test()
    {
        // SceneManager
    }
}
