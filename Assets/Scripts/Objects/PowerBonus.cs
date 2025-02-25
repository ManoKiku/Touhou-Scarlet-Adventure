using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PowerBonus : Bonus
{
    protected override void AddBonus()
    {
        PlayerStatus.instance.powerAmount += addValue;
        if(PlayerStatus.instance.powerAmount >= 128)
        {
            PlayerStatus.instance.powerAmount = 128;
        }
    }
}
