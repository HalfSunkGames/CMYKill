using UnityEngine;

[CreateAssetMenu(fileName = "New BGMData", menuName = "Data/BGMData")]
public sealed class BGMData :ScriptableObject{
	public float loopPoint;
	public AudioClip audioClip;
}
