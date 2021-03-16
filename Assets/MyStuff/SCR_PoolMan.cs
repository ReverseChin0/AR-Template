using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SCR_PoolMan : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string etiqueta;
        public GameObject prefab;
        public int cantidad;
        [Tooltip("Marca si el Manager instanciara mas objetos cuando llegue exceda la cantidad de objetos")]
        public bool debeCrecer = false;
        [Tooltip("Marca si el Pool reutilizara objetos aunque esten activos")]
        public bool debeReutilizar = true;
    }

    #region Singleton
    public static SCR_PoolMan instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> diccionarioPool;
    [SerializeField] Transform parent;

    private void Start() {

        diccionarioPool = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool p in pools)
        {
            Queue<GameObject> poolDeObjeto = new Queue<GameObject>();

            for (int i = 0; i < p.cantidad; i++)
            {
                GameObject objeto = Instantiate(p.prefab, parent);
                objeto.SetActive(false);
                poolDeObjeto.Enqueue(objeto);
            }

            diccionarioPool.Add(p.etiqueta, poolDeObjeto);
        }
    }

    public GameObject Spawn(string _etiqueta, Vector3 _position, Quaternion _rotation)
    {
        if (!diccionarioPool.ContainsKey(_etiqueta))
        {
            Debug.LogWarning("El pool " + _etiqueta + " no existe.");
            return null;
        }

        Pool currentPool = pools.Find(p => p.etiqueta == _etiqueta);//obtengo el pool actual

        if (currentPool.debeReutilizar)
        {
            //Si debe reutilizar me debe de valer y solo saco del queue lo que siga y lo activo
            GameObject spawneable = diccionarioPool[_etiqueta].Dequeue();

            spawneable.SetActive(true);
            spawneable.transform.position = _position;
            spawneable.transform.rotation = _rotation;

            diccionarioPool[_etiqueta].Enqueue(spawneable); //Lo volvemos a meter en el queue
            return spawneable;
        }
        else
        {
            //sino checo si debe crecer
            if (currentPool.debeCrecer) 
            {
                GameObject spawneable = null;
                foreach (GameObject obj in diccionarioPool[_etiqueta])//checo si hay alguno apagado
                {
                    if (!obj.activeSelf)
                        spawneable = obj;
                }

                if (spawneable == null) //si no hay objeto hacemos uno nuevo, lo colocamos y lo metemos al queue
                {
                    spawneable = Instantiate(currentPool.prefab);
                    spawneable.SetActive(true);
                    spawneable.transform.position = _position;
                    spawneable.transform.rotation = _rotation;

                    diccionarioPool[_etiqueta].Enqueue(spawneable);
                    return spawneable;
                }
                else
                {
                    spawneable.SetActive(true);
                    spawneable.transform.position = _position;
                    spawneable.transform.rotation = _rotation;

                    diccionarioPool[_etiqueta].Enqueue(spawneable); //Lo volvemos a meter en el queue
                    return spawneable;
                }
            }
            //si no debe crecer y no hay objetos inactivos pues no hacemos ni mais
            return null;
        }
        
    }

}
