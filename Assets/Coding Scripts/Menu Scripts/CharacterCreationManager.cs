using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CharacterCreationManager : MonoBehaviour
{
    [SerializeField] GameObject raceClassPrefabObject;
    List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] Transform parentRace;
    [SerializeField] Transform parentClass;
    [SerializeField] Canvas canvas;

    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject creationMainPanel;
    [SerializeField] GameObject raceSelectPanel;
    [SerializeField] GameObject classSelectPanel;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject returnButton;
    [SerializeField] GameObject raceDisplayObject;
    [SerializeField] GameObject classDisplayObject;
    [SerializeField] TMP_InputField inputTextName;

    #region //stats display
    [SerializeField] TextMeshProUGUI raceHealthTMP;
    [SerializeField] TextMeshProUGUI raceStaminaTMP;
    [SerializeField] TextMeshProUGUI raceMagicTMP;
    [SerializeField] TextMeshProUGUI raceStregthTMP;
    [SerializeField] TextMeshProUGUI raceIntelligenceTMP;
    [SerializeField] TextMeshProUGUI raceSpeedTMP;

    [SerializeField] TextMeshProUGUI classHealthTMP;
    [SerializeField] TextMeshProUGUI classStaminaTMP;
    [SerializeField] TextMeshProUGUI classMagicTMP;
    [SerializeField] TextMeshProUGUI classStregthTMP;
    [SerializeField] TextMeshProUGUI classIntelligenceTMP;
    [SerializeField] TextMeshProUGUI classSpeedTMP;

    [SerializeField] TextMeshProUGUI totalHealthTMP;
    [SerializeField] TextMeshProUGUI totalStaminaTMP;
    [SerializeField] TextMeshProUGUI totalMagicTMP;
    [SerializeField] TextMeshProUGUI totalStregthTMP;
    [SerializeField] TextMeshProUGUI totalIntelligenceTMP;
    [SerializeField] TextMeshProUGUI totalSpeedTMP;
    int totalHealthNumber;
    int totalStaminaNumber;
    int totalMagicNumber;
    int totalStrengthNumber;
    int totalIntelligenceNumber;
    int totalSpeedNumber;
    [SerializeField] string emptyNumberString;

    #endregion

    [SerializeField] TextMeshProUGUI titleTextTMP;
    [SerializeField] string creationMainText;
    [SerializeField] string raceSelectText;
    [SerializeField] string classSelectText;
    public bool loadingRacesBool;
    public bool loadingClassesBool;

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
        DisplayCreationMainPanel();
    }

    private void LoadRacesAndClasses()
    {
        foreach (GameObject go in prefabs)
        {
            go.GetComponent<CharacterUnlockItemInfo>().onSelectedRaceOrClass -= Subscriber_OnEventClicked;
        }
        foreach (GameObject go in prefabs)
        {
            Destroy(go);
        }
        prefabs.Clear();

        PlayerInfo playerInfo = LoadGameMasterHandler.Instance.GetPlayerInfo();

        foreach (UnlockObject unlock in playerInfo.unlocks)
        {
            if (loadingRacesBool && unlock.raceStringID != String.Empty)
            {
                GameObject _raceObject = Instantiate(raceClassPrefabObject, transform.position, transform.rotation);
                _raceObject.transform.SetParent(parentRace);
                _raceObject.GetComponent<CharacterUnlockItemInfo>().raceToUnlock = unlock.unlockedRace;
                _raceObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
                prefabs.Add(_raceObject);

                float x = canvas.transform.localScale.x * _raceObject.transform.localScale.x;
                float y = canvas.transform.localScale.y * _raceObject.transform.localScale.y;
                _raceObject.transform.localScale = new Vector3(x,y,1);

            }
            else if (loadingClassesBool && unlock.classStringID != String.Empty)
            {
                GameObject _classObject = Instantiate(raceClassPrefabObject, transform.position, transform.rotation);
                _classObject.transform.SetParent(parentClass);
                _classObject.GetComponent<CharacterUnlockItemInfo>().classToUnlock = unlock.unlockedClass;
                _classObject.GetComponent<CharacterUnlockItemInfo>().DisplayUnlocked();
                prefabs.Add(_classObject);

                float x = canvas.transform.localScale.x * _classObject.transform.localScale.x;
                float y = canvas.transform.localScale.y * _classObject.transform.localScale.y;
                _classObject.transform.localScale = new Vector3(x,y,1);
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

        totalHealthNumber = 0;
        totalStaminaNumber = 0;
        totalMagicNumber = 0;
        totalStrengthNumber = 0;
        totalIntelligenceNumber = 0;
        totalSpeedNumber = 0;

        Race _race = raceDisplayObject.GetComponent<CharacterUnlockItemInfo>().raceToUnlock;
        if(_race != null)
        {
            raceHealthTMP.text = _race.health.ToString();
            raceStaminaTMP.text = _race.stamina.ToString();
            raceMagicTMP.text = _race.magic.ToString();
            raceStregthTMP.text = _race.strength.ToString();
            raceIntelligenceTMP.text = _race.intelligence.ToString();
            raceSpeedTMP.text = _race.speed.ToString();

            totalHealthNumber += _race.health;
            totalStaminaNumber += _race.stamina;
            totalMagicNumber += _race.magic;
            totalStrengthNumber += _race.strength;
            totalIntelligenceNumber += _race.intelligence;
            totalSpeedNumber += _race.speed;

        }
        else
        {
            raceHealthTMP.text = emptyNumberString;
            raceStaminaTMP.text = emptyNumberString;
            raceMagicTMP.text = emptyNumberString;
            raceStregthTMP.text = emptyNumberString;
            raceIntelligenceTMP.text = emptyNumberString;
            raceSpeedTMP.text = emptyNumberString;
        }

        Class _class = classDisplayObject.GetComponent<CharacterUnlockItemInfo>().classToUnlock;
        if (_class != null)
        {
            classHealthTMP.text = _class.health.ToString();
            classStaminaTMP.text = _class.stamina.ToString();
            classMagicTMP.text = _class.magic.ToString();
            classStregthTMP.text = _class.strength.ToString();
            classIntelligenceTMP.text = _class.intelligence.ToString();
            classSpeedTMP.text = _class.speed.ToString();

            totalHealthNumber += _class.health;
            totalStaminaNumber += _class.stamina;
            totalMagicNumber += _class.magic;
            totalStrengthNumber += _class.strength;
            totalIntelligenceNumber += _class.intelligence;
            totalSpeedNumber += _class.speed;
        }
        else
        {
            classHealthTMP.text = emptyNumberString;
            classStaminaTMP.text = emptyNumberString;
            classMagicTMP.text = emptyNumberString;
            classStregthTMP.text = emptyNumberString;
            classIntelligenceTMP.text = emptyNumberString;
            classSpeedTMP.text = emptyNumberString;
        }

        float healthX = 1;
        float staminaX = 1;
        float magicX = 1;
        float strengthX = 1;
        float intelligenceX = 1;
        float speedX = 1;

        foreach (Perk perk in _race.startingPerks)
        {
            totalHealthNumber += perk.bonusHealth;
            totalStaminaNumber += perk.bonusStamina;
            totalMagicNumber += perk.bonusMagic;
            totalStrengthNumber += perk.bonusStrength;
            totalIntelligenceNumber += perk.bonusIntelligence;
            totalSpeedNumber += perk.bonusSpeed;

            healthX += perk.healthMultiplier;
            staminaX += perk.staminaMultiplier;
            magicX += perk.magicMultiplier;
            strengthX += perk.strengthMultiplier;
            intelligenceX += perk.intelligenceMultiplier;
            speedX += perk.speedMultiplier;
        }

        totalHealthTMP.text = (totalHealthNumber * healthX).ToString();
        totalStaminaTMP.text = (totalStaminaNumber * staminaX).ToString();
        totalMagicTMP.text = (totalMagicNumber * magicX).ToString();
        totalStregthTMP.text = (totalStrengthNumber * strengthX).ToString();
        totalIntelligenceTMP.text = (totalIntelligenceNumber * intelligenceX).ToString();
        totalSpeedTMP.text = (totalSpeedNumber * speedX).ToString();

        if (totalHealthNumber == 0)
        {
            totalHealthTMP.text = emptyNumberString;
            totalStaminaTMP.text = emptyNumberString;
            totalMagicTMP.text = emptyNumberString;
            totalStregthTMP.text = emptyNumberString;
            totalIntelligenceTMP.text = emptyNumberString;
            totalSpeedTMP.text = emptyNumberString;
        }

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
            // Debug.LogError("one not selected");
            return;
        }

        if (inputTextName.text == "")
        {
            // Debug.Log("Need a name");
            inputTextName.text = "I'm Silly Billy";
        }

        //Stats Calculated
        int _baseHealth = _class.health + _race.health;
        int _baseStamina = _class.stamina + _race.stamina;
        int _baseMagic = _class.magic + _race.magic;
        int _baseIntelligence = _class.intelligence + _race.intelligence;
        int _baseStrength = _class.strength + _race.strength;
        int _baseSpeed = _class.speed + _race.speed;

        List<PerkObject> _startingPerks = new List<PerkObject>();
        foreach (Perk _perk in _race.startingPerks)
        {
            PerkObject newPerk = new PerkObject(_perk.perkName, _perk, 1, true, default);
            _startingPerks.Add(newPerk);
        }  
        foreach (Perk _perk in _class.startingPerks)
        {
            PerkObject newPerk = new PerkObject(_perk.perkName, _perk, 1, true, default);
            _startingPerks.Add(newPerk);
        }

        float strengthX = 1;
        float intelligenceX = 1;
        float speedX = 1;

        foreach (PerkObject perk in _startingPerks)
        {
            if (perk.perk == null) { continue; }
            strengthX += perk.perk.strengthMultiplier;
            intelligenceX += perk.perk.intelligenceMultiplier;
            speedX += perk.perk.speedMultiplier;
        }

        //Bonus Stats
        int _bonusAttack = Mathf.FloorToInt((_baseStrength * strengthX) / bonusAttackModifer);
        int _bonusDefense = Mathf.FloorToInt((_baseStrength * strengthX) / bonusDefenseModifer);
        int _holdingCapacity = Mathf.FloorToInt((_baseStrength * strengthX) / holdingCapacityModifer);

        int _bonusMagicAttack = Mathf.FloorToInt((_baseIntelligence * intelligenceX) / bonusMagicAttackModifer);
        int _bonusMagicDefense = Mathf.FloorToInt((_baseIntelligence * intelligenceX) / bonusMagicDefenseModifer);
        int _spellbookCapacity = Mathf.FloorToInt((_baseIntelligence * intelligenceX) / spellbookCapacityModifer);

        int _movement = Mathf.FloorToInt((_baseSpeed * speedX) / movementModifer);

        //Character Info
        SaveObject saveObject = new SaveObject
        {
            //Basic Info
            nameOfCharacter = inputTextName.text.ToString(), //does it need to be converted? #TODO
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
            raceAbilityPoints = 1,
            classAbilityPoints = 1,     

            //starting info
            perks = _startingPerks,
            // equipment = new InventoryItem[10]
            

        };

        CharacterRegistry.Instance.AddCharacter(saveObject);

        SaveManagerVersion3.SaveGame(CharacterRegistry.Instance);
    }
}
