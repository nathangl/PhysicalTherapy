using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Keywords : MonoBehaviour {
	int numKeywords=0;
    public QSearch qSearch;
	public int NumKeywords { get { return numKeywords; } }
	List<string> beginsWith = new List<string> { "What", "Why", "Do", "Are", "Does", "How", "Is", "Where" };
	Dictionary<string, string> keywordsDict = new Dictionary<string, string> {
        //The following are for subjective questions
		{ "happened", "happened" }, { "problem", "happened" }, { "issue", "happened" }, { "issues", "happened" },  { "problems", "happened" },
		{ "goals", "goals" }, { "goal", "goals"}, 
		{ "pain", "pain" }, { "pains", "pain" }, { "aches", "pain" }, { "feeling", "pain" },
		{ "live", "live" }, { "lives", "live" }, { "living", "live" },
		{ "home", "home" }, { "house", "home" }, { "apartment", "home" }, { "housing", "home" }, 
		{ "set up", "set up" }, { "setup", "set up" }, { "arrangement", "set up" }, { "arranged", "set up" }, { "organized", "set up" },
		{ "prior", "prior" }, { "before", "prior" },
		{ "managing", "managing" }, {"handling", "managing" }, { "doing", "managing" },
        { "where", "where" }, { "right", "right" }, { "arm", "arm" }, { "leg", "leg" },
        { "pick", "pick" }, { "lift", "pick" }, { "lap", "lap" }, { "look", "look" }, { "straighter", "straighter" },
        { "straight", "straighter" }, { "sit", "sit" },
        { "left", "left" }, { "hemiparesis", "hemiparesis" }, { "decreased", "decreased" }, { "strength", "strength" }, { "sensation", "sensation" }, { "visual", "visual" },
        { "tone", "tone" }, { "low", "low" }, { "alertness", "alertness" },
	};
	// Use this for initialization
	void Start () {
		/*
		string temp = "goal";
		string temp2;
		if (keywordsDict.ContainsKey(temp)) {
			keywordsDict.TryGetValue (temp, out temp2);

		}
		string test = "What happened to you";
		string[] test1 = test.Split (' ');
		FindKeywords (test1);
		*/
	}

	public string FindKeywords(string[] input)
	{
		int count=0;
		numKeywords = 0;
		string strMain="", temp;
		if(CheckFor(input) == true || qSearch.instructorQ == false)
		{
			foreach (var str in input) {
				string search = str;
				if (str == "set" && input[count + 1] == "up") {
					search = "setup";
				}
				keywordsDict.TryGetValue (search, out temp);
				if (temp != null && qSearch.instructorQ == true) {
					strMain += "Question LIKE '%" + temp + "%' AND ";
					numKeywords++;
				}
                else if (temp != null && qSearch.instructorQ == false)
                {
                    strMain += "answer LIKE '%" + temp + "%' AND ";
                    numKeywords++;
                }
				count++;
			}
			if(strMain.Length>4)
				strMain = strMain.Remove (strMain.Length - 4);
			Debug.Log (strMain);
		}
		return strMain;
	}

	bool CheckFor(string[] input)//Checks the begging of input and to see if it contains you/your
	{
		var match = beginsWith.Exists (x => x.ToLower() == input [0].ToLower());
		Debug.Log (match);
		if(match == true)
		{
			for(int i=0; i<input.Length; i++)
			{
				if (string.Equals(input[i], "you",System.StringComparison.OrdinalIgnoreCase) ||
					string.Equals(input[i], "your",System.StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(input[i], "yourself", System.StringComparison.OrdinalIgnoreCase))
						return true;
			}
		}
		return false;
	}
}
