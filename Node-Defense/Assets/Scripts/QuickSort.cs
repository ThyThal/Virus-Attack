using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuickSort
{
	static public int Partition(int[] arr, int left, int right)
	{
		int pivot;
		int aux = (left + right) / 2;   //tomo el valor central del vector
		pivot = arr[aux];



		// en este ciclo debo dejar todos los valores menores al pivot
		// a la izquierda y los mayores a la derecha
		while (true)
		{
			while (arr[left] < pivot)
			{
				left++;
			}
			while (arr[right] > pivot)
			{
				right--;
			}
			if (left < right)
			{
				int temp = arr[right];
				arr[right] = arr[left];
				arr[left] = temp;
			}
			else
			{
				// este es el valor que devuelvo como proxima posicion de
				// la particion en el siguiente paso del algoritmo
				return right;
			}
		}
	}
	
	static public void quickSort(int[] arr, int left, int right)
	{
		int pivot;
		if (left < right)
		{
			pivot = Partition(arr, left, right);
			if (pivot > 1)
			{
				// mitad del lado izquierdo del vector
				quickSort(arr, left, pivot - 1);
			}
			if (pivot + 1 < right)
			{
				// mitad del lado derecho del vector
				quickSort(arr, pivot + 1, right);
			}
		}
	}

}