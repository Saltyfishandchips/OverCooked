using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class VoiceObjectSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] delivberyFail;
    public AudioClip[] delivberySuccess;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] panSizzle;
    public AudioClip[] trash;
    public AudioClip[] footStep;
    public AudioClip[] waring;
}
