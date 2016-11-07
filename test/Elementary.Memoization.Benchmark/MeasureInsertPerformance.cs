using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace Elementary.Memoization.Benchmark
{
    public class MeasureInsertPerformance
    {
        private Func<int, int, int, int, int, int, int, int> memoizedWithTupels;
        private Func<int, int, int, int, int, int, int, int> memoizedWithCurriedParams;
        private int[] parameterValues;

        [Setup]
        public void SetupAllBenchmarks()
        {
            this.memoizedWithTupels = new MemoizationBuilder().MapFromParameterTuples()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculation);

            this.memoizedWithCurriedParams = new MemoizationBuilder().MapFromCurriedParameters()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculation);

            this.parameterValues = Enumerable.Range(1, 10006).ToArray();
        }

        private int ExpensiveCalculation(int p0, int p1, int p2, int p3, int p4, int p5, int p6)
        {
            // constant exection tyoe by doing nothing
            return 0;
        }

        [Benchmark]
        public void InsertWithTuples()
        {
            for (int i = 0; i < 10000; i++)
                this.memoizedWithTupels(
                        this.parameterValues[i],
                        this.parameterValues[i + 1],
                        this.parameterValues[i + 2],
                        this.parameterValues[i + 3],
                        this.parameterValues[i + 4],
                        this.parameterValues[i + 5],
                        this.parameterValues[i + 6]);
        }

        [Benchmark]
        public void InsertWithCurriedParams()
        {
            for (int i = 0; i < 10000; i++)
                this.memoizedWithTupels(
                        this.parameterValues[i],
                        this.parameterValues[i + 1],
                        this.parameterValues[i + 2],
                        this.parameterValues[i + 3],
                        this.parameterValues[i + 4],
                        this.parameterValues[i + 5],
                        this.parameterValues[i + 6]);
        }
    }
}