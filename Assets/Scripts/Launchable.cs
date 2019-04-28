using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Launchable : MonoBehaviour
{
    public abstract void Launch(Vector3 direction, float speed);
}
