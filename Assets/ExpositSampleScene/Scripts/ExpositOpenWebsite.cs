using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpositOpenWebsite : MonoBehaviour
{
    // Method to open a specified URL
    public void OpenURL(string url)
    {
        // Check if the URL is not empty or null
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogError("URL is empty or null!");
        }
    }

    // Example method to open a specific website
    public void OpenGoogle()
    {
        OpenURL("https://www.google.com");
    }

    // Example method to open another specific website
    public void OpenUnity()
    {
        OpenURL("https://unity.com");
    }
}
