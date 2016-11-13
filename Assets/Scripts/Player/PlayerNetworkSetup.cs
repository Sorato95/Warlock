using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkSetup : NetworkBehaviour {

    public Canvas healthBarCanvas;

    public Camera characterCam;

    public AudioListener audioListener;

    // Use this for initialization
    void Start () {
        if (isLocalPlayer)
        {
            GameObject.Find("Main Camera").SetActive(false);

            audioListener.enabled = true;
            characterCam.enabled = true;
        } else
        {
            healthBarCanvas.enabled = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
