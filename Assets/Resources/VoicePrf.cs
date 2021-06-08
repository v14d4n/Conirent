using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Voice;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

public class VoicePrf : MonoBehaviour
{
    private PhotonVoiceView _photonVoiceView;
    
    void Start()
    {
        _photonVoiceView = GetComponent<PhotonVoiceView>();
        _photonVoiceView.UsePrimaryRecorder = true;
        _photonVoiceView.SpeakerInUse = transform.GetChild(2).GetComponent<Speaker>();
    }
}
