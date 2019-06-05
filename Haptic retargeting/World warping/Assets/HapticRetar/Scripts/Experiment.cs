using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experiment : MonoBehaviour
{

    public GameObject[] cubes;
    public Material activeMat, inactiveMat;
    public GameObject rightHand;

    public GameObject textPanel;

    Renderer[] cubesR;
    Transform rightHandT;
    TextMesh textMesh;
    SceneWarping warpScript;

    bool start = false;

    int activeCube = 0;

    void Start()
    {
        cubesR = new Renderer[4]; 
        
        cubesR[0] = cubes[0].GetComponent<Renderer>();
        cubesR[1] = cubes[1].GetComponent<Renderer>();
        cubesR[2] = cubes[2].GetComponent<Renderer>();
        cubesR[3] = cubes[3].GetComponent<Renderer>();

        cubesR[0].material = activeMat;
        cubesR[1].material = inactiveMat;
        cubesR[2].material = inactiveMat;
        cubesR[3].material = inactiveMat;

        rightHandT = rightHand.transform;

        textMesh = textPanel.GetComponent<TextMesh>();
        textMesh.text = "Please stay calm\nuntil the experiment\nstarts";
        warpScript = this.GetComponent<SceneWarping>();
    }

    // Update is called once per frame
    void Update() {

        if (start) {
            //print((rightHandT.position - cubes[activeCube].transform.position).magnitude);
            if ((rightHandT.position - cubes[activeCube].transform.position).magnitude < 0.15) {
                warpScript.beginWarping();
                activeCube++;
                if (activeCube > 3) {
                    start = false;
                    Start();
                    return;
                }
                foreach (Renderer r in cubesR)
                    r.material = inactiveMat;
                cubesR[activeCube].material = activeMat;
            }
        } else {
            if (Input.GetKeyDown("space")) {
                start = true;
                textMesh.text = "Touch the highlighted\nRED cube\n(Look left or right to find them)";
            }
        }
        
    }
}
