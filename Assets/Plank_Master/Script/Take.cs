using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.GetComponent<Plank>().SetActiveAndDeactive(false);
    }
}
