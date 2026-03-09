using UnityEngine;

public class TriggerAppear : MonoBehaviour
{
    public bool shouldAppear = false;
    [SerializeField] float appearSpeed = 0.5f;
    bool hasTriggered = false;

    void Update()
    {
        if (shouldAppear && !hasTriggered)
        {
            ProcessAppear();
        }
    }

    private void ProcessAppear()
    {
        // Get all renderers of the hit object and its children to fade them out        Renderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in meshRenderers)
        {
            foreach (var m in mr.materials)
            {
                Color c = m.color;
                c.a += appearSpeed * Time.deltaTime;
                m.color = c;

                if (c.a >= 1f)
                {
                    hasTriggered = true;
                }
            }
        }
    }
}
