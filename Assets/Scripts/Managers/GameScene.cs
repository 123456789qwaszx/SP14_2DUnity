using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        GameObject player = Managers.Resource.Instantiate("Player");
        player.name = "Player";

        GameObject parallax = Managers.Resource.Instantiate("Parallax/Parallax_001");
        parallax.name = "Parallax";

        Managers.Map.SetdefaultMap();
        
    }

    void Start()
    {
        GameObject gameUI = Managers.Resource.Instantiate("GameUI");
        gameUI.name = "GameUI";
    }
      
    public override void Clear()
    {
        
    }
}
