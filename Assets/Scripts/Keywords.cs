using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Keywords : MonoBehaviour
{
    static int numKeywords = 0;
    public static int NumKeywords { get { return numKeywords; } }
    static List<string> beginsWith = new List<string> { "What", "Why", "Do", "Are", "Does", "How", "Is", "Where", "Can" };
    static Dictionary<string, string> keywordsDict = new Dictionary<string, string> {
        { "happened", "happened" }, { "problem", "happened" }, { "issue", "happened" }, { "issues", "happened" },  { "problems", "happened" },
        { "goals", "goals" }, { "goal", "goals"},
        { "pain", "pain" }, { "pains", "pain" }, { "aches", "pain" }, { "feeling", "pain" }, { "discomfort", "pain" },
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

    public static string FindKeywords(string[] input, bool instructorQ)
    {
        int count = 0;
        numKeywords = 0;
        string strMain = "", temp;
        if (CheckFor(input) == true || instructorQ == false)
        {
            foreach (var str in input)
            {
                string search = str;
                if (str == "set" && input[count + 1] == "up")
                {
                    search = "setup";
                }
                keywordsDict.TryGetValue(search, out temp);
                if (temp != null && instructorQ == true)
                {
                    strMain += "Question LIKE '%" + temp + "%' AND ";
                    numKeywords++;
                }
                else if (temp != null && instructorQ == false)
                {
                    strMain += "answer LIKE '%" + temp + "%' AND ";
                    numKeywords++;
                }
                count++;
            }
            if (strMain.Length > 4)
                strMain = strMain.Remove(strMain.Length - 4);
            //Debug.Log(strMain);
        }
        return strMain;
    }

    static bool CheckFor(string[] input)//Checks the begging of input and to see if it contains you/your
    {
        var match = beginsWith.Exists(x => x.ToLower() == input[0].ToLower());
        //Debug.Log(match);

        return match;
    }
}
