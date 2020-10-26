using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MyPostprocessor : AssetPostprocessor
{
    private static readonly float DecompressSizeBorderKB = 200 * Mathf.Pow(2, 10);
    private static readonly float StreamingSizeBorderMB = 2 * Mathf.Pow(2, 20);
    
    private void OnPreprocessAudio()
    {
        AudioImporter audioImporter = (AudioImporter)assetImporter;
        audioImporter.loadInBackground = true;
        audioImporter.preloadAudioData = true;
        
        var newSettings = audioImporter.defaultSampleSettings;

        var info = new System.IO.FileInfo(audioImporter.assetPath);
        var fileSize = info.Length;
        if (fileSize <= DecompressSizeBorderKB)
        {
            newSettings.loadType = AudioClipLoadType.DecompressOnLoad;
        }
        else
        {
            if (fileSize <= StreamingSizeBorderMB)
            {
                newSettings.loadType = AudioClipLoadType.CompressedInMemory;
            }
            else
            {
                newSettings.loadType = AudioClipLoadType.Streaming;
            }
        }
        audioImporter.SetOverrideSampleSettings("Standalone", newSettings);
    }
}
