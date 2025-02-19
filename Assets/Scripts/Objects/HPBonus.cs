using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HPBonus : Bonus
{
    protected override void AddBonus()
    {
        PlayerStatus.instance.hp += addValue;
    }
}
