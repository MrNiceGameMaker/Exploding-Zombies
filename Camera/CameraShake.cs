using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPosition;
    public WeaponsListSO weaponsList;

    [SerializeField] Vector3SO movement;
    // Method for shaking the camera
    public void Shake()
    {
        if (movement.value.x == 0)
        {
            originalPosition = transform.localPosition;
            float intensity = weaponsList.weaponsList[weaponsList.index].cameraShakeStrength;
            float duration = weaponsList.weaponsList[weaponsList.index].cameraShakeLength;
            StartCoroutine(ShakeCoroutine(intensity, duration));
        }

    }

    private IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Generate a random offset within a sphere and apply it to the camera's position
            Vector3 randomOffset = Random.insideUnitSphere * intensity;
            transform.localPosition = originalPosition + randomOffset;

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset camera position after the shake duration
        transform.localPosition = originalPosition;
    }
}
