using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] public float sceneLoadTime = 1f;
    int scene;

    [SerializeField] int mainMenuSceneIndex = 0;
    [SerializeField] int characterSceneIndex = 1;
    [SerializeField] int characterCreationSceneIndex = 2;
    [SerializeField] int onlineLoadingSceneIndex = 3;
    [SerializeField] int onlineSceneIndex = 4;
    [SerializeField] int CharacterInfoSceneIndex = 5;
    [SerializeField] int GameSceneIndex = 6;
    [SerializeField] int mapSceneIndex = 7;
    [SerializeField] int optionSceneIndex = 8;
    [SerializeField] int abilitieScene = 9;
    [SerializeField] int glossarySceneIndex = 10;
    [SerializeField] int enemySceneIndex = 11;
    [SerializeField] int charactersUnlockSceneIndex = 12;

    private void Start() {
        Debug.Log("Screen height: " + Screen.height);
        Debug.Log("Screen width: " + Screen.width);
    }



    public void LoadMainMenu ()
    {
        scene = mainMenuSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharactersMenu()
    {
        scene = characterSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterCreationMenu()
    {
        scene = characterCreationSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadOnline()
    {
        scene = onlineSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterInfoScene()
    {
        scene = CharacterInfoSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadGameScene()
    {
        scene = GameSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadOnlineLoadingScene()
    {
        scene = onlineLoadingSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }
    public void LoadMapScene()
    {
        scene = mapSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }
    public void LoadOptionScene()
    {
        scene = optionSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadAbilitieScene()
    {
        scene = abilitieScene;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadGlossaryScene()
    {
        scene = glossarySceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadEnemyScene()
    {
        scene = enemySceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterUnlockScene()
    {
        scene = charactersUnlockSceneIndex;
        Invoke("LoadNextScene", sceneLoadTime);
    }


    

    private void LoadNextScene()
    {
        SceneManager.LoadScene(scene);
    }




    public void QuitGame()
    {
        Application.Quit();
        Invoke("QuitGameInvoke", sceneLoadTime);
    }

    private void QuitGameInvoke()
    {
        Application.Quit();
    }

    private void OnApplicationQuit() 
    {
        PlayerPrefs.SetInt("hasStarted", 0);
    }
}
