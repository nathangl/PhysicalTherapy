using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindingsManager : MonoBehaviour {

    public string mode;

    public GameObject Options;
    public Toggle toggle;


    //FOR TRANSFERS
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

    public List<string> equipment = new List<string>{ "Gait Belt", "Sliding Board", "Rolling Walker", "Standard Walker", "Quad Cane", "Parallel bars", "Mirror",
        "Hemi-sling" };

    Dictionary<string, List<int>> selected = new Dictionary<string, List<int>>()
    {
        {"Transfer w/c to/from bed", new List<int>{0, 0, 0, 0}},
        {"Sit to/from stand", new List<int>{0, 0, 0, 0}},
        {"Sitting", new List<int>{0, 0, 0, 0}},
        {"Standing", new List<int>{0, 0, 0, 0}},
        {"Walking", new List<int>{0, 0, 0, 0}}
    };

    //FOR DEFICITS
    public List<string> ROMS = new List<string>{ "Right UE: Within Functional Limits", "Left UE: Within Functional Limits", "Right LE: Within Functional Limits", "Left LE:  Within Functional Limits", "Right shoulder", "Right elbow",
        "Right wrist/hand", "Left shoulder", "Left elbow", "Left wrist/hand" , "Right hip", "Right knee", "Right ankle", "Left hip", "Left knee", "Left ankle", "Cervical spine", "Lumbar-thoracic spine" };

    public List<string> STRENGTH = new List<string>{ "Right UE: Grossly within functional limits", "Left UE:  Grossly within functional limits", "Right LE:  Grossly within functional limits", "Left UE:  Grossly within functional limits", "Right shoulder flexors/extensors", "Right shoulder abductors",
        "Right shoulder rotators", "Right elbow flexors", "Right elbow extensors", "Right wrist flexors/extensors" , "Right hand grip strength", "Left shoulder flexors/abductors", "Left shoulder rotators", "Left elbow flexors", "Left elbow extensors", "Left wrist flexors/extensors", "Left hand grip strength", "Right hip flexors/extensors",
        "Right hip abductors", "Right knee extensors/flexors", "Right ankle dorsiflexion/plantar flexion", "Left hip flexors/extensors", "Left hip abductors", "Left knee extensors/flexors", "Left ankle dorsiflexion/plantar flexion"};

    public List<string> TONE = new List<string> { "No abnormal tone noted", "Left shoulder (flexors, extensors, adductors, rotators)", "Left elbow (flexors or extensors)", "Left wrist/hand (flexors or extensors)", "Right shoulder (flexors, extensors, adductors, rotators)", "Right elbow (flexors or extensors",
        "Right wrist/hand (flexors or extensors", "Left hip (flexors, extensors, adductors, rotators)", "Left knee (flexors or extensors)", "Left ankle (dorsiflexors or plantar flexors)", "Right hip (flexors, extensors, adductors, rotators)", "Right knee (flexors or extensors)", "Right ankle (dorsiflexors or plantar flexors)" };

    public List<string> COGNITION = new List<string> { "This patient did not demonstrate any cognitive deficits", "Suspect mild cognitive deficits that will require further examination" };

    public List<string> VISUAL = new List<string> { "this patient does not demonstrate any deficits", "this patient demonstrates the following deficits: Left inattention" };

    public List<Toggle> children = new List<Toggle>();
    public List<Toggle> choices = new List<Toggle>();

    Dictionary<string, List<bool>> toggled = new Dictionary<string, List<bool>>()
    {
        {"AROM", new List<bool>()},
        {"PROM", new List<bool>()},
        {"STRENGTH", new List<bool>()},
        {"TONE", new List<bool>()},
        {"COGNITION", new List<bool>()},
        {"VISUAL SPACIAL", new List<bool>()}
    };

    public Button bed, sitstand, sitting, standing, walking;
    public Text tInfo, dInfo;
    public Dropdown amountD, typeD, positionD, equipmentD;
    public string button;
    public GameObject transferUI, deficitUI;

    void Start()
    {
        mode = "Transfer";
        Default();
        SetupSaves();
    }

    void Default()      //starts on transfer dropdowns
    {
        if (mode == "Transfer")
        {
            button = "Transfer w/c to/from bed";
            tInfo.text = button;
            amountD.AddOptions(amount);
            equipmentD.AddOptions(equipment);
            positionD.AddOptions(position);
            typeD.AddOptions(type[button]);
        }
        else if (mode == "Deficit")
        {
            button = "AROM";
            dInfo.text = button;
            LoadToggles();
        }

    }

    public void Button(string b)    //called by buttons in transfer menus
    {
        Save();
        button = b;
        tInfo.text = button;
        UpdateUI();
        Load();
    }

    public void ButtonToggles(string b)     //called by buttons in deficit menus
    {
        SaveToggles();
        button = b;
        dInfo.text = button;
        DeleteToggles();
        LoadToggles();
    }

    void UpdateUI()     //updates dropdown options
    {
        typeD.ClearOptions();
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

    void LoadToggles()      //loads toggles
    {
        if(button == "AROM" || button == "PROM")
        {
            for(int i = 0; i < ROMS.Count; i++)
            {
                Toggle temp = Instantiate(toggle);
                temp.gameObject.GetComponentInChildren<Text>().text = ROMS[i];
                temp.transform.SetParent(Options.transform);
                try
                {
                    if (toggled[button][i])
                        temp.isOn = true;
                }
                catch
                {

                }
            }
        }
        else if(button == "STRENGTH")
        {
            for (int i = 0; i < STRENGTH.Count; i++)
            {
                Toggle temp = Instantiate(toggle);
                temp.gameObject.GetComponentInChildren<Text>().text = STRENGTH[i];
                temp.transform.SetParent(Options.transform);
                try
                {
                    if (toggled[button][i])
                        temp.isOn = true;
                }
                catch
                {

                }
            }
        }
        else if (button == "TONE")
        {
            for (int i = 0; i < TONE.Count; i++)
            {
                Toggle temp = Instantiate(toggle);
                temp.gameObject.GetComponentInChildren<Text>().text = TONE[i];
                temp.transform.SetParent(Options.transform);
                try
                {
                    if (toggled[button][i])
                        temp.isOn = true;
                }
                catch
                {

                }
            }
        }
        else if (button == "COGNITION")
        {
            for (int i = 0; i < COGNITION.Count; i++)
            {
                Toggle temp = Instantiate(toggle);
                temp.gameObject.GetComponentInChildren<Text>().text = COGNITION[i];
                temp.transform.SetParent(Options.transform);
                try
                {
                    if (toggled[button][i])
                        temp.isOn = true;
                }
                catch
                {

                }
            }
        }
        else if (button == "VISUAL SPACIAL")
        {
            for (int i = 0; i < VISUAL.Count; i++)
            {
                Toggle temp = Instantiate(toggle);
                temp.gameObject.GetComponentInChildren<Text>().text = VISUAL[i];
                temp.transform.SetParent(Options.transform);
                try
                {
                    if (toggled[button][i])
                        temp.isOn = true;
                }
                catch
                {

                }
            }
        }
    }

    void SaveToggles()      //saves which toggles are active in dictionaries
    {
        foreach (Transform child in Options.transform)
        {
            children.Add(child.gameObject.GetComponent<Toggle>());
        }
        if(button == "AROM" || button == "PROM")
        {
            for (int i = 0; i < ROMS.Count; i++)
            {
                if (children[i].isOn)
                    toggled[button][i] = true;
            }
        }
        else if(button == "STRENGTH")
        {
            for (int i = 0; i < STRENGTH.Count; i++)
            {
                if (children[i].isOn)
                    toggled[button][i] = true;
            }
        }
        else if(button == "TONE")
        {
            for (int i = 0; i < TONE.Count; i++)
            {
                if (children[i].isOn)
                    toggled[button][i] = true;
            }
        }
        else if (button == "COGNITION")
        {
            for (int i = 0; i < COGNITION.Count; i++)
            {
                if (children[i].isOn)
                    toggled[button][i] = true;
            }
        }
        else if (button == "VISUAL SPACIAL")
        {
            for (int i = 0; i < VISUAL.Count; i++)
            {
                if (children[i].isOn)
                    toggled[button][i] = true;
            }
        }
    }

    void SetupSaves()       //Setup lists for toggle saves
    {
        for (int i = 0; i < ROMS.Count; i++)
        {
            toggled["AROM"].Add(false);
            toggled["PROM"].Add(false);
        }
        for (int i = 0; i < STRENGTH.Count; i++)
        {
            toggled["STRENGTH"].Add(false);
        }
        for (int i = 0; i < TONE.Count; i++)
        {
            toggled["TONE"].Add(false);
        }
        for (int i = 0; i < COGNITION.Count; i++)
        {
            toggled["COGNITION"].Add(false);
        }
        for (int i = 0; i < VISUAL.Count; i++)
        {
            toggled["VISUAL SPACIAL"].Add(false);
        }
    }

    void DeleteToggles()
    {
        foreach (Transform child in Options.transform)
        {
            Destroy(child.gameObject);
        }
        children.Clear();
    }

    public void Continue()
    {
        mode = "Deficit";
        transferUI.SetActive(false);
        deficitUI.SetActive(true);
        Default();
    }

    /*public void Switch(int i)         //switches between two options, but cant attach to prefab...
    {
        foreach (Transform child in Options.transform)
        {
            choices.Add(child.gameObject.GetComponent<Toggle>());
        }
        if(choices[0].isOn && i == 1)
        {
            choices[0].isOn = false;
        }
        else if(choices[1].isOn && i == 0)
        {
            choices[1].isOn = false;
        }
    }*/

    public void Submit()
    {

    }
}
