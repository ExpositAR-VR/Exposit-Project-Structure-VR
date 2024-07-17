using UnityEngine;

public class AssistantVoiceManager : MonoBehaviour
{
	public AudioSource audioSource;
	public float updateStep = 1e-07f;
	public int sampleDataLength = 1024;

	private float currentUpdateTime = 0f;

	public float clipLoudness;
	private float[] clipSampleData;

	public GameObject AssistantBackground;
	public float sizeFactor = 30f;

	public float minSize = 0.81f;
	public float maxSize = 0.93f;

	// Use this for initialization
	private void Awake()
	{
		clipSampleData = new float[sampleDataLength];
	}

	// Update is called once per frame
	private void Update()
	{
		currentUpdateTime += Time.deltaTime;
		if (currentUpdateTime >= updateStep)
		{
			currentUpdateTime = 0f;
			audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
			clipLoudness = 0f;
			foreach (var sample in clipSampleData)
			{
				clipLoudness += Mathf.Abs(sample);
			}
			clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for

			clipLoudness *= sizeFactor;
			clipLoudness = Mathf.Clamp(clipLoudness, minSize, maxSize);
            AssistantBackground.transform.localScale = new Vector3(clipLoudness, clipLoudness, clipLoudness);
		}
	}
}