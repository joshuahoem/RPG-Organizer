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
    [SerializeField] int diceSceneIndex = 13;
    [SerializeField] int jukeboxIndex = 14;
    [SerializeField] int NoteSceneIndex = 15;



    public void LoadMainMenu ()
    {
        scene = mainMenuSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharactersMenu()
    {
        scene = characterSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterCreationMenu()
    {
        scene = characterCreationSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadOnline()
    {
        scene = onlineSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterInfoScene()
    {
        scene = CharacterInfoSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadGameScene()
    {
        scene = GameSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadOnlineLoadingScene()
    {
        scene = onlineLoadingSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }
    public void LoadMapScene()
    {
        scene = mapSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }
    public void LoadOptionScene()
    {
        scene = optionSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadAbilitieScene()
    {
        scene = abilitieScene;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadGlossaryScene()
    {
        scene = glossarySceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadEnemyScene()
    {
        scene = enemySceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadCharacterUnlockScene()
    {
        scene = charactersUnlockSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadDiceScene()
    {
        scene = diceSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadJukeBoxScene()
    {
        scene = jukeboxIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }

    public void LoadNoteScene()
    {
        scene = NoteSceneIndex;
        PlaySound();
        Invoke("LoadNextScene", sceneLoadTime);
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(scene);
    }

    public void PlaySound()
    {
        MusicSoundHandler musicSoundHandler = FindObjectOfType<MusicSoundHandler>();
        if (musicSoundHandler != null)
        {
            musicSoundHandler.PlayButtonSFX();
        }
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
