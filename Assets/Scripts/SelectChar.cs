using UnityEngine;

public enum Character
{
    NinjaFrog, PinkMan, VirutalGuy, MaskDude
}

public class SelectChar : MonoBehaviour
{
    public static Character currentCharacter;
    public Character character;

    Animator anim;
    SpriteRenderer sr;
    public SelectChar[] chars;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        if (currentCharacter == character) OnSelect();
        else OnDeSelect();
    }

    private void OnMouseUpAsButton()
    {
        currentCharacter = character;
        OnSelect();

        for(int i = 0; i < chars.Length; i++)
        {
            if (chars[i] != this) chars[i].OnDeSelect();
        }
    }

    void OnSelect()
    {
        anim.SetBool("isSelect", true);
        sr.color = new Color(1f, 1f, 1f);
    }void OnDeSelect()
    {
        anim.SetBool("isSelect", false);
        sr.color = new Color(0.5f, 0.5f, 0.5f);
    }
}
