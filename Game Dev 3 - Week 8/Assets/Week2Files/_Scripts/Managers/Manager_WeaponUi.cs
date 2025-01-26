using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameDevWithNey.ScriptabelObjects;


namespace GameDevWithNey.Managers
{
    public class Manager_WeaponUi : MonoBehaviour
    {

        //References to the UI elements that we want to change 
        public TMP_Text gunName;
        public TMP_Text gunSubTitle;
        public TMP_Text gunDescription;
        public Image gunSymbol;



        //The method has a parameter of type scp_GunsType_ScriptableObject in 
        //order to be versatile when we need to use it.
        public void AssignUI(GunsType_ScriptableObject currentSO)
        {
            //These lines of code will assign the data we have in the s.o. to the Ui
            gunName.text = currentSO.gunName;
            gunSubTitle.text = currentSO.gunSubTitle;
            gunDescription.text = currentSO.gunDescription;
            gunSymbol.sprite = currentSO.gunSymbol.sprite;
        }

    }

}
