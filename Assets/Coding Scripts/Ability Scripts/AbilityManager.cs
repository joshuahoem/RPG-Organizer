using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] AbilityDatabase abilityDatabase;
    [SerializeField] ShopContentFitter contentFitter;
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
        SaveObject save = NewSaveSystem.FindCurrentSave();
        SaveState saveState = NewSaveSystem.FindSaveState();

        raceAbilityTab.SetActive(true);
        classAbilityTab.SetActive(false);
        perkTab.SetActive(false);
        learnedAbilityTab.SetActive(false);
        abilityPanelObject.SetActive(false);

        contentFitter.GOParent = raceAbilityContent.gameObject;

        saveState.screenState = ScreenState.CharacterInfo;

        saveState.raceAbilityBool = true;

        NewSaveSystem.SaveStateOfGame(saveState);

        //UpdateSpellBook();

        UpdateAbilities();

    }

    private void UpdateAbilities()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();
        foreach (AbilitySaveObject _ability in save.abilityInventory)
        {
            if (_ability.ability == null)
            {
                for (int i = 0; i < abilityDatabase.allAbilities.Length; i++)
                {
                    if (_ability.stringID == abilityDatabase.allAbilities[i].abilityName)
                    {
                        _ability.ability = abilityDatabase.allAbilities[i];
                        Debug.Log("replaced");
                    }
                }
            }
        }

        NewSaveSystem.SaveChanges(save);
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
        SaveState saveState = NewSaveSystem.FindSaveState();
        saveState.learnedAbilityBool = false;
        saveState.classAbilityBool = false;
        saveState.raceAbilityBool = true;
        contentFitter.GOParent = raceAbilityContent.gameObject;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void ClassAbilityTreeOption()
    {
        SaveState saveState = NewSaveSystem.FindSaveState();
        saveState.learnedAbilityBool = false;
        saveState.raceAbilityBool = false;
        saveState.classAbilityBool = true;
        contentFitter.GOParent = classAbilityContent.gameObject;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void LearnedAbilityOption()
    {
        SaveState saveState = NewSaveSystem.FindSaveState();
        saveState.learnedAbilityBool = true;
        saveState.raceAbilityBool = false;
        saveState.classAbilityBool = false;
        contentFitter.GOParent = learnedAbilityContent.gameObject;
        NewSaveSystem.SaveStateOfGame(saveState);
    }

    public void LoadAbilities()
    {
        SaveState saveState = NewSaveSystem.FindSaveState();
        SaveObject save = NewSaveSystem.FindCurrentSave();

        foreach (GameObject card in abilityCardPrefabs)
        {
            Destroy(card);
        }
        abilityCardPrefabs.Clear();

        foreach (AbilitySaveObject abilitySO in save.abilityInventory)
        {
            if (abilitySO.ability == null)
            {
                abilitySO.ability = abilityDatabase.GetStringID[abilitySO.stringID];
            }
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

        contentFitter.FitContent(abilityCardPrefabs.Count);

    }

    public void LoadAbilityPanel(AbilitySaveObject _abilitySO)
    {
        abilityPanelObject.SetActive(true);
        abilityPanelObject.GetComponent<AbilityPanelManager>().DisplayAbility(_abilitySO, true);
    }

    public void LoadPerks()
    {
        SaveObject save = NewSaveSystem.FindCurrentSave();

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

        contentFitter.GOParent = perkContent.gameObject;
        contentFitter.FitContent(perkPrefabList.Count);


    }
}
