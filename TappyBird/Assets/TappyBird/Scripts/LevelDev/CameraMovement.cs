using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    void OnEnable()  => GameManager.OnLoseGame += LoseLevel;
    void OnDisable() => GameManager.OnLoseGame -= LoseLevel;

    private void LoseLevel() =>  StartCoroutine(Shake(GameManager.LoseToResetDelay * 0.8f, 0.05f));

    /// <summary> Shake the camera with a certain magnitude for a set time. Perfect for getting hit or losing effect. </summary>
    private IEnumerator Shake(float shakeDuration, float shakeMagnitude)
    {
        var originalPos = transform.localPosition;
        var timer  = 0f;
        var delayBetweenIndividualMovements = 0.025f;
        while (timer < shakeDuration)
        {
            yield return new WaitForSecondsRealtime(delayBetweenIndividualMovements);
            timer += delayBetweenIndividualMovements;
            float nextXPos = Random.Range(-1f, 1f) * shakeMagnitude;
            float nextYPos = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.localPosition = new Vector3(nextXPos, nextYPos, originalPos.z);
        }
        transform.localPosition = originalPos;
    }

}