using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject[] props = default;
    [SerializeField] Renderer[] meshes = default;
    [SerializeField] int vineta = 0;
    //MaterialPropertyBlock mtblock = default;
    SCR_PoolMan pool = default;

    private void Awake()
    {
        //mtblock = new MaterialPropertyBlock();
    }
    private void Start()
    {
        pool = SCR_PoolMan.instance;
    }
    public void HacerEfecto()
    {
        //DOTween.To(updateMaterial, 0, 1, 1.25f).SetEase(Ease.OutQuad);
    }
    void updateMaterial(float _tValue)
    {
        /*mtpblock.SetFloat("_tEndValue", _tValue);
        quadFXrenderer.SetPropertyBlock(mtpblock);*/
    }
    public void InteractBtn()
    {
        vineta++;
        if (vineta > 5)
            vineta = 0;

        switch (vineta)
        {
            case 1: //Roxy dance
                props[1].SetActive(true);
                props[0].SetActive(false);
                pool.Spawn("particulin", Vector3.zero, Quaternion.identity);

                meshes[0].material.SetFloat("_tVal", 0);
                meshes[0].material.DOFloat(1, "_tVal", 1f);
                meshes[1].material.SetFloat("_tVal", 0);
                meshes[1].material.DOFloat(1, "_tVal", 1f);
                break;
            case 2: //Henry Dance
                props[2].SetActive(true);
                pool.Spawn("particulin", props[2].transform.position, Quaternion.identity);

                meshes[2].material.SetFloat("_tVal", 0);
                meshes[2].material.DOFloat(1, "_tVal", 1f);
                meshes[3].material.SetFloat("_tVal", 0);
                meshes[3].material.DOFloat(1, "_tVal", 1f);
                break;
            case 3: //Roxy Serious
                props[3].SetActive(true);
                props[1].SetActive(false);
                props[2].SetActive(false);
                pool.Spawn("particulin", Vector3.zero, Quaternion.identity);

                meshes[4].material.SetFloat("_tVal", 0);
                meshes[4].material.DOFloat(1, "_tVal", 1f);
                break;
            case 4: //Henry
                props[4].SetActive(true);
                props[3].SetActive(false);
                pool.Spawn("particulin", Vector3.zero, Quaternion.identity);

                meshes[5].material.SetFloat("_tVal", 0);
                meshes[5].material.DOFloat(1, "_tVal", 1f);
                break;
            case 5: //Roxy Happy
                props[5].SetActive(true);
                props[4].SetActive(false);
                pool.Spawn("particulin", Vector3.zero, Quaternion.identity);

                meshes[6].material.SetFloat("_tVal", 0);
                meshes[6].material.DOFloat(1, "_tVal", 1f);
                break;

            default:
                //Mensaje
                props[0].SetActive(true);
                props[5].SetActive(false);
                pool.Spawn("particulin", Vector3.zero, Quaternion.identity);
                break;
        }
    }
}
