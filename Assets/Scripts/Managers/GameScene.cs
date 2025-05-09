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


    }
    

    public override void Clear()
    {
        
    }
}
