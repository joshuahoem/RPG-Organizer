using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] float sceneLoadTime = 1f;
    int scene;

    #region scene Index
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
    [SerializeField] int noteSceneIndex = 14;
    #endregion

    public void LoadMainMenu ()
    {
        scene = mainMenuSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadCharactersMenu()
    {
        scene = characterSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadCharacterCreationMenu()
    {
        scene = characterCreationSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadOnline()
    {
        scene = onlineSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadCharacterInfoScene()
    {
        scene = CharacterInfoSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadGameScene()
    {
        scene = GameSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadOnlineLoadingScene()
    {
        scene = onlineLoadingSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }
    public void LoadMapScene()
    {
        scene = mapSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }
    public void LoadOptionScene()
    {
        scene = optionSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadAbilitieScene()
    {
        scene = abilitieScene;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadGlossaryScene()
    {
        scene = glossarySceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadEnemyScene()
    {
        scene = enemySceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadCharacterUnlockScene()
    {
        scene = charactersUnlockSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadDiceScene()
    {
        scene = diceSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
    }

    public void LoadNoteScene()
    {
        scene = noteSceneIndex;
        PlaySound();
        Invoke(nameof(LoadNextScene), sceneLoadTime);
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
        Invoke(nameof(QuitGameInvoke), sceneLoadTime);
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
