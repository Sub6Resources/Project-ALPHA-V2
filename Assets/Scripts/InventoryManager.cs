using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {
    

    public static int NRG = 0;
    public static int Battery = 1;
    public static int CarbonDioxideFilter = 2;
    public static int Plant = 3;
    public static int Water = 4;
    public static int BoosterFood = 5;
    public static int ERK = 6;

    public static string NRG_ITEM = "NRG";
    public static string BATTERY_ITEM = "Battery";
    public static string C02_FILTER_ITEM = "Co2Filter";
    public static string PLANT_ITEM = "Plant";
    public static string WATER_ITEM = "Water";
    public static string FOOD_ITEM = "Food";
    public static string ERK_ITEM = "ERK";

    public InventoryObject Player = new InventoryObject();
    public InventoryObject BetaStorage1 = new InventoryObject();
    public InventoryObject BetaStorage2 = new InventoryObject();
    public InventoryObject BetaStorage3 = new InventoryObject();
    public InventoryObject BetaStorage4 = new InventoryObject();
    //private int ReservedForFutureUse = 5 - 10;
    public InventoryObject AlphaStorage1 = new InventoryObject();


    
    public int NRGBeingHeldBy(InventoryObject location)
    {
        return location.NRGAmount;
    }
    public int BatteriesBeingHeldBy(InventoryObject location)
    {
        return location.BatteriesAmount;
    }
    public int C02FiltersBeingHeldBy(InventoryObject location)
    {
        return location.C02FilterAmount;
    }
    public int PlantsBeingHeldBy(InventoryObject location)
    {
        return location.PlantAmount;
    }
    public int WaterBeingHeldBy(InventoryObject location)
    {
        return location.WaterAmount;
    }
    public int BoosterFoodBeingHeldBy(InventoryObject location)
    {
        return location.BoosterFoodAmount;
    }
    /// <summary>
    /// THIS METHOD IS DEPRECATED. JUST LOOK DIRECTLY IN THE INVENTORY OBJECT
    /// </summary>
    /// <param name="location">InventoryObject</param>
    /// <returns>null int[]</returns>
    public static int[] AllStuffBeingHeldBy(InventoryObject location)
    {
        return new int[] { };
    }
    /// <summary>
    /// Gives the location amount number of type objects.
    /// </summary>
    /// <param name="location">What to give the items to</param>
    /// <param name="amount">Number of items to give</param>
    /// <param name="type">Type of object to give to player</param>
    public void Give(InventoryObject location, int amount, int type)
    {
        if(type==NRG)
        {
            location.NRGAmount += amount;
        } else if(type == Battery)
        {
            location.BatteriesAmount += amount;
        } else if(type == CarbonDioxideFilter)
        {
            location.C02FilterAmount += amount;
        } else if(type == Plant)
        {
            location.PlantAmount += amount;
        } else if(type == Water)
        {
            location.WaterAmount += amount;
        } else if(type == BoosterFood)
        {
            location.BoosterFoodAmount += amount;
        } else
        {
            return;
        }
    }

    /// <summary>
    /// Sets an inventory location with the given amounts.
    /// </summary>
    /// <param name="location">InventoryObject to set</param>
    /// <param name="NRGAmount">Amount of NRG to set</param>
    /// <param name="BatteryAmount">Amount of Batteries to set</param>
    /// <param name="C02FilterAmount">Amount of C02 Filters to give</param>
    /// <param name="PlantAmount">Amount of Plants to give</param>
    /// <param name="WaterAmount">Amount of Water to give</param>
    /// <param name="FoodAmount">Amount of Food to Give</param>
    /// <param name="ERKAmount">Amount of ERKs to Give</param>
    public void Set(InventoryObject location, int NRGAmount, int BatteryAmount, int C02FilterAmount, int PlantAmount, int WaterAmount, int FoodAmount, int ERKAmount)
    {
        location.NRGAmount = NRGAmount;
        location.BatteriesAmount = BatteryAmount;
        location.C02FilterAmount = C02FilterAmount;
        location.PlantAmount = PlantAmount;
        location.WaterAmount = WaterAmount;
        location.BoosterFoodAmount = FoodAmount;
        location.ERKAmount = ERKAmount;
    }
    /// <summary>
    /// Sets all values in location to given amount
    /// </summary>
    /// <param name="location">InventoryObject to Set</param>
    /// <param name="amount">Amount to set all values to</param>
    public void Set(InventoryObject location, int amount)
    {
        location.NRGAmount = amount;
        location.BatteriesAmount = amount;
        location.C02FilterAmount = amount;
        location.PlantAmount = amount;
        location.WaterAmount = amount;
        location.BoosterFoodAmount = amount;
        location.ERKAmount = amount;
    }
    /// <summary>
    /// Sets the Inventory location to the newValue inventory object.
    /// </summary>
    /// <param name="location">InventoryObject to set</param>
    /// <param name="newValues">New Inventory Object to set location to</param>
    public void Set(InventoryObject location, InventoryObject newValues)
    {
        location = newValues;
    }
    /// <summary>
    /// Takes amount number of type objects from the location.
    /// </summary>
    /// <param name="location">What to take the items from</param>
    /// <param name="amount">Number of items to take</param>
    /// <param name="type">Type of object to take from player</param>
    public void Take(InventoryObject location, int amount, int type)
    {
        if (type == NRG)
        {
            location.NRGAmount -= amount;
        }
        else if (type == Battery)
        {
            location.BatteriesAmount -= amount;
        }
        else if (type == CarbonDioxideFilter)
        {
            location.C02FilterAmount -= amount;
        }
        else if (type == Plant)
        {
            location.PlantAmount -= amount;
        }
        else if (type == Water)
        {
            location.WaterAmount -= amount;
        }
        else if (type == BoosterFood)
        {
            location.BoosterFoodAmount -= amount;
        }
        else
        {
            return;
        }
    }
    public InventoryObject GetObjectFromName(string name)
    {
        switch(name)
        {
            case "Player":
                return Player;
            case "BetaStorage1":
                return BetaStorage1;
            case "BetaStorage2":
                return BetaStorage2;
            case "BetaStorage3":
                return BetaStorage3;
            case "BetaStorage4":
                return BetaStorage4;
            case "AlphaStorage1":
                return AlphaStorage1;
            default:
                return null;
        }
    }
    
}
/// <summary>
/// Object for Project AL1681 to represent the items in an objects inventory.
/// </summary>
public class InventoryObject
{
    public int NRGAmount;
    public int BatteriesAmount;
    public int C02FilterAmount;
    public int PlantAmount;
    public int WaterAmount;
    public int BoosterFoodAmount;
    public int ERKAmount;

    /// <summary>
    /// Sets the Inventory location to the newValue inventory object.
    /// </summary>
    /// <param name="location">InventoryObject to set</param>
    /// <param name="newValues">New Inventory Object to set location to</param>
    public void Set(InventoryObject newValues)
    {
        this.NRGAmount = newValues.NRGAmount;
        this.BatteriesAmount = newValues.BatteriesAmount;
        this.C02FilterAmount = newValues.C02FilterAmount;
        this.PlantAmount = newValues.PlantAmount;
        this.WaterAmount = newValues.WaterAmount;
        this.BoosterFoodAmount = newValues.BoosterFoodAmount;
        this.ERKAmount = newValues.ERKAmount;
    }
}


