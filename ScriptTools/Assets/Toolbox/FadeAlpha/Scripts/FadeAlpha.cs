using UnityEngine;
using System.Collections;
using UnityEditorInternal;

public class FadeAlpha : MonoBehaviour {

    [SerializeField]
    public float duration = 1.0f;

    private Color originalColor;
    private Color fadedColor = new Color(0, 0, 0, 0);
    
    void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
        Shader.WarmupAllShaders();
    }

    public void SwitchToFade()
    {
        // get the material from the object
        var mat = GetComponent<Renderer>().material;

        // set the Rendering Mode to Fade (0 is Opaque, 1 is Cutout, 2 is Fade, and 3 is Transparent)
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");       
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        // start the coroutine that blends from one color to another
        StartCoroutine("FadeOut");
    }

    private IEnumerator FadeOut()
    {
        // get the material from the object
        var mat = GetComponent<Renderer>().material;

        // initialize a time counter
        float elapsedTime = 0.0f;

        // store the starting material color
        Color startingColor = mat.color;

        // lerp the fade transition over the amount of time specified in the inspector
        while (elapsedTime < duration)
        {
            // update the color once per frame based on how much time has elapsedTime
            mat.color = Color.Lerp(startingColor, fadedColor, (elapsedTime / duration));
            // keep track of how much time has passed since the coroutine started
            elapsedTime = elapsedTime + Time.deltaTime;            
            yield return null;
        }

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void SwitchToOpaque()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;

        // start the coroutine that blends from one color to another
        StartCoroutine("FadeIn");
    }

    private IEnumerator FadeIn()
    {
        // get the material from the object
        var mat = GetComponent<Renderer>().material;

        // initialize a time counter
        float elapsedTime = 0.0f;

        // store the starting material color
        Color startingColor = mat.color;

        // lerp the fade transition over the amount of time specified in the inspector
        while (elapsedTime < duration)
        {
            // update the color once per frame based on how much time has elapsedTime
            mat.color = Color.Lerp(startingColor, originalColor, (elapsedTime / duration));
            // keep track of how much time has passed since the coroutine started
            elapsedTime = elapsedTime + Time.deltaTime;            
            yield return null;
        }

        // the transition has finished because the while loop is done
        // just in case the color didn't finish changing all the way back to what it should be:
        mat.color = originalColor;
        mat.SetFloat("_Mode", 0);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1;
    }
}