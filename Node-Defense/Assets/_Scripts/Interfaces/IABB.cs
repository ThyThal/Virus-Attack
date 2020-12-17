using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABB 
{
    Virus Raiz();
    NodoABB HijoIzq();
    NodoABB HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(ref NodoABB n, Virus x);
    void EliminarElem(ref NodoABB n, Virus x);
}
