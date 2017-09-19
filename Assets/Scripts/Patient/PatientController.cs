using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PatientController : MonoBehaviour
{

    public Dropdown userDropdown;
    public SphereCollider leftShoulder, rightShoulder;
    private RaycastHit hit;
    TreeNode root = new TreeNode { Value = "Patient" };
    Animator patientAnim;
    string dropdownIndex;
    bool dropdownActive = false;
    string currentTag = "";
    public string currentScreen = "";
    GameObject chart;
    Manager manager;
    public GameObject PROMAnims;
    string prevMode = ""; //Previous mode (AROM/PROM)
    public HandManager handManager;
    bool dropdownEnabled = true;
    bool PROMActive = false;
    DotControlller dotController;
    IEnumerable<GameObject> currentPROM; //The current PROM animation object
    [HideInInspector]
    public List<GameObject> PROMObjs = new List<GameObject>(); //List of all PROM gameobjects
    public Slider slider;
    public InputField notes;

    //temp
    string strengthTest = "";
    bool moving = false;
    bool found = false;

    public bool screen = false;
    public Text screenInfo;


    List<string> currentDD = new List<string>(); //the current dropdown list
    void Awake()
    {
        chart = GameObject.FindGameObjectWithTag("Chart");
        manager = chart.GetComponent<Manager>();
        //handManager = GameObject.Find("Hands").GetComponent<HandManager>();
        patientAnim = GetComponent<Animator>();
        dotController = GetComponent<DotControlller>();
    }

    void Start()
    {
        //Disables all PROMObjs
        for (int i = 1; i < PROMAnims.transform.childCount; i++)
        {
            PROMObjs.Add(PROMAnims.transform.GetChild(i).gameObject);
        }
        PROMObjs.ForEach(x => x.SetActive(false));

        //Creates tree of patient movements and joints
        CreatePatientTree();
    }

    void Update()
    {
        //Testing for a notification system
        if(screen)
        {
            screenInfo.gameObject.SetActive(true);
            WaitFor(.1f);
            screen = false;
        }

        //Temporary strength testing
        if (currentScreen == "STRENGTH")
        {
            if(strengthTest == "Extension" && currentTag == "RightShoulder")
            {
                if (slider.value == 3 && !found)
                {
                    found = true;
                    //DisableSlider();
                    moving = true;
                    //patientAnim.speed = -1.0f;
                    //patientAnim.SetBool("Mirror", true);
                    patientAnim.SetFloat("Speed", -1f);
                    patientAnim.speed = patientAnim.GetCurrentAnimatorStateInfo(0).speed;
                }
                if(moving)
                {
                    Debug.Log(patientAnim.speed);
                    if(patientAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < .3)
                    {
                        //patientAnim.speed = 1;
                        //patientAnim.SetBool("Mirror", false);
                        patientAnim.SetFloat("Speed", 1f);
                        patientAnim.speed = patientAnim.GetCurrentAnimatorStateInfo(0).speed;
                    }
                    if(patientAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > .5)
                    {
                        patientAnim.speed = 0;
                        moving = false;
                    }
                }
            }
        }

        //If mode is changed (like between AROM and PROM)
        if (currentScreen != prevMode)
        {
            patientAnim.speed = 1;
            patientAnim.Play("Idle2");
            if(currentPROM != null)
                currentPROM.GetEnumerator().Current.SetActive(false);
            prevMode = currentScreen;
        }

        //If mode is changed away from PROM, reset patient
        if (currentScreen != "PROM")
        {
            PROMAnims.SetActive(false);
            PROMActive = false;
        }
        else if (handManager.success == true)
        {
            dotController.DisableDots();

            PROMAnims.SetActive(true);
            PROMActive = true;
            handManager.success = false;
        }

        //Raycast management along with dropdowns
        if (Input.GetMouseButtonDown(0) && dropdownEnabled && EventSystem.current.IsPointerOverGameObject() == false) // if left mouse clicked
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // make a raycast to clicked position

            if (Physics.Raycast(ray, out hit, Mathf.Infinity)) // if the raycast hit something
            {
                if (currentScreen != "")
                {
                    if (currentTag != hit.collider.transform.tag && dropdownActive == true) //if raycast hits a new collider
                    {
                        currentTag = hit.collider.transform.tag;
                        SetDropdown(hit.collider.transform.tag);
                    }
                    else if (dropdownActive == false)   //if raycast hits a collider
                    {
                        currentTag = hit.collider.transform.tag;
                        SetDropdown(hit.collider.transform.tag);   // if (hit.collider.transform.tag == "Patient")
                    }
                }
            }

            if (dropdownActive && EventSystem.current.currentSelectedGameObject == null && currentScreen == "")
            {
                /*dropChecker = EventSystem.current.currentSelectedGameObject.layer;    //get gameobject's layer
                if (dropChecker != 8)
                {
                    DisableDropdown();
                }*/
                DisableDropdown();
            }
            else if (EventSystem.current.currentSelectedGameObject)
            {
                if (EventSystem.current.currentSelectedGameObject.layer != 8)
                {
                    DisableDropdown();
                }
            }
        }
    }

    //Creates a dropdown at the mouse location, given the tag of the object that was raycasted
    public void SetDropdown(string clicked)
    {
        if(clicked == "Untagged")
        {
            return;
        }
        Debug.Log(clicked);
        List<string> dropdownList = new List<string>();
        Action<TreeNode> traverse = null;
        traverse = (n) => {
            if (n.Value == clicked)
            {
                Debug.Log(n.Value + " CLICKED!");
                Tracker.LogData(n.Value + " CLICKED!");
                if(currentScreen == "PROM")
                {
                    Tracker.clicked.Add("PROM" + n.Value);
                }
                else
                {
                    Tracker.clicked.Add("AROM" + n.Value);
                }
                dropdownIndex = n.Value;
                dropdownList.Add(n.Value);
                currentDD.Add(n.Value);
                foreach (TreeNode nv in n.Nodes)
                {
                    //Debug.Log(nv.Value);
                    dropdownList.Add(nv.Value);
                    currentDD.Add(nv.Value);
                }
            }
            else
            {
                n.Nodes.ForEach(traverse);
            }

        };

        traverse(root);

        userDropdown.gameObject.SetActive(true);
        dropdownActive = true;
        userDropdown.transform.position = new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, 0);
        Debug.Log(new Vector3(Input.mousePosition.x + 80, Input.mousePosition.y - 15, Input.mousePosition.z));
        userDropdown.ClearOptions();
        userDropdown.AddOptions(dropdownList);
    }

    //Called from the dropdown's event system. Checks which value was entered
    public void DetermineDropdown()
    {
        patientAnim.Play("Idle2");
        if (userDropdown.value == 0)
        {
            return;
        }
        if (currentScreen == "AROM")
        {
            dotController.DisableDots();
            //patientAnim.SetTrigger("A" + dropdownIndex + userDropdown.value);
            patientAnim.Play("A" + dropdownIndex + userDropdown.value, 0);
        }
        else if (currentScreen == "PROM")
        {
            dotController.DisableDots();

            Action<TreeNode> traverse = null;
            traverse = (n) => {
                if (n.Value == "UpperExtremity")
                {
                    foreach (TreeNode nv in n.Nodes[0].Nodes)
                    {
                        if (nv.Value == currentTag)
                        {
                            dotController.EnableUpperHandDots();
                        }
                    }
                }
                else
                {
                    n.Nodes.ForEach(traverse);
                }

            };
            traverse(root);
            if (!dotController.hand)
                dotController.EnableLowerHandDots();
            //dotController.EnableHandDots();
            Debug.Log("PROM Animation Accessed");
            handManager.ToggleHands();
            handManager.currentlyTesting = dropdownIndex;
            Tracker.LogData("PROM " + dropdownIndex + " " + currentDD[userDropdown.value]);
            //string find = (dropdownIndex == "RightShoulder") ? "R" : "L";
            try
            {
                //currentPROM = GameObject.Find("PROMAnims/PROM" + find + currentDD[userDropdown.value]);
                string tempName = "PROM" + currentDD[0] + currentDD[userDropdown.value];
                PROMObjs.ForEach(x => x.SetActive(true));
                //currentPROM = from obj in PROMObjs where obj.name == tempName select obj;
                PROMObjs.Where(x => x.name != tempName).ToList().ForEach(x => x.SetActive(false));
                //currentPROM.GetEnumerator().Current.SetActive(true);
            }
            catch(Exception e)
            {
                Debug.Log(e);
                Debug.Log("Object not found " + "PROM" + currentDD[0] + currentDD[userDropdown.value]);
            }
        }

        else if (currentScreen == "STRENGTH")
        {
            strengthTest = currentDD[userDropdown.value];
            dotController.DisableDots();
            patientAnim.speed = 0;
            patientAnim.Play("A" + dropdownIndex + userDropdown.value, 0, 0.40f);
            slider.gameObject.SetActive(true);
        }

        else if (userDropdown.value == 2 && currentScreen == "")
        {
            currentScreen = "PROM";
            Debug.Log("PROM MODE");
        }
        else
            Debug.Log("Not Yet Implemented");

        DisableDropdown();
    }

    //Disables strength slider
    public void DisableSlider()
    {
        slider.value = 0;
        slider.gameObject.SetActive(false);
        found = false;
    }

    //Disables dropdown menu
    //TODO: fix setactive
    public void DisableDropdown()
    {
        userDropdown.value = 0;
        userDropdown.transform.position = new Vector3(10000, 0, 0);
        dropdownActive = false;
        //userDropdown.gameObject.SetActive(false);
    }


    //Called from all buttons
    public void ButtonInput(string mode)
    {
        DisableSlider();
        currentScreen = "";
        PROMActive = false;
        PROMAnims.SetActive(false);
        patientAnim.Play("Idle2");
        patientAnim.speed = 1;
        dotController.DisableDots();
        if (mode == "NOTES")
        {
            if (notes.IsActive())
                notes.gameObject.SetActive(false);
            else
                notes.gameObject.SetActive(true);
        }
        if (mode != "chart" && mode != "NOTES")
        {
            currentScreen = mode;
            dropdownEnabled = true;
            dotController.EnableDropdownDots();
        }
        if(slider.gameObject.activeSelf == true)
        {
            DisableSlider();
        }
        if(handManager.active)
        {
            handManager.ToggleHands();
        }
        Debug.Log(mode + " MODE");
        Tracker.LogData(mode + " MODE");
        Tracker.clicked.Add(mode);
    }

    IEnumerator WaitFor(float time)
    {
        yield return new WaitForSeconds(time);

        screenInfo.gameObject.SetActive(false);
    }

    public void CreatePatientTree()
    {
        root.Nodes.Add(new TreeNode { Value = "UpperExtremity" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Shoulder" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "LeftShoulder" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "RightShoulder" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Elbow" });
        root.Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "LeftElbow" });
        root.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "RightElbow" });
        root.Nodes[0].Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes.Add(new TreeNode { Value = "Hand" });
        root.Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "LeftHand" });
        root.Nodes[0].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[0].Nodes[2].Nodes.Add(new TreeNode { Value = "RightHand" });
        root.Nodes[0].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[0].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes.Add(new TreeNode { Value = "LowerExtremity" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "Hip" });
        root.Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "LeftHip" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[1].Nodes[0].Nodes[0].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "RightHip" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Abduction" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Adduction" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "Internal Rotation" });
        root.Nodes[1].Nodes[0].Nodes[1].Nodes.Add(new TreeNode { Value = "External Rotation" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "Knee" });
        root.Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "LeftKnee" });
        root.Nodes[1].Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[1].Nodes[1].Nodes[0].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "RightKnee" });
        root.Nodes[1].Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "Flexion" });
        root.Nodes[1].Nodes[1].Nodes[1].Nodes.Add(new TreeNode { Value = "Extension" });
        root.Nodes[1].Nodes.Add(new TreeNode { Value = "Ankle" });
        root.Nodes[1].Nodes[2].Nodes.Add(new TreeNode { Value = "LeftAnkle" });
        root.Nodes[1].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Dorsiflexion" });
        root.Nodes[1].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Plantarflexion" });
        root.Nodes[1].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Inversion" });
        root.Nodes[1].Nodes[2].Nodes[0].Nodes.Add(new TreeNode { Value = "Eversion" });
        root.Nodes[1].Nodes[2].Nodes.Add(new TreeNode { Value = "RightAnkle" });
        root.Nodes[1].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Dorsiflexion" });
        root.Nodes[1].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Plantarflexion" });
        root.Nodes[1].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Inversion" });
        root.Nodes[1].Nodes[2].Nodes[1].Nodes.Add(new TreeNode { Value = "Eversion" });
    }
}

class TreeNode
{
    public string Value { get; set; }
    public List<TreeNode> Nodes { get; set; }


    public TreeNode()
    {
        Nodes = new List<TreeNode>();
    }
}