using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBGM : MonoBehaviour {
	public BGMData bgm;
	public float delay = 2f;
	public AudioSource audioSource;

	private static BGMPlayer m_bgmPlayer;

	IEnumerator Start() {
		if (m_bgmPlayer == null)
			m_bgmPlayer = new BGMPlayer();
		else
			m_bgmPlayer.Stop();
		m_bgmPlayer.audioSource = audioSource;

		yield return new WaitForSeconds(delay);

		m_bgmPlayer.ChangeTrack(bgm);
	}
}
