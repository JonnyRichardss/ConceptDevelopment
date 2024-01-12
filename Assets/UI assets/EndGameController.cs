using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
    public Transform background;
    public Sprite[] images;
    public Transform text;
    // Start is called before the first frame update
    void Start()
    {
        text.GetComponent<TextMeshProUGUI>().text = GetEndReason();
    }

    string GetEndReason()
    {
        switch (GameManager.instance.endReason)
        {
            case GameManager.EndReason.allGone:
                background.GetComponent<Image>().sprite = images[0];
                return "Everyone in the village is gone, either they all died, they just left, or you sent them all away.";
            case GameManager.EndReason.allStarved:
                background.GetComponent<Image>().sprite = images[1];
                return "Everyone has starved, probably including you, the USSR will cover this up or spin this to be a success.";
            case GameManager.EndReason.suspicion:
                background.GetComponent<Image>().sprite = images[2];
                return "You may have USSR reputation or not, but this means nothing in the USSR if Stalin himself doesn't trust you, you have been purged.";
            case GameManager.EndReason.noPeopleRep:
                background.GetComponent<Image>().sprite = images[3];
                return "Your choices have either led you too close to the USSR or too far away from the people, either way the people have risen up against you.";
            case GameManager.EndReason.noSovietRep:
                background.GetComponent<Image>().sprite = images[4];
                return "You didn't listen to the USSR or you listened to the people too much, it seems the USSR cannot control you.";
            default:
                return "what!?";
        }
    }
}
