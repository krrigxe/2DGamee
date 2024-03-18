using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class charecterEvents
{
    public static UnityAction<GameObject, int> charecterDamaged;
    public static UnityAction<GameObject, int> charecterHealed;
}
