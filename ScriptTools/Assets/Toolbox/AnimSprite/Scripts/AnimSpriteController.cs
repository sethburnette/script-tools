using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimSpriteController : MonoBehaviour
{
    public Image animatedImage;
    public float frameRate = 0.01f;
    public Sprite[] frames;

    private bool played = false;
    private int currentImage;
    
    void Start()
    {
        currentImage = 0;        
    }

    public void PlayOnceAnimation()
    {
        InvokeRepeating("ChangeImage", 0.1f, frameRate);
    }

    public void RepeatableAnimation()
    {
        if (currentImage != frames.Length)
        {
            InvokeRepeating("RepeatChangeImage", 0.1f, frameRate);            
        }            
    }

    public void RewindAnimation()
    {
        if (currentImage == frames.Length - 1)
        {
            InvokeRepeating("RewindChangeImage", 0.1f, frameRate);
        }

    }

    public void PingPongAnimation()
    {
        if (played == true)
        {
            InvokeRepeating("RewindChangeImage", 0.1f, frameRate);
            played = false;
        }

        else
        {
            InvokeRepeating("ChangeImage", 0.1f, frameRate);
            played = true;
        }
    }

    private void ChangeImage()
    {
        if (currentImage == frames.Length - 1)
        {
            CancelInvoke("ChangeImage");            
        }

        else
        {
            currentImage += 1;
            animatedImage.sprite = frames[currentImage];
        }
    }

    private void RepeatChangeImage()
    {
        if (currentImage == frames.Length - 1)
        {
            CancelInvoke("RepeatChangeImage");
            currentImage = 0;            
        }

        else
        {
            currentImage += 1;
            animatedImage.sprite = frames[currentImage];
        }
    }

    private void RewindChangeImage()
    {
        if (currentImage == frames.Length - frames.Length)
        {
            CancelInvoke("RewindChangeImage");
            currentImage = 0;            
        }

        else
        {
            currentImage -= 1;
            animatedImage.sprite = frames[currentImage];
        }
    }    


}
