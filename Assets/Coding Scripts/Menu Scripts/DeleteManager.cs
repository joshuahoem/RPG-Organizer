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
            Debug.Log(go);
            Destroy(go);
        }
        deleteManager.deleteObjects.Clear();

        Debug.Log("loading");
        string SAVE_FOLDER = Application.dataPath + "/Saves/";
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");

        for (int i=0; i < saveFiles.Length+2; i++)
        {
            if (File.Exists(SAVE_FOLDER + "/save_" + i + ".txt"))
            {
                GameObject newDeleteObject = Instantiate(deletePrefab, transform.position, transform.rotation);
                newDeleteObject.transform.SetParent(deleteManager.parentTransform);
                newDeleteObject.GetComponent<CharacterUpdate>().LoadCharacter(i);
                deleteManager.deleteObjects.Add(newDeleteObject);
            }   
        }
    }

    public void DeleteCharacter()
    {
        string selectedCharacter = EventSystem.current.currentSelectedGameObject.name;
        string SAVE_FOLDER = Application.dataPath + "/Saves/";

        // var path = AssetDatabase.GUIDToAssetPath(SAVE_FOLDER + "/save_" + selectedCharacter + ".txt");

        File.Delete(SAVE_FOLDER + "/save_" + selectedCharacter + ".txt");
        UnityEditor.AssetDatabase.Refresh();

        LoadCharacterFiles();

        SaveState saveState = NewSaveSystem.FindSaveState();

        saveState.numberOfCharacters--;

        NewSaveSystem.SaveStateOfGame(saveState);
         
    }
}
