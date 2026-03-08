using UnityEngine;

public class TriggerAppear : MonoBehaviour
{
    public bool shouldAppear = false;
    [SerializeField] float appearSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldAppear)
        {
            ProcessAppear();
        }
    }

    private void ProcessAppear()
    {
        // Get all renderers of the hit object and its children to fade them out
        Renderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        foreach (var m in meshRenderer.materials)
        {
            Color c = m.color;
            c.a += appearSpeed * Time.deltaTime;
            m.color = c;
        }
    }
}
