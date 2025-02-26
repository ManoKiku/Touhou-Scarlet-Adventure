using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumiaBoss : BossStatus
{
    public override void OnDead()
    {
        Debug.Log("Dead!");
        Destroy(gameObject);
    }
}
