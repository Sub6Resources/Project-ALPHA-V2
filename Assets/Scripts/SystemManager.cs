using UnityEngine;
using System.Collections;

public class SystemManager : MonoBehaviour {

    public float[] oxygenlevels;
    public float[] co2levels;
    public float[] airpressure;
    public float[] energy;

    private WarningSystem warningSystem;
    

	// Use this for initialization
	void Start () {
        warningSystem = FindObjectOfType<WarningSystem>();
	}

	/*
     * Red = immediate action
     * Yellow = error or broken
     * Blue = low levels
     * Magenta/Purple = high levels
     * Grey = permanently damaged
     * Black = emergencyMode.exe or system restart
     * Green = functional or balanced
    */

	void Update () {
	    for(int i=0; i<airpressure.Length; i++)
        {
            
            
            if(airpressure[i] < 6.9)
            {
                warningSystem.redalert("Air Pressure is at fatal levels!! CHECK FOR ERRORS NOW!!!");
            }
            else if (airpressure[i] < 8)
            {
                warningSystem.error("Air Pressure approaching fatal levels!! Check for issues immediately!!");
            }
            else if (airpressure[i] < 10)
            {
                warningSystem.warn("Air Pressure is less than 10 PSI, check for issues now!");
            }

            if(airpressure[i] > 50)
            {
                warningSystem.redalert("Air pressure is at fatal levels!! CHECK FOR ERRORS NOW!!!");
            }
            else if(airpressure[i] > 30)
            {
                warningSystem.error("Air pressure is above standard amount by a reasonable margin. Please check for issues.");
            } else if(airpressure[i] > 17)
            {
                warningSystem.warn("Air pressure is above 17 PSI, which is a reasonable amount above normal levels. Check for issues now.");
            }
        }
        for (int i = 0; i < energy.Length; i++)
        {
            if (energy[i] < 10)
            {
                warningSystem.redalert("emergencyMode.exe will initiate very soon!!! RESTORE POWER SOURCE NOW!!!");
            }
            else if (energy[i] < 50)
            {
                warningSystem.redalert("Energy is below 5%!! RESTORE POWER SOURCE NOW!!!");
            }
            else if (energy[i] < 200)
            {
                warningSystem.bluealert("Energy is below 20%!! Restore power source immediately!!");
            }
            else if (energy[i] < 400)
            {
                warningSystem.bluealert("Energy is below 40%! Restore power source immediately!");
            }
            else if (energy[i] < 1000)
            {
                warningSystem.greenalert("Energy is at 100%");
            }
        }

    }
    public float[][] valuesINeedSaved()
    {
        return new float[][] {oxygenlevels, co2levels, airpressure, energy};
    }

    public void setValuesOfAll(float[][] values)
    {
        oxygenlevels = values[0];
        co2levels = values[1];
        airpressure = values[2];
        energy = values[3];
    }
}
