using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScriptData : MonoBehaviour
{
    public int index;
    public string talker;
    public string script;
    public float talkSpeed;
    public string standImg;

    public RoomScriptData(int index, string talker, string script, float talkSpeed, string standImg)
    {
        this.index = index;
        this.talker = talker;
        this.script = script;
        this.talkSpeed = talkSpeed;
        this.standImg = standImg;
    }
}
