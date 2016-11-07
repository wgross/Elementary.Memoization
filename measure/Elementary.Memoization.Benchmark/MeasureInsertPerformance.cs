using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace Elementary.Memoization.Benchmark
{
    public class MeasureInsertPerformance
    {
        private Func<int, int, int, int, int, int, int, int> memoizedWithTupelsMax;
        
        private Func<int, int> memoizedWithTupelsMin;

        private Func<int, int, int, int, int, int, int, int> memoizedWithCurriedParamsMax;

        private Func<int, int> memoizedWithCurriedParamsMin;

        private int[] parameterValues;
        private const int numberOfOperations = 100000;

        [Setup]
        public void SetupAllBenchmarks()
        {
            this.memoizedWithTupelsMax = new MemoizationBuilder().MapFromParameterTuples()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculationMax);

            this.memoizedWithTupelsMin = new MemoizationBuilder().MapFromParameterTuples()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int>(this.ExpensiveCalculationMin);

            this.memoizedWithCurriedParamsMax = new MemoizationBuilder().MapFromCurriedParameters()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculationMax);

            this.memoizedWithCurriedParamsMin = new MemoizationBuilder().MapFromCurriedParameters()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int>(this.ExpensiveCalculationMin);

            this.parameterValues = Enumerable.Range(1, numberOfOperations+6).ToArray();
        }

        private int ExpensiveCalculationMin(int p0)
        {
            // constant exection tyoe by doing nothing
            return 0;
        }

        private int ExpensiveCalculationMax(int p0, int p1, int p2, int p3, int p4, int p5, int p6)
        {
            // constant exection tyoe by doing nothing
            return 0;
        }

        [Benchmark]
        public void InsertWithTuplesMin()
        {
            for (int i = 0; i < numberOfOperations; i++)
                this.memoizedWithTupelsMin(this.parameterValues[i]);
        }

        [Benchmark]
        public void InsertWithTuplesMax()
        {
            for (int i = 0; i < numberOfOperations; i++)
                this.memoizedWithTupelsMax(
                        this.parameterValues[i],
                        this.parameterValues[i + 1],
                        this.parameterValues[i + 2],
                        this.parameterValues[i + 3],
                        this.parameterValues[i + 4],
                        this.parameterValues[i + 5],
                        this.parameterValues[i + 6]);
        }

        [Benchmark]
        public void InsertWithCurriedParamsMin()
        {
            for (int i = 0; i < numberOfOperations; i++)
                this.memoizedWithTupelsMin(
                        this.parameterValues[i]);
        }

        [Benchmark]
        public void InsertWithCurriedParamsMax()
        {
            for (int i = 0; i < numberOfOperations; i++)
                this.memoizedWithTupelsMax(
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