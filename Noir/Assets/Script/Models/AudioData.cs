using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public struct AudioInfo
    {
        public string Name;
        public int ID;
        public AudioClip audioClip;

    }

    public AudioInfo[] audioInfo;


}
