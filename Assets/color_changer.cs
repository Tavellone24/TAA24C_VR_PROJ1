using UnityEngine;

public sealed class RandomEmission : MonoBehaviour
{
    [SerializeField] private float interval = 0.75f;
    [SerializeField][Range(1f, 10f)] private float intensity = 2.0f; // Boosts the "glow"

    private Material targetMaterial;
    private float timer;

    // Standard shader property name for Emission
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    void Start()
    {
        targetMaterial = GetComponent<Renderer>().material;
        targetMaterial.EnableKeyword("_EMISSION");

        // Set an initial random color
        SetRandomColor();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            SetRandomColor();
        }
    }

    private void SetRandomColor()
    {
        // Generates a random high-saturation color
        Color newColor = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);

        // Apply intensity for the HDR glow effect
        targetMaterial.SetColor(EmissionColor, newColor * intensity);
    }
}