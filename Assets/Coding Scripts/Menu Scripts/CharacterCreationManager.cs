using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterCreationManager : MonoBehaviour
{
    [SerializeField] GameObject raceClassPrefabObject;
    List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] Transform parentRace;
    [SerializeField] Transform parentClass;
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject creationMainPanel;
    [SerializeField] GameObject raceSelectPanel;
    [SerializeField] GameObject classSelectPanel;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject returnButton;
    [SerializeField] GameObject raceDisplayObject;
    [SerializeField] GameObject classDisplayObject;
    [SerializeField] TMP_InputField inputTextName;



    [SerializeField] TextMeshProUGUI titleTextTMP;
    [SerializeField] string creationMainText;
    [SerializeField] string raceSelectText;
    [SerializeField] string classSelectText;
    bool loadingRacesBool;
    bool loadingClassesBool;

    #region //Bonus stat modifiers
    [SerializeField] public int bonusAttackModifer;
    [SerializeField] public int bonusDefenseModifer;
    [SerializeField] public int holdingCapacityModifer;
    [SerializeField] public int bonusMagicAttackModifer;
    [SerializeField] public int bonusMagicDefenseModifer;
    [SerializeField] public int spellbookCapacityModifer;
    [SerializeField] public int movementModifer;
    #endregion

    private void Start() 
    {
        returnButton.SetActive(false);
        backButton.SetActive(true);
    }

    private void LoadRacesAndClasses()
    {
        foreach (GameObject go in prefabs)
        {
            Destroy(go);
        }
        prefabs.Clear();

        PlayerInfo playerInfo = NewSaveSystem.FindPlayerInfoFile();
        foreach (UnlockObject unlock in playerInfo.unlocks)
        {
            if (loadingRacesBool && unlock.unlockedRace != null)
            {
                GameObject _raceObject = Instantiate(raceClassPrefabObject, transform.position, transform.rotation);
                _raceObject.transform.SetParent(parentRace);
                _raceObject.GetComponent<CharacterUnlockItemInfo>().raceToUnlock = unlock.unlockedRace;
                _raceObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
                prefabs.Add(_raceObject);
            }
            else if (loadingClassesBool && unlock.unlockedClass != null)
            {
                Debug.Log("class");
                GameObject _classObject = Instantiate(raceClassPrefabObject, transform.position, transform.rotation);
                _classObject.transform.SetParent(parentClass);
                _classObject.GetComponent<CharacterUnlockItemInfo>().classToUnlock = unlock.unlockedClass;
                _classObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
                prefabs.Add(_classObject);
            }
        }

        SetUpListner();

    }

    private void SetUpListner() 
    {
        foreach (GameObject go in prefabs)
        {
            go.GetComponent<CharacterUnlockItemInfo>().onSelectedRaceOrClass += Subscriber_OnEventClicked;
        }
    }

    private void Subscriber_OnEventClicked(object sender, CharacterUnlockItemInfo.SelectedClassEventArgs e)
    {
        if (e.selectedRace != null)
        {
            raceDisplayObject.GetComponent<CharacterUnlockItemInfo>().raceToUnlock = e.selectedRace;
            raceDisplayObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
            raceDisplayObject.GetComponent<CharacterUnlockItemInfo>().DisplayName();
        }
        else if (e.selectedClass != null)
        {
            classDisplayObject.GetComponent<CharacterUnlockItemInfo>().classToUnlock = e.selectedClass;
            classDisplayObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
            classDisplayObject.GetComponent<CharacterUnlockItemInfo>().DisplayName();
        }

        DisplayCreationMainPanel();

    }

    public void DisplayCreationMainPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); 
        }

        creationMainPanel.SetActive(true);
        titleTextTMP.text = creationMainText;

        loadingClassesBool = false;
        loadingRacesBool = false;

        returnButton.SetActive(false);
        backButton.SetActive(true);

    }

    public void DisplayRaceSelectPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); 
        }

        raceSelectPanel.SetActive(true);
        titleTextTMP.text = raceSelectText;

        loadingRacesBool = true;
        LoadRacesAndClasses();

        returnButton.SetActive(true);
        backButton.SetActive(false);

    }

    public void DisplayClassSelectPanel()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false); 
        }

        classSelectPanel.SetActive(true);
        titleTextTMP.text = classSelectText;

        loadingClassesBool = true;
        LoadRacesAndClasses();

        returnButton.SetActive(true);
        backButton.SetActive(false);

    }

    public void SaveNewCharacter()
    {
        Race _race = raceDisplayObject.GetComponent<CharacterUnlockItemInfo>().raceToUnlock;
        Class _class = classDisplayObject.GetComponent<CharacterUnlockItemInfo>().classToUnlock;
        if (_race == null || _class == null)
        {
            Debug.LogError("one not selected");
            return;
        }

        int numberofCharactersPrior = NewSaveSystem.NumberOfCharacters();

        if (inputTextName.text == "")
        {
            Debug.Log("Need a name");
            inputTextName.text = "I'm Silly Billy";
        }

        //Stats Calculated
        int _baseHealth = _class.health + _race.health;
        int _baseStamina = _class.stamina + _race.stamina;
        int _baseMagic = _class.magic + _race.magic;
        int _baseIntelligence = _class.intelligence + _race.intelligence;
        int _baseStrength = _class.strength + _race.strength;
        int _baseSpeed = _class.speed + _race.speed;

        //Bonus Stats
        int _bonusAttack = Mathf.FloorToInt(_baseStrength / bonusAttackModifer);
        int _bonusDefense = Mathf.FloorToInt(_baseStrength / bonusDefenseModifer);
        int _holdingCapacity = Mathf.FloorToInt(_baseStrength / holdingCapacityModifer);

        int _bonusMagicAttack = Mathf.FloorToInt(_baseIntelligence / bonusMagicAttackModifer);
        int _bonusMagicDefense = Mathf.FloorToInt(_baseIntelligence / bonusMagicDefenseModifer);
        int _spellbookCapacity = Mathf.FloorToInt(_baseIntelligence / spellbookCapacityModifer);

        int _movement = Mathf.FloorToInt(_baseSpeed / movementModifer);

        //Character Info
        SaveObject saveObject = new SaveObject
        {
            //Basic Info
            nameOfCharacter = inputTextName.text.ToString(), //does it need to be converted? #TODO
            characterFileNumber = numberofCharactersPrior + 1,
            characterClass = _class.name,
            race = _race.name,
            raceObject = _race,
            classObject = _class,

            //BaseStats
            baseHealth = _baseHealth,
            baseStamina = _baseStamina,
            baseMagic = _baseMagic,
            baseIntelligence = _baseIntelligence,
            baseStrength = _baseStrength,
            baseSpeed = _baseSpeed,

            currentHealth = _baseHealth,
            currentStamina = _baseStamina,
            currentMagic = _baseMagic,
            currentIntelligence = _baseIntelligence,
            currentStrength = _baseStrength,
            currentSpeed = _baseSpeed,

            //Bonus Stats
            bonusAttack = _bonusAttack,
            bonusDefense = _bonusDefense,
            holdingCapacity = _holdingCapacity,
            bonusMagicAttack = _bonusMagicAttack,
            bonusMagicDefense = _bonusMagicDefense,
            spellbookCapacity = _spellbookCapacity,
            movement = _movement,

            //Level Info
            level = 1,
            hasLevelUp = true,
            levelPoints = 3,
            levelRolls = 3,                        

        };

        SaveState saveState = new SaveState { numberOfCharacters = numberofCharactersPrior+1 };

        string character = JsonUtility.ToJson(saveObject);

        // Debug.Log(numberofCharactersPrior+1 + " number of characters now");
        NewSaveSystem.SaveCharacter(character, numberofCharactersPrior+1);
        NewSaveSystem.SaveStateOfGame(saveState);
    }
}
