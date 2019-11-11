using System;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

/// <summary>
/// Reads ELIAS' output and plays it via an AudioSource.
/// </summary>
public class EliasAudioReader
{
	private EliasHelper elias;
	private AudioSource audioSource;
	private GCHandle gcHandle;
	private float[] dataBuffer;
	private int currentDataIndex;
	private int bufferedLength;


    public volatile AudioSpeakerMode unityChannelMode;

	public EliasAudioReader(EliasHelper eliasHelper, AudioSource audioSourceTarget, bool useProceduralClip)
	{
		elias = eliasHelper;
		audioSource = audioSourceTarget;
		dataBuffer = new float[elias.FramesPerBuffer * elias.ChannelCount];
		if (useProceduralClip)
        {
            AudioClip clip = AudioClip.Create("elias clip", elias.FramesPerBuffer * elias.ChannelCount, elias.ChannelCount, elias.SampleRate, true, PCMReadCallback);
            audioSource.clip = clip;
        }
        // By not having any audio clip, and making sure ELIAS is the first effect on the Audio Source, ELIAS is treated as the "source".
		audioSource.loop = true;
		gcHandle = GCHandle.Alloc(dataBuffer, GCHandleType.Pinned);
	}

	/// <summary>
	/// Stops the AudioSource and disposes references. DOES NOT stop ELIAS!
	/// </summary>
	public void Dispose()
	{
		audioSource.Stop();
		audioSource = null;
		elias = null;
		gcHandle.Free ();
    }

    private void PCMReadCallback(float[] data)
    {
        ReadCallback(data);
    }


    public bool ReadCallback(float[] data)
	{
		currentDataIndex = 0;
		while (currentDataIndex < data.Length)
		{
            int channelCount;
            switch (unityChannelMode)
            {
                case AudioSpeakerMode.Mono:
                    channelCount = 1;
                    break;
                case AudioSpeakerMode.Stereo:
                    channelCount = 2;
                    break;
                case AudioSpeakerMode.Quad:
                    channelCount = 4;
                    break;
                case AudioSpeakerMode.Surround:
                    channelCount = 5;
                    break;
                case AudioSpeakerMode.Mode5point1:
                    channelCount = 6;
                    break;
                case AudioSpeakerMode.Mode7point1:
                    channelCount = 8;
                    break;
                default:
                    channelCount = -1;
                    break;
            }
			if (bufferedLength > 0 && elias.ChannelCount == channelCount)
			{
				int length = Math.Min(data.Length - currentDataIndex, bufferedLength);
				Array.Copy(dataBuffer, 0, data, currentDataIndex, length);
				currentDataIndex += length;
				bufferedLength -= length;
				if (bufferedLength > 0)
				{
					Array.Copy(dataBuffer, length, dataBuffer, 0, bufferedLength);
				}
			}
            else if (bufferedLength > 0)
            {
                copyConvertedData(data, channelCount);
            }
            else
			{
				EliasWrapper.elias_result_codes r = EliasWrapper.elias_read_samples(elias.Handle, Marshal.UnsafeAddrOfPinnedArrayElement(dataBuffer, 0));
				bufferedLength = elias.FramesPerBuffer * elias.ChannelCount;
				EliasHelper.LogResult(r, "Failed to render");
                if (r != EliasWrapper.elias_result_codes.elias_result_success)
                {
                    return false;
                }
			}
		}
        return true;
	}

    private void copyConvertedData(float[] data, int channelCount)
    {
        int lengthInFrames = Math.Min((data.Length - currentDataIndex) / channelCount, bufferedLength / elias.ChannelCount);
        if (elias.ChannelCount == 2 && unityChannelMode == AudioSpeakerMode.Mono)
        {
            convertStereoToMono(data, lengthInFrames, channelCount);
        }
        else if (elias.ChannelCount == 2 && unityChannelMode == AudioSpeakerMode.Quad)
        {
            convertStereoToQuad(data, lengthInFrames, channelCount);
        }
        else if (elias.ChannelCount == 2 && unityChannelMode == AudioSpeakerMode.Surround)
        {
            convertStereoToSurround(data, lengthInFrames, channelCount);
        }
        else if (elias.ChannelCount == 2 && unityChannelMode == AudioSpeakerMode.Mode5point1)
        {
            convertStereoTo51(data, lengthInFrames, channelCount);
        }
        else if (elias.ChannelCount == 2 && unityChannelMode == AudioSpeakerMode.Mode7point1)
        {
            convertStereoTo71(data, lengthInFrames, channelCount);
        }
        if (bufferedLength > 0)
        {
            Array.Copy(dataBuffer, lengthInFrames * elias.ChannelCount, dataBuffer, 0, Math.Min(dataBuffer.Length, bufferedLength));
        }
    }

