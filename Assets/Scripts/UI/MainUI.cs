using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {
	Communication comm = Communication.GetCommunication();
	// Use this for initialization
	void Start () {
#if DEBUG
		comm.SendMessageToServer ('A', "InitID InitPasswor");
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
