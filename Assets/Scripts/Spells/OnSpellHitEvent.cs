using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class OnSpellHitEvent : UnityEvent<PlayerController, ProjectileSpell, Vector3> {

}
