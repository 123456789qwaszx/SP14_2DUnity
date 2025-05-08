using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    // 모든 기능을 싹다 끌어오는 GameObject
    protected override void Init()
    {
        base.Init();
        GameObject player = Managers.Resource.Instantiate("Player");
        player.name = "Player";
    }

    public override void Clear()
    {
        
    }
}
