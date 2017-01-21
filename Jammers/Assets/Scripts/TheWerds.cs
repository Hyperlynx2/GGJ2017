using UnityEngine;
using System.Collections;

public class TheWerds : MonoBehaviour {

	public static TheWerds tehWERDSofTheLord;

	public class arrayArray{
		public string[] stringArray;
	}

	public arrayArray[] theGodFatherArray;

	// Use this for initialization
	void Awake () {
		if (tehWERDSofTheLord == null) {
			this = tehWERDSofTheLord;
		} else {
			if (tehWERDSofTheLord != this){
				Destroy(this);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
