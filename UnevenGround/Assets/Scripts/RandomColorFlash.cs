using UnityEngine;

public class RandomColorFlash : MonoBehaviour
{
    private float changeInterval = 1f;

    Renderer[] renderers;
    float timer;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            timer = 0f;

            Color randomColor = new Color(
                Random.value,
                Random.value,
                Random.value
            );

            foreach (Renderer r in renderers)
            {
                r.material.color = randomColor;
            }
        }
    }
}