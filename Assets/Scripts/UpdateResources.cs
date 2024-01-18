using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateResources : MonoBehaviour
{
    private void FixedUpdate()
    {
        GameManager.instance.ShowEffects();
    }
}
