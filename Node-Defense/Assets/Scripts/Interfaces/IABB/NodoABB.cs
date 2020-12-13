using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoABB
{
    // datos a almacenar, en este caso un Virus
    public Virus info;
    // referencia los nodos izquiero y derecho
    public NodoABB hijoIzq = null;
    public NodoABB hijoDer = null;
}
