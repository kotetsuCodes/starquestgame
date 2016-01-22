using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var meshRenderer = GetComponent<MeshRenderer>();
        var material = meshRenderer.material;

        Vector2 offset = material.mainTextureOffset;
        offset.x += Time.deltaTime / 10f;

        material.mainTextureOffset = offset;

    }
}
