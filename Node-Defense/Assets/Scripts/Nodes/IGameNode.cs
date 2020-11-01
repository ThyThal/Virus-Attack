using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameNode 
{
    int Vertex { get; set; }
    PowerUp PowerUp { get; set; }
}
