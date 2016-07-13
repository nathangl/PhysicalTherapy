using UnityEngine;
using System.Collections;

public class AdjustPointsScript : MonoBehaviour {

	 void OnGUI()
    {
        if (GUI.Button(new Rect(10, 60, 100, 30), "Health up"))
        {
            LoadingUserPreviousActivities.control.health += 10; 
        } 
        if (GUI.Button(new Rect(10, 100, 100, 30), "Health Down"))
        {
            LoadingUserPreviousActivities.control.health -= 10;
        }
        
    }
}
