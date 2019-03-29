using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneryRandomizer : MonoBehaviour {

    public List<MeshAndWeight> Meshes;
    Mesh currentMesh;

	// Use this for initialization
	void Start ()
    {
        float randomNumber = Random.Range(0,100);
        foreach(MeshAndWeight mesh in Meshes)
        {
            if (randomNumber < mesh.weight)
            {
                currentMesh = mesh.mesh;
            }
        }
        gameObject.GetComponent<MeshFilter>().mesh = currentMesh;
        transform.rotation = Quaternion.Euler(-90,Random.Range(0,360),0);
        transform.localScale = new Vector3(Random.Range(0.8f, 1.1f), Random.Range(0.8f, 1.1f), Random.Range(0.3f, 1.6f));
	}

    [System.Serializable]
    public class MeshAndWeight
    {
        public string name;
        public Mesh mesh;
        public float weight;
    }
}
