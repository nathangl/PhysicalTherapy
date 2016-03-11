using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class TextReadDisplay : MonoBehaviour {

    public TextAsset chartOneFile;
    public string[] chartOneLines;
    private List<string> chartOneList;

    public TextAsset chartTwoFile;
    public string[] chartTwoLines;
    private List<string> chartTwoList;

    public Text title;
    public Text leftOne;
    public Text leftTwo;
    public Text leftThree;
    public Text leftFour;
    public Text leftFive;
    public Text leftSix;
    public Text rightOne;
    public Text rightTwo;
    public Text rightThree;
    public Text rightFour;
    public Text rightFive;
    public Text rightSix;


    void Start () {
        //int[] charts = new int[2] { 0, 1 };
        int random = Random.Range(0, 2);
        Debug.Log(random);


        if (random == 0)
        {
            chartOneList = new List<string>();
            if (chartOneFile != null)
            {
                chartOneLines = (chartOneFile.text.Split('\n'));
            }
            for (int i = 0; i < chartOneLines.Length; i++)
            {
                chartOneList.Add(chartOneLines[i]);
            }
            this.title.text = chartOneList[0];
            this.leftOne.text = chartOneList[1];
            this.leftTwo.text = chartOneList[2];
            this.leftThree.text = chartOneList[3];
            this.leftFour.text = chartOneList[4];
            this.leftFive.text = chartOneList[5];
            this.leftSix.text = chartOneList[6];
            this.rightOne.text = chartOneList[7];
            this.rightTwo.text = chartOneList[8];
            this.rightThree.text = chartOneList[9];
            this.rightFour.text = chartOneList[10];
            this.rightFive.text = chartOneList[11];
            this.rightFive.text = chartOneList[12];
        }

        if (random == 1)
        {
            chartTwoList = new List<string>();
            if (chartTwoFile != null)
            {
                chartTwoLines = (chartTwoFile.text.Split('\n'));
            }
            for (int i = 0; i < chartTwoLines.Length; i++)
            {
                chartTwoList.Add(chartTwoLines[i]);
            }
            this.title.text = chartTwoList[0];
            this.leftOne.text = chartTwoList[1];
            this.leftTwo.text = chartTwoList[2];
            this.leftThree.text = chartTwoList[3];
            this.leftFour.text = chartTwoList[4];
            this.leftFive.text = chartTwoList[5];
            this.leftSix.text = chartTwoList[6];
            this.rightOne.text = chartTwoList[7];
            this.rightTwo.text = chartTwoList[8];
            this.rightThree.text = chartTwoList[9];
            this.rightFour.text = chartTwoList[10];
            this.rightFive.text = chartTwoList[11];
            this.rightFive.text = chartTwoList[12];
        }

    }
	
	void Update () {
	
	}
}

