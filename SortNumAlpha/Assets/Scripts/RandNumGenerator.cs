using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandNumGenerator : MonoBehaviour
{

    Random rand = new Random();

    int randNum = rand.Next(0, 100);
}
