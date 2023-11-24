using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ResourceHolder Resources; 
    List<Effect> Effects;
    // Start is called before the first frame update
    void Start()
    {
        Resources = new ResourceHolder(100f,100f,0f,0,0);
        Effects = new List<Effect>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void ApplyAllEffects()
    {
        List<Effect> effectsTemp = new List<Effect>();
        foreach(Effect effect in Effects)
        {
            if (Resources.TryApplyEffect(effect))
            {
                effectsTemp.Add(effect);
            }
        }
        Effects = effectsTemp;
    }
}
