using BenchmarkDotNet.Attributes;
using Elementary.Memoization;
using System;
using System.Linq;

namespace Elementary.Memoization.Benchmark
{ 
    public class MeasureReadPerformance 
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
                this.parameterValues = Enumerable.Range(1, numberOfOperations+6).ToArray();

                this.memoizedWithTupelsMax = new MemoizationBuilder().MapFromParameterTuples()
                    .StoreInDictionaryWithStrongReferences()
                    .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculationMax);

                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMax(
                            this.parameterValues[i],
                            this.parameterValues[i + 1],
                            this.parameterValues[i + 2],
                            this.parameterValues[i + 3],
                            this.parameterValues[i + 4],
                            this.parameterValues[i + 5],
                            this.parameterValues[i + 6]);

                this.memoizedWithTupelsMin = new MemoizationBuilder().MapFromParameterTuples()
                    .StoreInDictionaryWithStrongReferences()
                    .From<int, int>(this.ExpensiveCalculationMin);

                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMin(this.parameterValues[i]);

                this.memoizedWithCurriedParamsMax = new MemoizationBuilder().MapFromCurriedParameters()
                    .StoreInDictionaryWithStrongReferences()
                    .From<int, int, int, int, int, int, int, int>(this.ExpensiveCalculationMax);

                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMax(
                            this.parameterValues[i],
                            this.parameterValues[i + 1],
                            this.parameterValues[i + 2],
                            this.parameterValues[i + 3],
                            this.parameterValues[i + 4],
                            this.parameterValues[i + 5],
                            this.parameterValues[i + 6]);

                this.memoizedWithCurriedParamsMin = new MemoizationBuilder().MapFromCurriedParameters()
                    .StoreInDictionaryWithStrongReferences()
                    .From<int, int>(this.ExpensiveCalculationMin);

                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMin(this.parameterValues[i]);
            
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
            public void ReadWithTuplesMin()
            {
                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMin(this.parameterValues[i]);
            }

            [Benchmark]
            public void ReadWithTuplesMax()
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
            public void ReadWithCurriedParamsMin()
            {
                for (int i = 0; i < numberOfOperations; i++)
                    this.memoizedWithTupelsMin(
                            this.parameterValues[i]);
            }

            [Benchmark]
            public void ReadWithCurriedParamsMax()
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
