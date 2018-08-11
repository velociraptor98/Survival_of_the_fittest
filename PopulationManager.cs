using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulationManager : MonoBehaviour {
    public GameObject obj;
    public int popSize = 10;
    List<GameObject> objlist = new List<GameObject>();
    public static float elapsed=0.0f;
    int trial = 10;
    int generation = 1;


    GUIStyle gui = new GUIStyle();
    GameObject Breed(GameObject param1,GameObject param2)
    {
        Vector2 pos = new Vector2(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f));
        GameObject child = Instantiate(obj, pos, Quaternion.identity);
        DNA dna1 = param1.GetComponent<DNA>();
        DNA dna2 = param2.GetComponent<DNA>();
        //swapping DNA data
        if (Random.Range(0, 50) == 25)
        {
            //mutation(1/50 chance of occuring)
            child.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            child.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            child.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            child.GetComponent<DNA>().size = Random.Range(0.7f, 0.9f);
        }
        else
        {
            //50 % chance of either of the 2 parents being selected for r,g,b
            child.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
            child.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
            child.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
            child.GetComponent<DNA>().size = Random.Range(0, 10) < 5 ? dna1.size : dna2.size;
        }
        return child;
    }
    private void BreedPop()
    {
        List<GameObject> newpop = new List<GameObject>();
        List<GameObject> sortedList = objlist.OrderBy(o => o.GetComponent<DNA>().time).ToList();
        objlist.Clear();
        //breeding the fit parents contained in the second half of the array
        for(int i=(int)(sortedList.Count()/2.0f)-1;i<sortedList.Count()-1;i++)
        {
            objlist.Add(Breed(sortedList[i],sortedList[i+1]));
            objlist.Add(Breed(sortedList[i + 1], sortedList[i]));
        }
        for(int i=0;i<sortedList.Count();i++)
        {
            Destroy(sortedList[i]);
        }
        ++generation;
    }
    private void OnGUI()
    {
        gui.fontSize = 20;
        gui.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: "+generation, gui);
        GUI.Label(new Rect(10, 60, 100, 20), "Time: " + elapsed, gui);
    }
    // Use this for initialization  
    void Start () {
        for(int i=0;i<popSize;i++)
        {
            Vector2 pos = new Vector2(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f));
            GameObject inst = Instantiate(obj, pos, Quaternion.identity);
            inst.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            inst.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            inst.GetComponent<DNA>().b = Random.Range(0.0f,1.0f);
            inst.GetComponent<DNA>().size = Random.Range(0.7f, 0.9f);
            objlist.Add(inst);
            print(i);
        }
	}
	
	// Update is called once per frame
	void Update () {
        elapsed += Time.deltaTime;
        if (elapsed > trial)
        {
            BreedPop();
            elapsed = 0;
        }
	}
}
