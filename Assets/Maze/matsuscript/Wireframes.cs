using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wireframes : MonoBehaviour {
    public bool wire;
    private Vector3 position;
    // スクリーン座標をワールド座標に変換した位置座標
    private Vector3 screenToWorldPointPosition;
    // Use this for initialization
    void Start () {
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material.color = new Color(0.4f, 0.9f, 0.4f);
    }
	
	// Update is called once per frame
	void Update () {
        position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);
        // ワールド座標に変換されたマウス座標を代入
        gameObject.transform.position = screenToWorldPointPosition;
    }
    public void wiref()
    {
        while(wire == true) {
            MeshFilter mf = GetComponent<MeshFilter>();
            mf.mesh.SetIndices(mf.mesh.GetIndices(0), MeshTopology.LineStrip, 0);
            MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.material.color = new Color(0.4f, 0.9f, 0.4f);
        }
    }
}
