using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IABB 
{
    int Raiz();
    NodoABB HijoIzq();
    NodoABB HijoDer();
    bool ArbolVacio();
    void InicializarArbol();
    void AgregarElem(ref NodoABB n, int x);
    void EliminarElem(ref NodoABB n, int x);
}
