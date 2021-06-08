using Photon.Pun;
using Photon.Voice.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VoiceConnection))]
public class NetworkVoiceManager : MonoBehaviour
{
    public Transform _remoteVoiceParent;
    private VoiceConnection _voiceConnection;
    private Recorder _recorder;
    
    void Awake()
    {
        _voiceConnection = GetComponent<VoiceConnection>();
        _recorder = GetComponent<Recorder>();
    }

    private void OnEnable()
    {
        _voiceConnection.SpeakerLinked += this.OnSpeakerCreated;
    }

    private void OnDisable()
    {
        _voiceConnection.SpeakerLinked -= this.OnSpeakerCreated;
    }

    private void OnSpeakerCreated(Speaker speaker)
    {
        //_remoteVoiceParent.parent = speaker.transform;
        speaker.OnRemoteVoiceRemoveAction += OnRemoteVoiceRemove;
    }

    private void OnRemoteVoiceRemove(Speaker speaker)
    {
        if(speaker != null)
        {
            Destroy(speaker.gameObject);
        }
    }
}