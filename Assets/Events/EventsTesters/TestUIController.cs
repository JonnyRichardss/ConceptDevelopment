using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TestUIController : MonoBehaviour
{
    public static TestUIController instance;

    public Transform m_event;
    public TextMeshProUGUI m_eventTitle;
    public TextMeshProUGUI m_eventText;
    public Button m_firstButton;
    public Button m_secondButton;
    public Button m_returnButton;

    public Transform m_choice;
    public TextMeshProUGUI m_choiceTitle;
    public TextMeshProUGUI m_choiceText;
    public TextMeshProUGUI m_reference;
    public Button m_endButton;

    bool drawingEvents = false;
    bool isEvent = false;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
        SetEventUI(transform.GetComponent<EventTestScript>().Events[Random.Range(0, transform.GetComponent<EventTestScript>().Events.Count)]);
    }

    public void SetEventUI(EventScriptable newEvent)
    {
        m_event.gameObject.SetActive(true);
        m_choice.gameObject.SetActive(false);
        m_firstButton.onClick.RemoveAllListeners();
        m_secondButton.onClick.RemoveAllListeners();
        m_returnButton.onClick.RemoveAllListeners();
        m_eventTitle.text = newEvent.m_eventName;
        m_eventText.text = newEvent.m_eventText;
        m_reference.text = newEvent.reference;
        m_firstButton.GetComponentInChildren<TextMeshProUGUI>().text = newEvent.m_firstChoice.m_choiceName;
        m_firstButton.onClick.AddListener(() => SetChoiceUI(newEvent.m_firstChoice));
        m_firstButton.GetComponent<ChoiceContainer>().choice = newEvent.m_firstChoice;
        m_secondButton.GetComponentInChildren<TextMeshProUGUI>().text = newEvent.m_secondChoice.m_choiceName;
        m_secondButton.onClick.AddListener(() => SetChoiceUI(newEvent.m_secondChoice));
        m_secondButton.GetComponent<ChoiceContainer>().choice = newEvent.m_secondChoice;
        m_returnButton.onClick.AddListener(() => SetEvent(false));
    }

    void SetChoiceUI(ChoiceScriptable newChoice)
    {
        m_event.gameObject.SetActive(false);
        m_choice.gameObject.SetActive(true);
        m_choiceTitle.text = newChoice.m_choiceName;
        m_choiceText.text = newChoice.m_choiceText;
        if (newChoice.m_eventScript != null)
        {
            newChoice.m_eventScript.TriggerEventScript();
        }
        GameManager.instance.Effects.Add(newChoice.m_choiceEffect);
        GameManager.instance.ApplyChoiceChange(newChoice.m_choiceEffect);
    }

    public void SetEvent(bool choice)
    {
        isEvent = choice;
    }

    public void CallDrawEvents(int eventNumber, Action<bool> callback)
    {
        print("guh?");
        StartCoroutine(DrawEventsRoutine(eventNumber, callback));
    }

    IEnumerator DrawEventsRoutine(int eventNumber, Action<bool> callback)
    {
        for (int i = 0; i < eventNumber; i++)
        {
            SetRandomEventUI();
            SetEvent(true);
            while (isEvent)
            {
                yield return new WaitForFixedUpdate();
            }
        }
        callback(true);
    }
}
