using System.Collections;
using System.Collections.Generic;
using gun;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public GameObject playerMechHealthObj;//the player object
    public MechHealthScript mechHealthScript;


    public GameObject currentWeapon;//the current gun equipped
    public GunScript gunScript;//the gunScript

    public GameObject mechHealthTextObject;
    public TextMeshProUGUI mechHealthText;

    public GameObject ammunitionObject;//the object of the ammunition text
    public GameObject spareAmmunitionObject;//the object of the spare ammunition text
    public TextMeshProUGUI ammunitionText;//ammunition text
    public TextMeshProUGUI spareAmmunitionText;//spare ammunition text 
    // Start is called before the first frame update

    void Awake()
    {
        ammunitionObject = GameObject.Find("AmmunitionText"); //finds ammunition text object
        spareAmmunitionObject = GameObject.Find("SpareAmmunitionText");//finds spare ammunition text object

        ammunitionText = ammunitionObject.GetComponent<TextMeshProUGUI>();//sets the ammunition text variable to the ammunition text object text
        spareAmmunitionText = spareAmmunitionObject.GetComponent<TextMeshProUGUI>();//sets the spare ammunition text variable to the spare ammunition text object text

        mechHealthTextObject = GameObject.Find("MechHealthText");
        mechHealthText = mechHealthTextObject.GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        currentWeapon = GameObject.Find("RArmTool");
        

        gunScript = currentWeapon.GetComponent<GunScript>();//gunscript is set to the currentweapon gunscript

        mechHealthScript = playerMechHealthObj.GetComponent<MechHealthScript>();

    }

    // Update is called once per frame
    void Update()
    {

        

        mechHealthText.text = ("Health : " + mechHealthScript.playerHealth);

        if (currentWeapon != null)
        {
            gunScript = currentWeapon.GetComponent<GunScript>();//the gunscript variable is the gunscript variable within the currentgun object.
            ammunitionText.text = gunScript.currentAmmunition.ToString() + " / " + gunScript.maxAmmunition.ToString();//sets the ammunition text appropriately, using the currentAmmunition and maxAmmunition variables.
            spareAmmunitionText.text = gunScript.spareAmmunition.ToString();//sets the spare ammunition text appropriately, using the spareAmmunition variables

        }



    }
}