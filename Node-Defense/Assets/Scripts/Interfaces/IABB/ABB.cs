using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABB : IABB
{
    public NodoABB raiz;

    public int Raiz()
    {
        return raiz.info;
    }

    public bool ArbolVacio()
    {
        return (raiz == null);
    }

    public void InicializarArbol()
    {
        raiz = null;
    }

    public NodoABB HijoDer()
    {
        return raiz.hijoDer;
    }

    public NodoABB HijoIzq()
    {
        return raiz.hijoIzq;
    }

    public void AgregarElem(ref NodoABB raiz, int x)
    {
        if (raiz == null)
        {
            raiz = new NodoABB();
            raiz.info = x;
        }
        else if (raiz.info > x)
        {
            AgregarElem(ref raiz.hijoIzq, x);
        }
        else if (raiz.info < x)
        {
            AgregarElem(ref raiz.hijoDer, x);
        }
    }

    public void EliminarElem(ref NodoABB raiz, int x)
    {
        if (raiz != null)
        {
            if (raiz.info == x && (raiz.hijoIzq == null) && (raiz.hijoDer == null))
            {
                raiz = null;
            }
            else if (raiz.info == x && raiz.hijoIzq != null)
            {
                raiz.info = this.mayor(raiz.hijoIzq);
                EliminarElem(ref raiz.hijoIzq, raiz.info);
            }
            else if (raiz.info == x && raiz.hijoIzq == null)
            {
                raiz.info = this.menor(raiz.hijoDer);
                EliminarElem(ref raiz.hijoDer, raiz.info);
            }
            else if (raiz.info < x)
            {
                EliminarElem(ref raiz.hijoDer, x);
            }
            else
            {
                EliminarElem(ref raiz.hijoIzq, x);
            }
        }
    }

    public int mayor(NodoABB a)
    {
        if (a.hijoDer == null)
        {
            return a.info;
        }
        else
        {
            return mayor(a.hijoDer);
        }
    }

    public int menor(NodoABB a)
    {
        if (a.hijoIzq == null)
        {
            return a.info;
        }
        else
        {
            return menor(a.hijoIzq);
        }
    }

}
