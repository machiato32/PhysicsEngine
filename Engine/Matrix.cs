using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Matrix
    {
        double[,] matrix;
        internal int NumR { get; private set; }
        internal int NumC { get; private set; }
        public double this[int x, int y]
        {
            get { return matrix[x, y]; }
        }
        public Matrix(double[,] matrix)
        {
            NumR = matrix.GetLength(0);
            NumC = matrix.GetLength(1);

            this.matrix = new double[NumR, NumC];
            for(int i=0; i<NumR; i++)
            {
                for(int j=0; j<NumC; j++)
                {
                    this.matrix[i, j] = matrix[i,j];
                }
            }
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if(a.NumC==b.NumC && a.NumR == b.NumR)
            {
                double[,] newMatrix = new double[a.NumR, a.NumC];
                for (int i = 0; i < a.NumR; i++)
                {
                    for (int j = 0; j < a.NumC; j++)
                    {
                        newMatrix[i, j] = a[i, j] + b[i, j];
                    }
                }
                return new Matrix(newMatrix);
            }
            throw new ArgumentNullException();
        }
        public static Matrix operator -(Matrix a)
        {
            return (-1) * a;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + (-b);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.NumC == b.NumR)
            {
                double[,] newMatrix = new double[a.NumR, b.NumC];
                for (int i = 0; i < a.NumR; i++)
                {
                    for (int j = 0; j <b.NumC; j++)
                    {
                        double element = 0;
                        for(int k=0; k<a.NumC; k++)
                        {
                            element += a[i, k] * b[k, j];
                        }
                        newMatrix[i, j] = element;
                    }
                }
                return new Matrix(newMatrix);
            }
            throw new ArgumentNullException();
        }
        public static Matrix operator *(double d, Matrix a)
        {
            double[,] newMatrix = new double[a.NumR, a.NumC];
            for (int i = 0; i < a.NumR; i++)
            {
                for (int j = 0; j < a.NumC; j++)
                {
                    newMatrix[i, j] = a[i, j] * d;
                }
            }
            return new Matrix(newMatrix);
        }
        public static Matrix operator *(Matrix a, double d)
        {
            return d * a;
        }
        public static Matrix operator /(Matrix a, double d)
        {
            return (1 / d) * a;
        }
        public static Vector operator *(Matrix a, Vector v)
        {
            Matrix newMatrix = a * new Matrix(new double[,] { { v.X }, { v.Y } });
            return new Vector(newMatrix[0, 0], newMatrix[1, 0]);
        }
        public static Matrix operator *(Vector v, Matrix a)
        {
            return new Matrix(new double[,] { { v.X }, { v.Y } }) * a;
        }

        public static Matrix Identity(int dim)
        {
            double[,] identity = new double[dim, dim];
            for(int i = 0; i<dim; i++)
            {
                for(int j =0; j < dim; j++)
                {
                    identity[i, j] = (i == j) ? 1 : 0;
                }
            }
            return new Matrix(identity);
        }

        public static Vector Rotate(double angle, Vector v)
        {
            Matrix m = new Matrix(new double[,] { { Math.Cos(angle), -Math.Sin(angle) }, { Math.Sin(angle), Math.Cos(angle) } });

            return m * v;
        }
        public double Det()
        {
            if (NumC == NumR)
            {
                if (NumC == 2)
                {
                    return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                }
                double det = 0;
                for(int i=0; i<NumC; i++)
                {
                    double[,] newMatrix = new double[NumC - 1, NumC - 1];
                    for (int j = 0; j < NumC - 1; j++) 
                    {
                        for (int k = 0; k < NumC - 1; k++)
                        {
                            if (j < i)
                            {
                                newMatrix[k, j] = matrix[k + 1, j];
                            }
                            else
                            {
                                newMatrix[k, j] = matrix[k + 1, j + 1];
                            }
                        }
                    }
                    Matrix matrixNew = new Matrix(newMatrix);
                    det += matrix[0, i] * matrixNew.Det() * ((i % 2 == 0) ? 1 : -1);
                }
                return det;
            }
            throw new ArgumentException();
        }

    }
}
