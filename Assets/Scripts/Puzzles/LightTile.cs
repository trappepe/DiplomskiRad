using UnityEngine;

public class LightTile : MonoBehaviour
{
    public LightTile[] neighbors;
    public Light lightSource;
    public ParticleSystem particles;

    public bool isOn = false;

     void Awake()
    {
        lightSource = GetComponentInChildren<Light>(true);
    particles = GetComponentInChildren<ParticleSystem>(true);

        SetState(false);
    }
    void Start()
{
    SetState(false);
}

    public void SetState(bool state)
    {
        isOn = state;

        if (lightSource != null)
            lightSource.enabled = isOn;

        if (particles != null)
        {
            if (isOn)
                particles.Play();
            else
                particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void Toggle()
    {
        SetState(!isOn);
    }

    public void ToggleWithNeighbors()
    {
        Toggle();

        foreach (var n in neighbors)
        {
            if (n != null)
                n.Toggle();
        }
    }
}