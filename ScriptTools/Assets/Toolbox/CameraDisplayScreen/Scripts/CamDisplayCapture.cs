using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CamDisplayCapture : MonoBehaviour
{
    public Camera captureCam;
    public int fileCounter = 0;
    public GameObject textDisplay;

    private string captureInfo;
    private Text currentText;

    void Start()
    {
        if (textDisplay.GetComponent<Text>() != null)
        {
            currentText = textDisplay.GetComponent<Text>();
        }
        else
        {
            //do nothing
        }
    }

    public void CamCapture()
    {
        RenderTexture currentRendTex = RenderTexture.active;
        RenderTexture.active = captureCam.targetTexture;

        captureCam.Render();

        Texture2D Image = new Texture2D(captureCam.targetTexture.width, captureCam.targetTexture.height);
        Image.ReadPixels(new Rect(0, 0, captureCam.targetTexture.width, captureCam.targetTexture.height), 0, 0);
        Image.Apply();
        RenderTexture.active = currentRendTex;

        var Bytes = Image.EncodeToPNG();
        Destroy(Image);

        File.WriteAllBytes(Application.productName + "_" + Time.realtimeSinceStartup + "_" + fileCounter + ".png", Bytes);
        fileCounter++;

        captureInfo = "PNG file saved";

        currentText.text = captureInfo;
    }
}
