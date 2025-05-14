using TMPro;
using UnityEngine;

public enum Character
{
    NinjaFrog, PinkMan, VirutalGuy, MaskDude
}
public class DataManager : MonoBehaviour
{
    public Character currentCharacter;

    public TMP_InputField inputField;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        Debug.Log($"SelectCharacterUI = {currentCharacter}");
    }
    public void NameSave()
    {
        string name = inputField.text;
        Debug.Log($"inputTxt = {name}");
    }
}
