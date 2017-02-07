using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour
{
    // the main camera
    private Camera _MainCamera;

    // water level
    public float waterLevel = 19;

    // sky fog
    public bool skyFog = true;
    public Color skyFogColor = Color.yellow;
    public float skyFogDensity = 1.0f;
    public float skyFogStart = 50.0f;
    public float skyFogEnd = 200.0f;
    public FogMode skyFogMode = FogMode.Linear;

    // water fog
    public bool waterFog = true;
    public Color waterFogColor = Color.blue;
    public float waterFogDensity = 5.0f;
    public float waterFogStart = 0.0f;
    public float waterFogEnd = 30.0f;
    public FogMode waterFogMode = FogMode.Linear;

    private bool isUnderwater;

    // Use this for initialization
    void Start ()
    {
        _MainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
        if (!_MainCamera)
        {
            Debug.LogWarning ("Can't find a camera with tag MainCamera!");
        }
    }
	
    // Update is called once per frame
    void Update ()
    {
        if (transform.position.y < waterLevel != isUnderwater)
        {
            isUnderwater = transform.position.y < waterLevel;
            if (isUnderwater)
                SetUnderwater ();
            if (!isUnderwater)
                UnsetUnderwater ();
        }
    }

    void SetUnderwater ()
    {
        _MainCamera.clearFlags = CameraClearFlags.SolidColor;
        _MainCamera.backgroundColor = waterFogColor;
        // vr rig
        Camera[] eyeCams = _MainCamera.GetComponentsInChildren<Camera> ();
        foreach (Camera eye in eyeCams)
        {
            eye.clearFlags = CameraClearFlags.SolidColor;
            eye.backgroundColor = waterFogColor;
        }
        Debug.Log ("Switch to underwater settings.");
        RenderSettings.fog = waterFog;
        RenderSettings.fogColor = waterFogColor;
        RenderSettings.fogDensity = waterFogDensity;
        RenderSettings.fogStartDistance = waterFogStart;
        RenderSettings.fogEndDistance = waterFogEnd;
        RenderSettings.fogMode = waterFogMode;
    }

    void UnsetUnderwater ()
    {
        _MainCamera.clearFlags = CameraClearFlags.Skybox;
        // vr rig
        Camera[] eyeCams = _MainCamera.GetComponentsInChildren<Camera> ();
        foreach (Camera eye in eyeCams)
        {
            eye.clearFlags = CameraClearFlags.Skybox;
        }
        Debug.Log ("Switch to sky settings.");
        RenderSettings.fog = skyFog;
        RenderSettings.fogColor = skyFogColor;
        RenderSettings.fogDensity = skyFogDensity;
        RenderSettings.fogStartDistance = skyFogStart;
        RenderSettings.fogEndDistance = skyFogEnd;
        RenderSettings.fogMode = skyFogMode;
    }
}
