using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M07
{
    public class SortMatrix
    {
        /// <summary>
        /// Sort the matrix
        /// </summary>
        public Action<int[,]> Sort;
        /// <summary>
        /// Order type from ready algorithm
        /// </summary>
        public OrderType OrderType { get; private set; }
        /// <summary>
        /// Comparison type from ready algorithm
        /// </summary>
        public ComparisonType ComparisonType { get; private set; }
        /// <summary>
        /// Initializes a new instance of the SortMatrix class that hasn't a method of sort, i.e null
        /// </summary>
        public SortMatrix()
        {
            Sort = null;
        }
        /// <summary>
        /// Initializes a new instance of the SortMatrix class that has a your method of sort
        /// </summary>
        /// <param name="sort">Your method of sort</param>
        public SortMatrix(Action<int[,]> sort)
        {
            Sort = sort;
        }
        /// <summary>
        /// Initializes a new instance of the SortMatrix class that has the specified method of sort
        /// </summary>
        /// <param name="orderType">Order type of sort</param>
        /// <param name="comparisonType">Comparison type of sort</param>
        public SortMatrix(OrderType orderType, ComparisonType comparisonType)
        {
            OrderType = orderType;
            ComparisonType = comparisonType;
            Sort = GetSort(OrderType, ComparisonType);
        }

        private Action<int[,]> GetSort(OrderType orderType, ComparisonType comprasionType)
        {
            switch (orderType)
            {
                case OrderType.Asc:
                    switch (comprasionType)
                    {
                        case ComparisonType.MaxElement:
                            return MethodsOfSort.SortAscMax;
                        case ComparisonType.MinElement:
                            return MethodsOfSort.SortAscMin;
                        case ComparisonType.SumRows:
                            return MethodsOfSort.SortAscSum;
                        default:
                            throw new ArgumentException("Unexpected comparison");

                    }
                case OrderType.Desc:
                    switch (comprasionType)
                    {
                        case ComparisonType.MaxElement:
                            return MethodsOfSort.SortDescMax;
                        case ComparisonType.MinElement:
                            return MethodsOfSort.SortDescMin;
                        case ComparisonType.SumRows:
                            return MethodsOfSort.SortDescSum;
                        default:
                            throw new ArgumentException("Unexpected comprasion");
                    }
                default: throw new ArgumentException("Unexpected order");
            }
        }


    }
}
