using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestUIController : MonoBehaviour
{
    public GameManager gameManager;

    public Transform m_event;
    public TextMeshProUGUI m_eventTitle;
    public TextMeshProUGUI m_eventText;
    public Button m_firstButton;
    public Button m_secondButton;

    public Transform m_choice;
    public TextMeshProUGUI m_choiceTitle;
    public TextMeshProUGUI m_choiceText;
    public Button m_endButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRandomEventUI()
    {
        SetEventUI(gameManager.GetComponent<EventTestScript>().Events[Random.Range(0, gameManager.GetComponent<EventTestScript>().Events.Count)]);
    }

    public void SetEventUI(EventScriptable newEvent)
    {
        m_event.gameObject.SetActive(true);
        m_choice.gameObject.SetActive(false);
        m_firstButton.onClick.RemoveAllListeners();
        m_secondButton.onClick.RemoveAllListeners();
        m_eventTitle.text = newEvent.m_eventName;
        m_eventText.text = newEvent.m_eventText;
        m_firstButton.GetComponentInChildren<TextMeshProUGUI>().text = newEvent.m_firstChoice.m_choiceName;
        m_firstButton.onClick.AddListener(() => SetChoiceUI(newEvent.m_firstChoice));
        m_secondButton.GetComponentInChildren<TextMeshProUGUI>().text = newEvent.m_secondChoice.m_choiceName;
        m_secondButton.onClick.AddListener(() => SetChoiceUI(newEvent.m_secondChoice));
    }

    void SetChoiceUI(ChoiceScriptable newChoice)
    {
        m_event.gameObject.SetActive(false);
        m_choice.gameObject.SetActive(true);
        m_choiceTitle.text = newChoice.m_choiceName;
        m_choiceText.text = newChoice.m_choiceText;
        gameManager.ApplyChoice(newChoice);
    }
}
