// 2. Составить программу по объектно-ориентированной методике. В программе должно быть
// не менее двух классов, связанных отношением наследования. Все массивы – динамические.
// Наличие конструктора – обязательно.
// Если первая строка прямоугольной матрицы имеет максимальное количество отрицательных элементов,
// проверить, как изменится среднее арифметическое всей
// матрицы, если заменить все отрицательные элементы матрицы их модулями.

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the size of the matrix: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int m = Convert.ToInt32(Console.ReadLine());
        Solution mtr = new Solution(n,m);
        
        Console.WriteLine("Enter the matrix: ");
        mtr.ReadMatrix();
        Console.WriteLine("Your matrix: ");
        mtr.PrintMatrix();
        
        int ?maxRow = mtr.IndexOfStringWithMaxNumberOfNegativeElements();

        if (maxRow == 0)
        {
            Console.WriteLine("The first row of the matrix contains the maximum number of negative elements.");
            double diff = mtr.GetAverageDifferenceAfterAbsoluteReplacement();
            Console.WriteLine($"The average difference is: {diff}");
        }
        else if (maxRow is null)
        {
            Console.WriteLine($"There are no negative elements");
        }
        else
        {
            Console.WriteLine("The first row of the matrix does not contain the maximum number of negative elements.");
        }
        
    }
}

class BaseMatrix
{
    private double[,] matrix;
    protected BaseMatrix(int rows, int cols)
    {
        if (rows <= 0 || cols <= 0)
        {
            throw new ArgumentOutOfRangeException("rows and cols must be non-negative and sould not be zero.");
        }
        
        matrix = new double[rows, cols];
    }
    
    protected BaseMatrix(BaseMatrix other)
    {
        int rows = other.Length[0];
        int cols = other.Length[1];
        matrix = new double[rows, cols];
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                matrix[r, c] = other[r, c];
            }
        }
    }

    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1))
                throw new IndexOutOfRangeException($"Index out of range: {row}, {col}");
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= matrix.GetLength(0) || col < 0 || col >= matrix.GetLength(1))
                throw new IndexOutOfRangeException($"Index out of range: {row}, {col}");
            matrix[row, col] = value;
        }
    }

    
    public void ReadMatrix()
    {
        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int col = 0; col < matrix.GetLength(1); col++)
            {
                matrix[row, col] = Convert.ToDouble(Console.ReadLine());
            }
        }
    }

    public int[] Length => new int[] { matrix.GetLength(0), matrix.GetLength(1) };

    public void PrintMatrix()
    {
        for (int row = 0; row < this.Length[0]; row++)
        {
            for (int col = 0; col < this.Length[1]; col++)
            {
                Console.Write(this[row, col] + " ");
            }
            Console.WriteLine();
        }
    }
}

class Solution : BaseMatrix
{
    public Solution(int rows, int cols): base(rows, cols) {}
    
    public Solution(BaseMatrix other) : base(other) {}

    private int NumberOfNegativeElements(int row)
    {
        int n = 0;
        for (int col = 0; col < this.Length[1]; col++)
        {
            if (this[row, col] < 0)
            {
                n++;
            }
        }

        return n;
    }

    public int? IndexOfStringWithMaxNumberOfNegativeElements()
    {
        int max_idx = 0;
        int maxNumberOfNegativeElements = this.NumberOfNegativeElements(max_idx);
        for (int row = 0; row < this.Length[0]; row++)
        {
            int tmp = this.NumberOfNegativeElements(max_idx);
            if (tmp > maxNumberOfNegativeElements)
            {
                max_idx = row;
                maxNumberOfNegativeElements = tmp;
            }
        }

        if (maxNumberOfNegativeElements == 0)
        {
            return null;
        }
        return max_idx;
    }

    public double Sum()
    {
        double sum = 0;
        for (int row = 0; row < this.Length[0]; row++)
        {
            for (int col = 0; col < this.Length[1]; col++)
            {
                sum += this[row, col];
            }
        }
        return sum;
    }
    
    public double GetAverage()
    {
        double sum = this.Sum();
        int totalElements = this.Length[0] * this.Length[1];
        return sum / totalElements;
        
    }

    private void ReplaceNegativesWithAbsolute()
    {
        Console.WriteLine("Matrix with absolute negative elements:");
        for (int row = 0; row < this.Length[0]; row++)
        {
            for (int col = 0; col < this.Length[1]; col++)
            {
                this[row, col] = Math.Abs(this[row, col]);
                Console.Write($"{this[row, col]} ");
            }
            Console.WriteLine();
        }
    }

    public double GetAverageDifferenceAfterAbsoluteReplacement()
    {
        double originalAverage = this.GetAverage();

        Solution matrixCopy = new Solution(this);

        matrixCopy.ReplaceNegativesWithAbsolute();
        
        double newAverage = matrixCopy.GetAverage();
        
        Console.WriteLine($"The average value = {originalAverage}");
        Console.WriteLine($"The average value after replacing negative elements with their absolute values = {newAverage}");
        
        return newAverage - originalAverage;
    }

}