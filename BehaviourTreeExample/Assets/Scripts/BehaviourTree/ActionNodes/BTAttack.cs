using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAttack : BTNode
{
    private IDamageable target;
    private GameObject attacker;
    private int damage;
    public BTAttack(GameObject _attacker, int _damage, IDamageable _target)
    {
        target = _target;
        damage = _damage;
        attacker = _attacker;
    }
    public override BTResult Run()
    {
        target.TakeDamage(attacker, damage);
        return BTResult.Success;
    }
}
