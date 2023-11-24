using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ResourceHolder Resources; 
    List<Effect> Effects = new List<Effect>();
    // Start is called before the first frame update
    void Start()
    {
        Resources = new ResourceHolder(100f,100f,0f,0,0);

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
    public void ChangePopulation(float factor)
    {
        Resources.Population += factor;
    }
    public void ChangeFood(float factor)
    {
        Resources.Food += factor;
    }
    public void ChangeSuspicion(float factor)
    {
        Resources.Suspicion += factor;
    }
    public void ChangeSovietRep(int factor)
    {
        Resources.RepSoviet += factor;
    }
    public void ChangePeopleRep(int factor)
    {
        Resources.RepPeople += factor;
    }
}
