using UnityEngine;

public class TriggerDisappear : MonoBehaviour
{
    public bool shouldDisappear = false;
    [SerializeField] float appearSpeed = 0.5f;
    void Update()
    {
        if (shouldDisappear)
        {
            ProcessDisappear();
        }
    }

    private void ProcessDisappear()
    {
        // Get all renderers of the hit object and its children to fade them out
        Renderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var r in meshRenderers)
        {
            foreach (var m in r.materials)
            {
                Color c = m.color;
                c.a -= appearSpeed * Time.deltaTime;
                m.color = c;

                // Destroy the object once it's fully transparent
                if (m.color.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }


    }
}
