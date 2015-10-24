using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace JPEG
{
	class Program
	{
		static void Main(string[] args)
		{
			Test test = new Test();
			var testresult=test.testZickZackbyte();
		}        
	}
}

//        using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;

//Matrix<double> A = DenseMatrix.OfArray(new double[,] {
//        {1,1,1,1},
//        {1,2,3,4},
//        {4,3,2,1}});
//    Vector<double>[] nullspace = A.Kernel();