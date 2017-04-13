using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class InventoryChanger : MonoBehaviour, IHasChanged
{
    public Transform playerslots;
    public Transform BetaStorageOneSlots;
    public Transform BetaStorageTwoSlots;
    private InventoryManager inventoryManager;
    //public InventoryObject container;
    private string containerName;
    public InventoryObject containerOpen;
    public GameObject playerInventory;
    public GameObject BetaStorage1;
    public GameObject BetaStorage2;
    private GameObject[] inventories;
    private CameraSwitcher cameraSwitcher;

    public GameObject FoodPrefab;
    public GameObject ERKPrefab;
    public GameObject NRGPrefab;
    public GameObject BatteryPrefab;
    public GameObject WaterPrefab;

    public Transform playerSlot1;
    public Transform playerSlot2;
    public Transform playerSlot3;
    public Transform playerSlot4;
    public Transform playerSlot5;
    public Transform playerSlot6;
    public Transform playerSlot7;
    public Transform playerSlot8;

    public Transform Beta1Slot1;
    public Transform Beta1Slot2;
    public Transform Beta1Slot3;
    public Transform Beta1Slot4;
    public Transform Beta1Slot5;

    public Transform Beta2Slot1;
    public Transform Beta2Slot2;
    public Transform Beta2Slot3;
    public Transform Beta2Slot4;
    public Transform Beta2Slot5;

    public static int PLAYER_INVENTORY = 0;
    public static int BETASTORAGE_ONE_INVENTORY = 1;
    public static int BETASTORAGE_TWO_INVENTORY = 2;

    public string[] playerInv = new string[] { "Moo", "Moo", "Moo", "Moo", "Moo", "Moo", "Moo", "Moo" };
    public string[] betaStorage1Inv = new string[] { "Moo", "Moo", "Moo", "Moo", "Moo" };
    public string[] betaStorage2Inv = new string[] { "Moo", "Moo", "Moo", "Moo", "Moo" };

    void Start()
    {
        
        
        
        inventoryManager = FindObjectOfType<InventoryManager>();
        inventories = new GameObject[3] { playerInventory, BetaStorage1, BetaStorage2 };
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
        HasChanged();

    }
    public void OpenInventory(string containerName)
    {
        this.containerName = containerName;
        CloseInventory();
        cameraSwitcher.unlockCursor();
        switch(containerName)
        {
            case "player":
                playerInventory.SetActive(true);
                break;
            case "BetaStorage1":
                BetaStorage1.SetActive(true);
                playerInventory.SetActive(true);
                break;
            case "BetaStorage2":
                BetaStorage2.SetActive(true);
                playerInventory.SetActive(true);
                break;
            default:
                Debug.Log("INVENTORY DOES NOT EXIST");
                break;
        }
    }

    public void CloseInventory()
    {
        cameraSwitcher.lockCursor();
        foreach (GameObject invObj in inventories)
        {
            if (invObj)
            {
                invObj.SetActive(false);
            }
        }
    }

    public bool isOpen()
    {
        foreach (GameObject invObj in inventories)
        {
            if(invObj.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    public bool hasRoom(int location)
    {
        switch(location)
        {
            case 0: //PLAYER_INVENTORY
                foreach(string item in playerInv)
                {
                    if(item == "" || item == null)
                    {
                        return true;
                    }
                }
                break;
            case 1: //BETASTORAGE_ONE_INVENTORY
                foreach (string item in betaStorage1Inv)
                {
                    if (item == "" || item == null)
                    {
                        return true;
                    }
                }
                break;
            case 2: //BETASTORAGE_TWO_INVENTORY
                foreach (string item in betaStorage2Inv)
                {
                    if (item == "" || item == null)
                    {
                        return true;
                    }
                }
                break;
            default:
                return false;
        }
        return false;
    }

    public int whereIsRoom(int location)
    {
        switch (location)
        {
            case 0: //PLAYER_INVENTORY
                for(int i = 0; i<playerInv.Length; i++)
                {
                    if (playerInv[i] == "" || playerInv[i] == null)
                    {
                        return i;
                    }
                }
                break;
            case 1: //BETASTORAGE_ONE_INVENTORY
                for (int i = 0; i < betaStorage1Inv.Length; i++)
                {
                    if (betaStorage1Inv[i] == "" || betaStorage1Inv[i] == null)
                    {
                        return i;
                    }
                }
                break;
            case 2: //BETASTORAGE_TWO_INVENTORY
                for (int i = 0; i < betaStorage2Inv.Length; i++)
                {
                    if (betaStorage2Inv[i] == "" || betaStorage2Inv[i] == null)
                    {
                        return i;
                    }
                }
                break;
            default:
                Debug.LogError("THERE IS NO EMPTY LOCATION, PLEASE CHECK FIRST NEXT TIME");
                return 1000;
        }
        Debug.LogError("THERE IS NO EMPTY LOCATION, PLEASE CHECK FIRST NEXT TIME");
        return 1000;
    }

    /// <summary>
    /// Give a location slot an item. The most direct of the Give() methods
    /// </summary>
    /// <param name="location">Location int (Use static integers)</param>
    /// <param name="slot">The slot number (we recommend you use whereIsRoom()</param>
    /// <param name="item">Item to use (use static strings)</param>
    /// <returns>Whether giving the item was successful or not (as a bool)</returns>
    public bool Give(int location, int slot, string item)
    {
        if (slot != 1000 && hasRoom(location))
        {
            switch (location)
            {
                case 0:
                    playerInv[slot] = item;
                    setGameObjectToInvObject();
                    return true;
                case 1:
                    betaStorage1Inv[slot] = item;
                    setGameObjectToInvObject();
                    return true;
                case 2:
                    betaStorage2Inv[slot] = item;
                    setGameObjectToInvObject();
                    return true;
            }
        } else
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// Gives location object an item
    /// </summary>
    /// <param name="location">Where to give the item</param>
    /// <param name="item">What to give</param>
    /// <returns>Whether or not the Give was successful</returns>
    public bool Give(int location, string item)
    {
        if(hasRoom(location))
        {
            return Give(location, whereIsRoom(location), item);
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Gives player the specified item
    /// </summary>
    /// <param name="item">Item to give</param>
    /// <returns>Whether or not the Give was succesful</returns>
    public bool Give(string item)
    {
        return Give(PLAYER_INVENTORY, item);
    }

    /// <summary>
    /// Gives player the specified item (Equivelant to Give(item))
    /// </summary>
    /// <param name="item">Item to give</param>
    /// <returns>Whether or not the Give was succesful</returns>
    public bool GivePlayer(string item)
    {
        return Give(item);
    }

    public void HasChanged()
    {
        Debug.Log("HasChanged called with the Array at "+playerInv.Length+" long");
        InventoryObject newPlayerInventory = new InventoryObject();
        InventoryObject newBetaStorageOne = new InventoryObject();
        InventoryObject newBetaStorageTwo = new InventoryObject();
        int iteration = 0;
        foreach (RectTransform slotTransform in playerslots)
        {

            if(slotTransform.GetComponent<Text>())
            {
                break;
            }
            SlotControl x = slotTransform.GetComponent<SlotControl>();
            GameObject item = x.item;
            if (item)
            {
                for(int a = 0; a<playerInv.Length; a++)
                {
                    Debug.Log("THROW playerInv["+a+"]: "+playerInv[a]);
                }
                switch (slotTransform.name)
                {
                    case "PlayerSlot1":
                        Debug.Log("THE ARRAY: " + playerInv.ToString() + ". THE LENGTH: " + playerInv.Length);
                        playerInv[0] = item.name;
                        break;
                    case "PlayerSlot2":
                        playerInv[1] = item.name;
                        break;
                    case "PlayerSlot3":
                        playerInv[2] = item.name;
                        break;
                    case "PlayerSlot4":
                        playerInv[3] = item.name;
                        break;
                    case "PlayerSlot5":
                        playerInv[4] = item.name;
                        break;
                    case "PlayerSlot6":
                        playerInv[5] = item.name;
                        break;
                    case "PlayerSlot7":
                        playerInv[6] = item.name;
                        break;
                    case "PlayerSlot8":
                        playerInv[7] = item.name;
                        break;

                }
                if (item.name == "NRG")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.NRG);
                    print("given NRG to Player from " + containerName);
                    newPlayerInventory.NRGAmount += 1;
                }
                else if (item.name == "Battery")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Battery);
                    print("given Battery to Player from" + containerName);
                    newPlayerInventory.BatteriesAmount += 1;
                }
                else if (item.name == "Water")
                {
                    // inventoryManager.Give(container, 1, InventoryManager.Water);
                    print("given Water to Player from" + containerName);
                    newPlayerInventory.WaterAmount += 1;
                }
                else if (item.name == "Plant")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Plant);
                    print("given Plant to Player from" + containerName);
                    newPlayerInventory.PlantAmount += 1;
                }
                else if (item.name == "Co2 Filter")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.CarbonDioxideFilter);
                    print("given Co2 filter to Player from" + containerName);
                    newPlayerInventory.C02FilterAmount += 1;
                }
                else if (item.name == "Food")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.BoosterFood);
                    print("given BoosterFood to Player from" + containerName);
                    newPlayerInventory.BoosterFoodAmount += 1;
                }
                else if(item.name=="ERK")
                {
                    print("given ERK to Player from" + containerName);
                    newPlayerInventory.ERKAmount += 1;
                }
                else
                {

                }
                
            } else
            {
                //TODO this
                playerInv[iteration] = "";
            }
            iteration++;
        }
        inventoryManager.Player.Set(newPlayerInventory);
        int i = 0;
        foreach (RectTransform slotTransform in BetaStorageOneSlots)
        {
            
            GameObject item = slotTransform.GetComponent<SlotControl>().item;
            if (item)
            {
                switch (slotTransform.name)
                {
                    case "Beta1Slot1":
                        betaStorage1Inv[0] = item.name;
                        break;
                    case "Beta1Slot2":
                        betaStorage1Inv[1] = item.name;
                        break;
                    case "Beta1Slot3":
                        betaStorage1Inv[2] = item.name;
                        break;
                    case "Beta1Slot4":
                        betaStorage1Inv[3] = item.name;
                        break;
                    case "Beta1Slot5":
                        betaStorage1Inv[4] = item.name;
                        break;
                }
                if (item.name == "NRG")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.NRG);
                    print("given NRG to BetaStorageOne in slot "+i);
                    newBetaStorageOne.NRGAmount += 1;
                }
                else if (item.name == "Battery")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Battery);
                    print("given Battery to BetaStorageOne in slot "+i);
                    newBetaStorageOne.BatteriesAmount += 1;
                }
                else if (item.name == "Water")
                {
                    // inventoryManager.Give(container, 1, InventoryManager.Water);
                    print("given Water to BetaStorageOne in slot "+i);
                    newBetaStorageOne.WaterAmount += 1;
                }
                else if (item.name == "Plant")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Plant);
                    print("given Plant to BetaStorageOne in slot "+i);
                    newBetaStorageOne.PlantAmount += 1;
                }
                else if (item.name == "Co2 Filter")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.CarbonDioxideFilter);
                    print("given Co2 filter to BetaStorageOne in slot "+i);
                    newBetaStorageOne.C02FilterAmount += 1;
                }
                else if (item.name == "Food")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.BoosterFood);
                    print("given BoosterFood to BetaStorageOne in slot"+i);
                    newBetaStorageOne.BoosterFoodAmount += 1;
                }
                else if(item.name == "ERK")
                {
                    print("given ERK to BetaStorageOne in slot "+i);
                    newBetaStorageOne.ERKAmount += 1;
                }
                else
                {
                    print("Given Strange Item called "+item.name+" to BetaStorageOne in slot "+i);
                }
            } else
            {
                //TODO this
                betaStorage1Inv[i] = "";
            }
            i++;
        }
        
        inventoryManager.BetaStorage1.Set(newBetaStorageOne);
        i = 0;
        foreach (RectTransform slotTransform in BetaStorageTwoSlots)
        {
           
            GameObject item = slotTransform.GetComponent<SlotControl>().item;
            if (item)
            {
                switch (slotTransform.name)
                {
                    case "Beta2Slot1":
                        betaStorage2Inv[0] = item.name;
                        break;
                    case "Beta2Slot2":
                        betaStorage2Inv[1] = item.name;
                        break;
                    case "Beta2Slot3":
                        betaStorage2Inv[2] = item.name;
                        break;
                    case "Beta2Slot4":
                        betaStorage2Inv[3] = item.name;
                        break;
                    case "Beta2Slot5":
                        betaStorage2Inv[4] = item.name;
                        break;
                }
                    if (item.name == "NRG")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.NRG);
                    print("given NRG to BetaStorageTwo");
                    newBetaStorageTwo.NRGAmount += 1;
                }
                else if (item.name == "Battery")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Battery);
                    print("given Battery to BetaStorageTwo");
                    newBetaStorageTwo.BatteriesAmount += 1;
                }
                else if (item.name == "Water")
                {
                    // inventoryManager.Give(container, 1, InventoryManager.Water);
                    print("given Water to BetaStorageTwo");
                    newBetaStorageTwo.WaterAmount += 1;
                }
                else if (item.name == "Plant")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.Plant);
                    print("given Plant to BetaStorageTwo");
                    newBetaStorageTwo.PlantAmount += 1;
                }
                else if (item.name == "Co2 Filter")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.CarbonDioxideFilter);
                    print("given Co2 filter to BetaStorageTwo");
                    newBetaStorageTwo.C02FilterAmount += 1;
                }
                else if (item.name == "Food")
                {
                    //inventoryManager.Give(container, 1, InventoryManager.BoosterFood);
                    print("given BoosterFood to BetaStorageTwo");
                    newBetaStorageTwo.BoosterFoodAmount += 1;
                }
                else if(item.name == "ERK")
                {
                    print("given ERK to BetaStorageTwo");
                    newBetaStorageTwo.ERKAmount += 1;
                }
                else
                {
                    print("Given Strange Item called "+item.name+" to BetaStorageTwo");
                }
            }
            else
            {
                //TODO this
                betaStorage2Inv[i] = "";
            }
            i++;
        }
        inventoryManager.BetaStorage2.Set(newBetaStorageTwo);
        
    }
    public string[][] stuffINeedSaved()
    {
        if(betaStorage1Inv.Length == 0)
        {
            betaStorage1Inv[0] = "";
            betaStorage1Inv[1] = "";
            betaStorage1Inv[2] = "";
            betaStorage1Inv[3] = "";
            betaStorage1Inv[4] = "";
        }
        if (betaStorage2Inv.Length == 0)
        {
            betaStorage2Inv[0] = "";
            betaStorage2Inv[1] = "";
            betaStorage2Inv[2] = "";
            betaStorage2Inv[3] = "";
            betaStorage2Inv[4] = "";
        }
        return new string[3][] { playerInv, betaStorage1Inv, betaStorage2Inv };
    }

    public void Load(string[][] loadString)
    {
        playerInv = loadString[0];
        Debug.Log("LoadString[0].Length = " + loadString[0].Length);
        foreach(string a in loadString[0])
        {
            Debug.Log("Load PLAYERINVENTORY : " + a);
        }
        betaStorage1Inv = loadString[1];
        Debug.Log("LoadString[1].Length = " + loadString[0].Length);
        foreach (string a in loadString[1])
        {
            Debug.Log("Load BETASTORAGE1INV : " + a);
        }
        betaStorage2Inv = loadString[2];
        Debug.Log("LoadString[2].Length = " + loadString[0].Length);
        foreach (string a in loadString[2])
        {
            Debug.Log("Load BETASTORAGE2INV : " + a);
        }
        setGameObjectToInvObject();
    }

    public void setGameObjectToInvObject()
    {
        foreach(Transform t in playerslots)
        {
            foreach(Transform child in t)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform t in BetaStorageOneSlots)
        {
            foreach (Transform child in t)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform t in BetaStorageTwoSlots)
        {
            foreach (Transform child in t)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i=0; i<playerInv.Length; i++)
        {
            GameObject tempObject = null;
            switch (playerInv[i])
            {
                case "NRG":
                    tempObject = Instantiate(NRGPrefab);
                    tempObject.name = NRGPrefab.name;
                    break;
                case "Battery":
                    tempObject = Instantiate(BatteryPrefab);
                    tempObject.name = BatteryPrefab.name;
                    break;
                case "Plant":
                    //TODO tempObject = Instantiate(PlantPrefab);
                    //TODO tempObject.name = PlantPrefab.name;
                    break;
                case "Water":
                    tempObject = Instantiate(WaterPrefab);
                    tempObject.name = WaterPrefab.name;
                    break;
                case "Co2 Filter":
                    //TODO tempObject = Instantiate(Co2FilterPrefab);
                    //TODO tempObject.name = Co2FilterPrefab.name;
                    break;
                case "Food":
                    tempObject = Instantiate(FoodPrefab);
                    tempObject.name = FoodPrefab.name;
                    break;
                case "ERK":
                    tempObject = Instantiate(ERKPrefab);
                    tempObject.name = ERKPrefab.name;
                    break;
                case "Moo":
                    //TODO fix this
                    Debug.Log("MOO!!!!!");
                    break;
                default:
                    tempObject = null;
                    break;
            }
            if(tempObject != null)
            {
                int number = i + 1;
                Debug.Log("Setting " + tempObject + " to correct parent (PlayerSlot" + number + ")");
                switch(number)
                {
                    case 1:
                        tempObject.transform.parent = playerSlot1;
                        break;
                    case 2:
                        tempObject.transform.parent = playerSlot2;
                        break;
                    case 3:
                        tempObject.transform.parent = playerSlot3;
                        break;
                    case 4:
                        tempObject.transform.parent = playerSlot4;
                        break;
                    case 5:
                        tempObject.transform.parent = playerSlot5;
                        break;
                    case 6:
                        tempObject.transform.parent = playerSlot6;
                        break;
                    case 7:
                        tempObject.transform.parent = playerSlot7;
                        break;
                    case 8:
                        tempObject.transform.parent = playerSlot8;
                        break;
                    default:
                        Debug.LogError("That slot (" + number + ") does not exit!");
                        break;
                }
                
            }
            else
            {
                Debug.Log("tempObject at " + i + " was null");
            }
        }
        for(int i=0; i<betaStorage1Inv.Length;i++)
        {
            GameObject tempObject = null;
            switch (betaStorage1Inv[i])
            {
                case "NRG":
                    tempObject = Instantiate(NRGPrefab);
                    tempObject.name = NRGPrefab.name;
                    break;
                case "Battery":
                    tempObject = Instantiate(BatteryPrefab);
                    tempObject.name = BatteryPrefab.name;
                    break;
                case "Plant":
                    //TODO tempObject = Instantiate(PlantPrefab);
                    //TODO tempObject.name = PlantPrefab.name;
                    break;
                case "Water":
                    tempObject = Instantiate(WaterPrefab);
                    tempObject.name = WaterPrefab.name;
                    break;
                case "Co2 Filter":
                    //TODO tempObject = Instantiate(Co2FilterPrefab);
                    //TODO tempObject.name = Co2FilterPrefab.name;
                    break;
                case "Food":
                    tempObject = Instantiate(FoodPrefab);
                    tempObject.name = FoodPrefab.name;
                    break;
                case "ERK":
                    tempObject = Instantiate(ERKPrefab);
                    tempObject.name = ERKPrefab.name;
                    break;
                case "Moo":
                    //TODO fix this
                    Debug.Log("MOO!!!!!");
                    break;
                default:
                    tempObject = null;
                    break;
            }
            if (tempObject != null)
            {
                int number = i + 1;
                Debug.Log("Setting " + tempObject + " to correct parent (Beta1Slot" + number + ")");
                switch (number)
                {
                    case 1:
                        tempObject.transform.parent = Beta1Slot1;
                        break;
                    case 2:
                        tempObject.transform.parent = Beta1Slot2;
                        break;
                    case 3:
                        tempObject.transform.parent = Beta1Slot3;
                        break;
                    case 4:
                        tempObject.transform.parent = Beta1Slot4;
                        break;
                    case 5:
                        tempObject.transform.parent = Beta1Slot5;
                        break;
                    default:
                        Debug.LogError("That slot (" + number + ") does not exit!");
                        break;
                }
            }
            else
            {
                Debug.Log("tempObject at " + i + " was null");
            }
        }
        for (int i = 0; i < betaStorage2Inv.Length; i++)
        {
            GameObject tempObject = null;
            switch (betaStorage2Inv[i])
            {
                case "NRG":
                    tempObject = Instantiate(NRGPrefab);
                    tempObject.name = NRGPrefab.name;
                    break;
                case "Battery":
                    tempObject = Instantiate(BatteryPrefab);
                    tempObject.name = BatteryPrefab.name;
                    break;
                case "Plant":
                    //TODO tempObject = Instantiate(PlantPrefab);
                    //TODO tempObject.name = PlantPrefab.name;
                    break;
                case "Water":
                    tempObject = Instantiate(WaterPrefab);
                    tempObject.name = WaterPrefab.name;
                    break;
                case "Co2 Filter":
                    //TODO tempObject = Instantiate(Co2FilterPrefab);
                    //TODO tempObject.name = Co2FilterPrefab.name;
                    break;
                case "Food":
                    tempObject = Instantiate(FoodPrefab);
                    tempObject.name = FoodPrefab.name;
                    break;
                case "ERK":
                    tempObject = Instantiate(ERKPrefab);
                    tempObject.name = ERKPrefab.name;
                    break;
                case "Moo":
                    //TODO fix this
                    Debug.Log("MOO!!!!!");
                    break;
                default:
                    tempObject = null;
                    break;
            }
            if (tempObject != null)
            {
                int number = i + 1;
                Debug.Log("Setting " + tempObject + " to correct parent (Beta2Slot" + number + ")");
                switch (number)
                {
                    case 1:
                        tempObject.transform.parent = Beta2Slot1;
                        break;
                    case 2:
                        tempObject.transform.parent = Beta2Slot2;
                        break;
                    case 3:
                        tempObject.transform.parent = Beta2Slot3;
                        break;
                    case 4:
                        tempObject.transform.parent = Beta2Slot4;
                        break;
                    case 5:
                        tempObject.transform.parent = Beta2Slot5;
                        break;
                    default:
                        Debug.LogError("That slot (" + number + ") does not exit!");
                        break;
                }
            } else
            {
                Debug.Log("tempObject at " + i + " was null");
            }
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
