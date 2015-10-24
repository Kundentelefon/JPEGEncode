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
            test.testZickZackbyte();
		}

		public void ReverseZickZack(int[]inputList,int maxx, int maxy)
		{
			
		}
		
	}
}

/// <summary>
/// gets 2d array returns zickzack 1d 
/// </summary>
/// <param name="inputArray"></param>
//		public void ZicZack(int[,] inputArray)
//		{
//			int numRow= inputArray.GetLength(0);
//			int numCol = inputArray.GetLength(1);

//			int currentRow = 0;
//			int currentCol = 0;
//			//while (currentRow <= numRow && currentCol <= numCol)
//			//{
//			//	if (currentRow == 1 && ((currentRow + currentCol) % 2) == 0 && currentCol != numCol)
//			//	{

//			//	}
//			//	else if (currentRow == numRow && ((currentRow + currentCol)% 2)!= 0 & currentCol!= numCol)
//			//	{
//			//		out(cur_index) =in(cur_row, cur_col);
//			//		cur_col = cur_col + 1;                          // move right at the bottom
//			//		cur_index = cur_index + 1;

//			//	}
//			//}
//		}
//		/*
//		while cur_row<=num_rows & cur_col<=num_cols
//	if cur_row==1 & mod(cur_row+cur_col,2)==0 & cur_col~=num_cols
//		out(cur_index)=in(cur_row,cur_col);
//		cur_col=cur_col+1;							%move right at the top
//		cur_index=cur_index+1;

//	elseif cur_row==num_rows & mod(cur_row+cur_col,2)~=0 & cur_col~=num_cols
//		out(cur_index)=in(cur_row,cur_col);
//		cur_col=cur_col+1;							%move right at the bottom
//		cur_index=cur_index+1;



//	elseif cur_col==1 & mod(cur_row+cur_col,2)~=0 & cur_row~=num_rows
//		out(cur_index)=in(cur_row,cur_col);
//		cur_row=cur_row+1;							%move down at the left
//		cur_index=cur_index+1;

//	elseif cur_col==num_cols & mod(cur_row+cur_col,2)==0 & cur_row~=num_rows
//		out(cur_index)=in(cur_row,cur_col);
//		cur_row=cur_row+1;							%move down at the right
//		cur_index=cur_index+1;

//	elseif cur_col~=1 & cur_row~=num_rows & mod(cur_row+cur_col,2)~=0
//		out(cur_index)=in(cur_row,cur_col);
//		cur_row=cur_row+1;		cur_col=cur_col-1;	%move diagonally left down
//		cur_index=cur_index+1;

//	elseif cur_row~=1 & cur_col~=num_cols & mod(cur_row+cur_col,2)==0
//		out(cur_index)=in(cur_row,cur_col);
//		cur_row=cur_row-1;		cur_col=cur_col+1;	%move diagonally right up
//		cur_index=cur_index+1;

//	elseif cur_row==num_rows & cur_col==num_cols	%obtain the bottom right element
//		out(end)=in(end);							%end of the operation
//		break										%terminate the operation
//	end
//end
//*/
//        using MathNet.Numerics.LinearAlgebra;
//using MathNet.Numerics.LinearAlgebra.Double;

//Matrix<double> A = DenseMatrix.OfArray(new double[,] {
//        {1,1,1,1},
//        {1,2,3,4},
//        {4,3,2,1}});
//    Vector<double>[] nullspace = A.Kernel();