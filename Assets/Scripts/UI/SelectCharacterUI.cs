using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//public enum Character
//{
//    NinjaFrog, PinkMan, VirutalGuy, MaskDude
//}
public class SelectCharacterUI : MonoBehaviour
{
    public Character currentCharacter;

    public TMP_InputField inputField;

    public void NameSave()
    {
        string name = inputField.text;  
        Debug.Log($"inputTxt = {name}");
    }
}
