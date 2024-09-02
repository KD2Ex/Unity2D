using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsVisitor
{
    public void Visit(HealthComponent health);
    public void Visit(ManaComponent mana);
}
