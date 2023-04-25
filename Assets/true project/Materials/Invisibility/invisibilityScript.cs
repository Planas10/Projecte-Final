using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibilityScript : MonoBehaviour
{

    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public float meshDestroyDelay = 3f;
    public Transform positionToSpawn;

    [Header("Shader Related")]
    public Material mat;
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderes;

    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown (KeyCode.LeftShift) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
            _renderer.material = mat;
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderes == null)
                skinnedMeshRenderes = GetComponentsInChildren<SkinnedMeshRenderer>();

            for(int i=0; i<skinnedMeshRenderes.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderes[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                StartCoroutine(AnimateMaterialFloat(mr.material, 0, shaderVarRate, shaderVarRefreshRate));

                Destroy(gObj, meshDestroyDelay);
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat (Material mat, float goal, float rate, float refreshRate)
    {
        float valueToAnime = mat.GetFloat(shaderVarRef);

        while (valueToAnime > goal)
        {
            valueToAnime -= rate;
            mat.SetFloat(shaderVarRef, valueToAnime);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
