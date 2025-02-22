using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelFadeOut : MonoBehaviour
{
    public WeaponsListSO weaponsList;
    public float fadeDuration = 2.0f; // Duration of the fade in seconds
    private float timer = 0f;
    private CanvasRenderer canvasRenderer;
    Color startColor;
    private void Awake()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        // Optionally, start the fading process immediately
        startColor = canvasRenderer.GetColor();
    }
    private void OnEnable()
    {
        StartCoroutine(FadeOut());
    }
    private void OnDisable()
    {
        canvasRenderer.SetColor(startColor);

    }
    private IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < weaponsList.weaponsList[weaponsList.index].reloadingTime)
        {
            // Calculate the normalized alpha value based on the timer and fade duration
            float alpha = 1.0f - Mathf.Clamp01(timer / weaponsList.weaponsList[weaponsList.index].reloadingTime);

            // Apply the alpha value to the panel's color
            Color color = canvasRenderer.GetColor();
            color.a = alpha;
            canvasRenderer.SetColor(color);

            // Increment timer based on time passed since last frame
            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the panel is fully faded out at the end
        Color finalColor = canvasRenderer.GetColor();
        finalColor.a = 0f;
        canvasRenderer.SetColor(finalColor);
    }
}

