using UnityEngine;
using System.Collections;

public sealed class BGMPlayer {
	public float LoopPoint;
	public AudioClip audioClip;
	public AudioSource audioSource;

	public float Time {
		get {
			long p = m_currentTime;

			p /= m_samples.Length;

			return p * audioClip.length;
		}
		set {
			double m = value / audioClip.length;

			m_currentTime = (long)(m * m_samples.Length);
		}
	}
	public float Duration {
		get {
			return audioClip.length;
		}
	}
	public long SampleTime {
		get {
			return m_currentTime;
		}
		set {
			if (value >= m_samples.Length) {
				Debug.LogError("Value is larger than audioClip.samples");
				return;
			}
			if (value < 0) {
				Debug.LogError("Value is lesser than 0");
				return;
			}
			m_currentTime = value;
		}
	}
	public int SampleDuration {
		get {
			return m_samples.Length;
		}
	}

	private float[] m_samples;
	private long m_currentTime;
	private int m_sampleLoopPoint;
	private int m_channels;

	public void Play() {
		double mul = LoopPoint / audioClip.length;
		m_sampleLoopPoint = (int)(mul * (audioClip.samples));

		m_channels = audioClip.channels;
		m_samples = new float[audioClip.samples * m_channels];

		audioClip.GetData(m_samples, 0);

		audioClip = AudioClip.Create(audioClip.name + "_Loop", audioClip.samples, m_channels, audioClip.frequency, true, OnAudioRead, OnAudioSetPos);

		audioSource.loop = true;
		audioSource.clip = audioClip;
		m_currentTime = 0;
		audioSource.Play();
	}
	public void Stop() {
		if(audioSource)
			audioSource.Stop();
		m_currentTime = 0;
	}
	public void Replay() {
		if (!audioSource)
			return;
		audioSource.Stop();
		m_currentTime = 0;
		audioSource.Play();
	}
	public void ChangeTrack(AudioClip newTrack, float loop) {
		if (audioClip != null) {
			Stop();
			Object.Destroy(audioClip);
		}
		audioClip = newTrack;
		LoopPoint = loop;
		Play();
	}
	public void ChangeTrack(BGMData bgmData) {
		if (audioClip != null) {
			Stop();
			Object.Destroy(audioClip);
		}
		audioClip = bgmData.audioClip;
		LoopPoint = bgmData.loopPoint;
		Play();
	}

	void OnAudioRead(float[] data) {
		for(int count = 0; count < data.Length; count++) {
			data[count] = m_samples[m_currentTime];

			m_currentTime++;
			if (m_currentTime >= m_samples.Length) {
				m_currentTime = m_sampleLoopPoint*m_channels;
				Debug.Log(count+"/"+data.Length);
			}
		}
	}
	void OnAudioSetPos(int newPos) {

	}
}