    private void convertStereoToMono(float[] data, int lengthInFrames, int channelCount)
    {
        for (int i = 0; i < lengthInFrames; ++i)
        {
            float lf = dataBuffer[0 + i * 2];
            float rf = dataBuffer[1 + i * 2];
            float ce = (lf + rf) * 0.5f;
            data[currentDataIndex + i] = ce; /* MONO */
        }

        currentDataIndex += lengthInFrames * channelCount;
        bufferedLength -= lengthInFrames * elias.ChannelCount;
    }

    private void convertStereoToQuad(float[] data, int lengthInFrames, int channelCount)
    {
        for (int i = 0; i < lengthInFrames; ++i)
        {
            float lf = dataBuffer[0 + i * 2];
            float rf = dataBuffer[1 + i * 2];
            float ce = (lf + rf) * 0.5f;
            data[currentDataIndex + 0 + i * 4] = lf; /* FL */
            data[currentDataIndex + 1 + i * 4] = rf; /* FR */
            data[currentDataIndex + 2 + i * 4] = lf;  /* BL */
            data[currentDataIndex + 3 + i * 4] = rf;  /* BR */
        }

        currentDataIndex += lengthInFrames * channelCount;
        bufferedLength -= lengthInFrames * elias.ChannelCount;
    }

    private void convertStereoToSurround(float[] data, int lengthInFrames, int channelCount)
    {
        for (int i = 0; i < lengthInFrames; ++i)
        {
            float lf = dataBuffer[0 + i * 2];
            float rf = dataBuffer[1 + i * 2];
            float ce = (lf + rf) * 0.5f;
            data[currentDataIndex + 0 + i * 5] = lf; /* FL */
            data[currentDataIndex + 1 + i * 5] = rf; /* FR */
            data[currentDataIndex + 2 + i * 5] = ce;  /* FC */
            data[currentDataIndex + 3 + i * 5] = lf;  /* BL */
            data[currentDataIndex + 4 + i * 5] = rf;  /* BR */
        }

        currentDataIndex += lengthInFrames * channelCount;
        bufferedLength -= lengthInFrames * elias.ChannelCount;
    }

    private void convertStereoTo51(float[] data, int lengthInFrames, int channelCount)
    {
        for (int i = 0; i < lengthInFrames; ++i)
        {
            float lf = dataBuffer[0 + i * 2];
            float rf = dataBuffer[1 + i * 2];
            float ce = (lf + rf) * 0.5f;
            data[currentDataIndex + 0 + i * 6] = lf; /* FL */
            data[currentDataIndex + 1 + i * 6] = rf; /* FR */
            data[currentDataIndex + 2 + i * 6] = ce;  /* FC */
            data[currentDataIndex + 3 + i * 6] = 0;   /* LFE (only meant for special LFE effects) */
            data[currentDataIndex + 4 + i * 6] = lf;  /* BL */
            data[currentDataIndex + 5 + i * 6] = rf;  /* BR */
        }

        currentDataIndex += lengthInFrames * channelCount;
        bufferedLength -= lengthInFrames * elias.ChannelCount;
    }

    private void convertStereoTo71(float[] data, int lengthInFrames, int channelCount)
    {
        for (int i = 0; i < lengthInFrames; ++i)
        {
            float lf = dataBuffer[0 + i * 2];
            float rf = dataBuffer[1 + i * 2];
            float ce = (lf + rf) * 0.5f;
            data[currentDataIndex + 0 + i * 8] = lf; /* FL */
            data[currentDataIndex + 1 + i * 8] = rf; /* FR */
            data[currentDataIndex + 2 + i * 8] = ce;  /* FC */
            data[currentDataIndex + 3 + i * 8] = 0;   /* LFE (only meant for special LFE effects) */
            data[currentDataIndex + 4 + i * 8] = lf;  /* BL */
            data[currentDataIndex + 5 + i * 8] = rf;  /* BR */
            data[currentDataIndex + 6 + i * 8] = lf;  /* SL */
            data[currentDataIndex + 7 + i * 8] = rf;  /* SR */
        }

        currentDataIndex += lengthInFrames * channelCount;
        bufferedLength -= lengthInFrames * elias.ChannelCount;
    }
}