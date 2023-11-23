using System.Collections;
using UnityEngine;
using System;

public class SimpleObjectAnimator : MonoBehaviour
{
    [SerializeField] Transform startTransform;
    [SerializeField] Transform endTransform;

    [SerializeField] float animationLength;

    public Action OnAnimationComplete;


    public void Play()
    {
        gameObject.SetActive(true);

        StartCoroutine(AnimateCor());
    }

    IEnumerator AnimateCor()
    {
        float time = 0f;
        float increment = 1f / animationLength;
        Vector3 startPos = startTransform.position;
        Vector3 endPos = endTransform.position;

        while (time < 1f)
        {
            time += increment * Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }

        transform.position = endPos;

        OnAnimationComplete?.Invoke();
    }
}
