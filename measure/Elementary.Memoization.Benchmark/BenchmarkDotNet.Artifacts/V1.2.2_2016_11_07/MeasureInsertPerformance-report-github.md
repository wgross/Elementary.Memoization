```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2825537 ticks, Resolution=353.9150 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003131

Type=MeasureInsertPerformance  Mode=Throughput  

```
                     Method |     Median |    StdDev |
--------------------------- |----------- |---------- |
        InsertWithTuplesMin |  1.4652 ms | 0.0063 ms |
        InsertWithTuplesMax | 72.9065 ms | 1.5345 ms |
 InsertWithCurriedParamsMin |  1.4641 ms | 0.0067 ms |
 InsertWithCurriedParamsMax | 72.6655 ms | 1.6693 ms |
