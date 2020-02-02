using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MainButtonAudio : MonoBehaviour
{
	public StudioEventEmitter audioEmitter;

    // Update is called once per frame
    public void ClickSound()
    {
        audioEmitter.Play ();
    }
}
