using UnityEngine;
using System.Collections;

public class UserClass : MonoBehaviour {

    static public User currentUser = new User();

	public class User
    {
        public string username { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string questionID { get; set; }
        public bool correct { get; set; }

    }
}
