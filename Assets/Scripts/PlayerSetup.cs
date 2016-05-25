using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] componentsToDisable;

    Camera sceneCamera;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            //for (int i = 0; i < componentsToDisable.Length; i++)
            //{
            //    componentsToDisable[i].enabled = true;
            //}
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
        }
	}
	
	// Update is called once per frame
	void OnDisable () {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
