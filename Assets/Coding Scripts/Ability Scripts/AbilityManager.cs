using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    SaveState saveState;
    SaveObject save;
    [SerializeField] TextMeshProUGUI spellbookCapacityTextTMP;
    [SerializeField] GameObject abilityObjectPrefab;
    [SerializeField] GameObject perkPrefab;

    [SerializeField] GameObject abilityPanelObject;
    List<GameObject> abilityCardPrefabs = new List<GameObject>();
    List<GameObject> perkPrefabList = new List<GameObject>();

    [SerializeField] GameObject raceAbilityTab;
    [SerializeField] GameObject classAbilityTab;
    [SerializeField] GameObject perkTab;
    [SerializeField] GameObject learnedAbilityTab;


    [SerializeField] Transform raceAbilityContent;
    [SerializeField] Transform classAbilityContent;
    [SerializeField] Transform perkContent;
    [SerializeField] Transform learnedAbilityContent;



    private void Start() 
    {
        save = NewSaveSystem.FindCurrentSave();
        saveState = NewSaveSystem.FindSaveState();

        raceAbilityTab.SetActive(true);
        classAbilityTab.SetActive(false);
        perkTab.SetActive(false);
        learnedAbilityTab.SetActive(false);
        abilityPanelObject.SetActive(false);

        saveState.screenState = ScreenState.CharacterInfo;
        
        saveState.raceAbilityBool = true;

        NewSaveSystem.SaveStateOfGame(saveState);
        saveState = NewSaveSystem.FindSaveState();

        //UpdateSpellBook();

    }

    //Old Method
    /*
    public void UpdateSpellBook()
    {
        spellbookCapacityTextTMP.text = save.abilityInventory.Count + "/" + save.spellbookCapacity;
    }
    */

    public void RaceAbilityTreeOption()
    {
        saveState.learnedAbilityBool = false;
        saveState.classAbilityBool = false;
        saveState.raceAbilityBool = true;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void ClassAbilityTreeOption()
    {
        saveState.learnedAbilityBool = false;
        saveState.raceAbilityBool = false;
        saveState.classAbilityBool = true;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void LearnedAbilityOption()
    {
        saveState.learnedAbilityBool = true;
        saveState.raceAbilityBool = false;
        saveState.classAbilityBool = false;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void LoadAbilities()
    {
        save = NewSaveSystem.FindCurrentSave();

        foreach (GameObject card in abilityCardPrefabs)
        {
            Destroy(card);
        }
        abilityCardPrefabs.Clear();

        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            switch (abilitySO.abilityType)
            {
                case AbilityType.raceAbility:
                    if (saveState.raceAbilityBool)
                    {
                        GameObject abilityInstance = Instantiate(abilityObjectPrefab, transform.position, transform.rotation);
                        abilityInstance.transform.SetParent(raceAbilityContent, false);
                        abilityInstance.GetComponent<AbilityCardInstance>().DisplayInfo(abilitySO);
                        abilityCardPrefabs.Add(abilityInstance);
                    }
                    break;
                case AbilityType.classAblity:
                    if (saveState.classAbilityBool)
                    {
                        GameObject abilityInstance = Instantiate(abilityObjectPrefab, transform.position, transform.rotation);
                        abilityInstance.transform.SetParent(classAbilityContent, false);
                        abilityInstance.GetComponent<AbilityCardInstance>().DisplayInfo(abilitySO);
                        abilityCardPrefabs.Add(abilityInstance);
                    }
                    break;
                case AbilityType.learnedAbility:
                    if (saveState.learnedAbilityBool)
                    {
                        GameObject abilityInstance = Instantiate(abilityObjectPrefab, transform.position, transform.rotation);
                        abilityInstance.transform.SetParent(learnedAbilityContent, false);
                        abilityInstance.GetComponent<AbilityCardInstance>().DisplayInfo(abilitySO);
                        abilityCardPrefabs.Add(abilityInstance);
                    }
                    break;
            }
        }

    }

    public void LoadAbilityPanel(AbilitySaveObject _abilitySO)
    {
        abilityPanelObject.SetActive(true);
        abilityPanelObject.GetComponent<AbilityPanelManager>().DisplayAbility(_abilitySO);
    }

    public void LoadPerks()
    {
        foreach (GameObject perkObject in perkPrefabList)
        {
            Destroy(perkObject);
        }
        perkPrefabList.Clear();

        foreach (PerkObject perk in save.perks)
        {
            foreach (GameObject GO in perkPrefabList)
            {
                if (GO.GetComponent<PerkInstanceObject>().perk == perk.perk)
                {
                    int displayNumber = GO.GetComponent<PerkInstanceObject>().perkObject.count + perk.count;
                    GO.GetComponent<PerkInstanceObject>().perkObject.count = displayNumber;
                    GO.GetComponent<PerkInstanceObject>().perkCountTMP.text = displayNumber.ToString();
                    return;
                }
                
            }

            GameObject _perk = Instantiate(perkPrefab, transform.position, transform.rotation);
            _perk.transform.SetParent(perkContent, false);
            _perk.GetComponent<PerkInstanceObject>().DisplayPerk(perk);
            perkPrefabList.Add(_perk);
            
        }
    }
}
