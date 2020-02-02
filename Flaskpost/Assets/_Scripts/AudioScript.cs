using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioScript : MonoBehaviour
{
	[SerializeField] private StudioEventEmitter musicEmitter;	
	

	
	void Awake() {
	}

	void Start()
	{
		DontDestroyOnLoad(gameObject);
		if (!musicEmitter.IsPlaying ()) {
			musicEmitter.Play ();
			Debug.Log ("AudioScript. musicEmitter Started");
		}
		else
			Debug.Log ("AudioScript. musicEmitter NOT Started");
	}

}
