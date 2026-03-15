using UnityEngine;

public class IntroSentencePulse : MonoBehaviour
{
    Vector3 startScale;
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.08f;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = startScale * scale;
    }
}