using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWarping : MonoBehaviour
{
    public GameObject head, hand;
    public GameObject table, realCube, virtualCube;
    public GameObject pivot;
    public GameObject world;

    //public GameObject table;

    public Vector3 offsetPos, offsetRot;

    private Vector3 thetaTot, realCubePos, realCubeVec, virtCubeVec;

    private float previousHeadPos, previousHeadAngle;
    private bool warp = false;
    public bool rotation, translation;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (warp) {
            if (rotation)
                worldRotation();
            else if (translation)
                worldTranslation();
        }
        previousHeadPos = head.transform.position.x;
        previousHeadAngle = head.transform.rotation.eulerAngles[1];

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            pivot.transform.position = hand.transform.position + offsetPos;
            Vector3 rotation = new Vector3(hand.transform.eulerAngles.x, hand.transform.eulerAngles.y, hand.transform.eulerAngles.z);
            pivot.transform.eulerAngles = rotation + offsetRot;
            
            Vector3 sceneReference = head.transform.position;

            previousHeadAngle = head.transform.rotation.eulerAngles[1];
            previousHeadPos = head.transform.position.x;

            realCubePos = realCube.transform.position;
        }
    }

    void worldRotation()
    {
        realCubeVec = realCubePos - pivot.transform.position;
        virtCubeVec = virtualCube.transform.position - pivot.transform.position;

        float theta = Mathf.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(realCubeVec, virtCubeVec)));
        theta *= Mathf.Acos(Vector3.Dot(Vector3.Normalize(realCubeVec), Vector3.Normalize(virtCubeVec)));
        //print("Theta = " + theta);

        if (theta < 0.0075)
            return;

        //print("Head : " + head.transform.rotation.eulerAngles[1]);

        float delta = previousHeadAngle - head.transform.rotation.eulerAngles[1];
        if (delta > 340)
        {      // Correction de la singularité angles d'euler
            delta -= 360;
        }
        else if (delta < -340)
        {
            delta += 360;
        }
        //print("Prev Angle = " + previousHeadAngle);
        //print("Angle = " + head.transform.rotation.eulerAngles[1]);
        //print("Delta = " + delta);

        float lambda = 0;
        if (Mathf.Sign(delta) == 1)
            lambda = 0.105f;
        else if (Mathf.Sign(delta) == -1)
            lambda = 0.05f;
        //print("Lambda = " + lambda);

        //print("Rotation angle = " + lambda * delta * Mathf.Sign(theta));
        float rotationResult = Mathf.Min(theta + head.transform.rotation.eulerAngles[1], Mathf.Min(0.01f, -lambda * Mathf.Abs(delta) * Mathf.Sign(theta)));

        world.transform.RotateAround(pivot.transform.position, Vector3.up, rotationResult);

        // Translation

        //world.transform.Translate
    }

    void worldTranslation() {
        realCubeVec = realCubePos - pivot.transform.position;
        virtCubeVec = virtualCube.transform.position - pivot.transform.position;
        //print("HeadX = " + head.transform.position.x);

        float delta = previousHeadPos - head.transform.position.x;
        //print("Delta = " + delta);

        float lambda = 0.25f;

        print(Mathf.Abs(virtCubeVec.x - realCubeVec.x));
        if (Mathf.Abs(virtCubeVec.x - realCubeVec.x) > Mathf.Abs(0.01f)) {
            //world.transform.position += new Vector3(-Mathf.Sign(virtCubeVec.x) * Mathf.Abs(delta) * lambda, 0f, 0f);
            world.transform.Translate(new Vector3(-Mathf.Sign(virtCubeVec.x) * Mathf.Abs(delta) * lambda, 0f, 0f));
            //print(Mathf.Sign(virtCubeVec.x) * Mathf.Abs(delta) * lambda);
        }
        
    }

    public void beginWarping() {
        warp = true;
    }
}
