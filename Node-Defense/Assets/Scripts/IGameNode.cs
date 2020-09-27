using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameNode 
{
    int TreePosition { get; set; }
    PowerUp PowerUp { get; set; }
}
