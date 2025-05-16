using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        //GameObject player = Managers.Resource.Instantiate("Player");

        GameObject parallax = Managers.Resource.Instantiate("Parallax/Parallax_004");

        Managers.Map.SetdefaultMap();
        
    }

    void Start()
    {
        GameObject gameUI = Managers.Resource.Instantiate("GameUI");
        // GameObject items = Managers.Resource.Instantiate("Items");
        // items.name = "Items";
    }
    public override void Clear()
    {
    }
}
