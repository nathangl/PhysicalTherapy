using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindingsManager : MonoBehaviour {

    public List<string> amount = new List<string>{ "Independent", "Supervision", "Stand-by Assist", "Contact Guard Assist", "Minimal Assistance", "Moderate Assistance",
        "Maximal Assistance", "Dependent", "Visual cues", "Tactile cues" };

    Dictionary<string, List<string>> type = new Dictionary<string, List<string>>()
    {
        {"Transfer w/c to/from bed",  new List<string>{ "stand pivot", "squat pivot", "lateral" } },
        {"Sit to/from stand", new List<string>{"block one knee", "block both knees", "no blocking required"}},
        {"Sitting", new List<string>{"verbal cues", "tactile cues", "physical assist"}},
        {"Standing", new List<string>{"verbal cues", "tactile cues", "physical assist"}},
        {"Walking", new List<string>{"verbal cues", "tactile cues", "physical assist"}}
    };

    public List<string> position = new List<string>{ "left side", "right side", "front", "behind" };

    public List<string> equipment = new List<string>{ "Gait Belt", "Sliding Board", "Rolling Walker", "Standard Walker", "Quad Cane", "Parallel bars", "Mirror-correct",
        "Hemi-sling" };

    Dictionary<string, List<int>> selected = new Dictionary<string, List<int>>()
    {
        {"Transfer w/c to/from bed", new List<int>{0, 0, 0, 0}},
        {"Sit to/from stand", new List<int>{0, 0, 0, 0}},
        {"Sitting", new List<int>{0, 0, 0, 0}},
        {"Standing", new List<int>{0, 0, 0, 0}},
        {"Walking", new List<int>{0, 0, 0, 0}}
    };

    public Button bed, sitstand, sitting, standing, walking, submit;
    public Text info;
    public Dropdown amountD, typeD, positionD, equipmentD;
    public string button;

    void Start()
    {
        Default();
    }

    void Default()      //starts on transfer dropdowns
    {
        button = "Transfer w/c to/from bed";
        info.text = button;
        amountD.AddOptions(amount);
        equipmentD.AddOptions(equipment);
        positionD.AddOptions(position);
        typeD.AddOptions(type[button]);
    }

    public void Button(string b)    //called by buttons
    {
        Save();
        button = b;
        UpdateUI();
        Load();
    }

    void UpdateUI()     //updates dropdown options
    {
        typeD.ClearOptions();
        info.text = button;
        typeD.AddOptions(type[button]);
    }

    void Save()     //saves dropdown values
    {
        selected[button][0] = amountD.value;
        selected[button][1] = typeD.value;
        selected[button][2] = positionD.value;
        selected[button][3] = equipmentD.value;
    }

    void Load()     //loads dropdown values
    {
        amountD.value = selected[button][0];
        typeD.value = selected[button][1];
        positionD.value = selected[button][2];
        equipmentD.value = selected[button][3];
    }
}
