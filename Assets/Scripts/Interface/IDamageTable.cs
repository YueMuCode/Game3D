using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTable
{
    void GetHit(float damage,Transform attacker);
}
