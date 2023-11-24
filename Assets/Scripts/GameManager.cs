using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ResourceHolder Resources; 
    List<Effect> Effects;
    GameState state;
    // Start is called before the first frame update
    void Start()
    {
        Resources = new ResourceHolder();
        Effects = new List<Effect>();
        state = GameState.Menu;
    }

    public void NewGame()
    {
        Resources = new ResourceHolder(100f, 100f, 0f, 0, 0);
        Effects = new List<Effect>();
        state = GameState.ThroneRoom;
    }
    private void AdvanceState()
    {
        state = (GameState) (((int)state + 1) % 3);
    }
    private void ApplyAllEffects()
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
