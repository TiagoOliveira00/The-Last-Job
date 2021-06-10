using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfCameras : MonoBehaviour
{
    public GameObject camera1stPerson;
    public GameObject camera3rdPerson;
    //public GameObject object1stPerson;
    //public GameObject PickedObject;
    public int CameraType;
   
    public bool taserOnHand;


    void Start()
    {
        taserOnHand = false;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        if (CameraType == 1)
    //        {
    //            CameraType = 0;
    //        }
    //        else if (taserOnHand)
    //        {
    //            CameraType += 1;
    //        }
            
    //        StartCoroutine(Change());
    //    }
    //}

    //public IEnumerator Change()
    //{
    //    yield return new WaitForSeconds(0.001f);

    //      //3rd pessoa
    //       if (CameraType == 0 )
    //       {
    //          camera1stPerson.SetActive(false);
    //          camera3rdPerson.SetActive(true);
    //         // object1stPerson.SetActive(false);
    //       }
        
        
    //     //1st pessoa
    //     if (CameraType == 1)
    //     {
    //         camera1stPerson.SetActive(true);
    //         camera3rdPerson.SetActive(false);
    //        // object1stPerson.SetActive(true);
    //     }
               
    //}
}
