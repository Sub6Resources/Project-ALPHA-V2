using UnityEngine;
using System.Collections;

public class HubHoverControl : MonoBehaviour {

    public string whatRoomIsThis;
    public float delay;

    private HubControl hubControl;
    private PowerManager powerManager;

    void Start()
    {
        hubControl = FindObjectOfType<HubControl>();
        powerManager = FindObjectOfType<PowerManager>();
    }
    void OnMouseOver()
    {
        Invoke(whatRoomIsThis, delay);
        //Debug.Log("Active Object Hovered Over");
        if (Input.GetMouseButtonDown(0))
        {
            if (!hubControl.powerMode)
            {
                Debug.Log("Click On Active Object");
                Invoke(whatRoomIsThis + "Click", delay);
            } else
            {
                Debug.Log("Click on power mode object");
                Invoke(whatRoomIsThis + "Power", delay);
            }
        }
    }

    void OnMouseExit()
    {
        //todo this.
    }

    void roomOne()
    {
        Debug.Log("This is Room One!!!!!");
    }

    void roomTwo()
    {
        Debug.Log("This is Room Two!!!!!");
    }
    void roomThree()
    {

    }
    void roomFour()
    {

    }
    void roomFive()
    {

    }
    void roomSix()
    {

    }
    void roomSeven()
    {

    }
    void roomEight()
    {

    }
    void roomNine()
    {

    }
    void roomTen()
    {

    }
    void roomEleven()
    {

    }
    //CLICKS
    void roomOneClick()
    {

    }

    void roomTwoClick()
    {

    }
    void roomThreeClick()
    {

    }
    void roomFourClick()
    {

    }
    void roomFiveClick()
    {

    }
    void roomSixClick()
    {

    }
    void roomSevenClick()
    {

    }
    void roomEightClick()
    {

    }
    void roomNineClick()
    {

    }
    void roomTenClick()
    {

    }
    void roomElevenClick()
    {

    }
    //POWER-----------------------------------------------------------
    void roomOnePower()
    {
        powerManager.roomOnePowered = !powerManager.roomOnePowered;
    }
    void roomTwoPower()
    {
        powerManager.roomTwoPowered = !powerManager.roomTwoPowered;
    }
    void roomThreePower()
    {
        powerManager.roomThreePowered = !powerManager.roomThreePowered;
    }
    void roomFourPower()
    {
        powerManager.roomFourPowered = !powerManager.roomFourPowered;
    }
    void roomFivePower()
    {
        powerManager.roomFivePowered = !powerManager.roomFivePowered;
    }
    void roomSixPower()
    {
        powerManager.roomSixPowered = !powerManager.roomSixPowered;
    }
    void roomSevenPower()
    {
        powerManager.roomSevenPowered = !powerManager.roomSevenPowered;
    }
    void roomEightPower()
    {
        powerManager.roomEightPowered = !powerManager.roomEightPowered;
    }
    void roomNinePower()
    {
        powerManager.roomNinePowered = !powerManager.roomNinePowered;
    }
    void roomTenPower()
    {
        powerManager.roomTenPowered = !powerManager.roomTenPowered;
    }
    void roomElevenPower()
    {
        powerManager.roomElevenPowered = !powerManager.roomElevenPowered;
    }
    void roomTwelvePower()
    {
        powerManager.roomTwelvePowered = !powerManager.roomTwelvePowered;
    }
    void roomThirteenPower()
    {
        powerManager.roomThirteenPowered = !powerManager.roomThirteenPowered;
    }
    void roomZeroPower()
    {
        powerManager.roomZeroPowered = !powerManager.roomZeroPowered;
    }
}
