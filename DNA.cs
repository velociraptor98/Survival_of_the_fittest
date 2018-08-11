using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {
    public float r;
    public float g;
    public float b;
    public float size;
    bool dead = false;
    public float time = 0.0f;
    SpriteRenderer spr;
    Collider2D col2d;
	// Use this for initialization
	void Start () {
        spr = this.GetComponent<SpriteRenderer>();
        col2d = this.GetComponent<Collider2D>();
        this.transform.localScale = new Vector2(size,size);
        spr.color = new Color(r, g, b);
	}
	// Update is called once per frame
	void Update () {
		
	}
    private void OnMouseDown()
    {
        dead = true;
        time = PopulationManager.elapsed;
        spr.enabled = false;
        col2d.enabled = false;
    }
}
