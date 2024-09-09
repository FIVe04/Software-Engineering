using System;

// 17.	Кроме двумерного массива A дан массив C1, C2, …, Cn. Если для всех элементов Ci выполняется 
// неравенство Ci > Aii, заменить значение каждого элемента Ci значением минимального элемента i-ой строки
//     массива A.

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the size of the matrix: ");
        int n = Convert.ToInt32(Console.ReadLine());
        if (n > 0)
        {
            double[,] A = GetMatrix(n);
            double[] C = GetVector(n);
            Console.WriteLine("Matrix: ");
            PrintMatrix(A);
            Console.WriteLine("Vector: ");
            PrintVector(C);

            if (IsGreaterThanDiagonal(C, A))
            {
                Console.WriteLine("All of the elements in vector are greater than corresponding diagonal element of the matrix!");
                ReplaceWithRowMin(C, A);
            }
            else
            {
                Console.WriteLine("Not all of the elements in vector are greater than corresponding diagonal element of the matrix!");
            }
            PrintVector(C);
        }
        else
        {
            Console.WriteLine("Matrix size should be > 0!");
        }
        
    }

    static void PrintMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"{Math.Round(matrix[i,j], 3)} ");
            }
            Console.WriteLine();
        }
    }

    static void PrintVector(double[] vector)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            Console.Write($"{Math.Round(vector[i],3)} ");
        }
        Console.WriteLine();
    }
    static double[,] GetMatrix(int n)
    {
        double [,] arr = new double[n, n];
        for (int i = 0; i < arr.GetLength(0); i++) {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                Console.Write($"Enter A[{i},{j}]: ");
                arr[i, j] = double.Parse(Console.ReadLine());
            }
        }
        return arr;
    }

    static double[] GetVector(int n)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Enter C[{i}]: ");
            vector[i] = double.Parse(Console.ReadLine());
        }
        return vector;
    }

    static double minElementInVector(double[,] matrix, int idx)
    {
        double minEl = matrix[idx,0];
        for (int i = 1; i < matrix.GetLength(1); i++)
        {
            if (matrix[idx,i] < minEl)
            {
                minEl = matrix[idx,i];
            }
        }
        return minEl;
    }

    static bool IsGreaterThanDiagonal(double[] vector, double[,] matrix)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            if (vector[i] < matrix[i, i])
            {
                return false;
            }
        }
        return true;
    }

    static void ReplaceWithRowMin(double[] vector, double[,] matrix)
    {
        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] = minElementInVector(matrix, i);
        }
    }
    
    
}