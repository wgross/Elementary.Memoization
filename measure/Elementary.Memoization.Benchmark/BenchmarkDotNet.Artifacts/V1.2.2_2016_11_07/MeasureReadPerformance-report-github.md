```ini

Host Process Environment Information:
BenchmarkDotNet.Core=v0.9.9.0
OS=Windows
Processor=?, ProcessorCount=8
Frequency=2825537 ticks, Resolution=353.9150 ns, Timer=TSC
CLR=CORE, Arch=64-bit ? [RyuJIT]
GC=Concurrent Workstation
dotnet cli version: 1.0.0-preview2-003131

Type=MeasureReadPerformance  Mode=Throughput  

```
                   Method |     Median |    StdDev |
------------------------- |----------- |---------- |
        ReadWithTuplesMin |  1.4612 ms | 0.0384 ms |
        ReadWithTuplesMax | 71.1881 ms | 7.0297 ms |
 ReadWithCurriedParamsMin |  1.4213 ms | 0.0168 ms |
 ReadWithCurriedParamsMax | 64.4450 ms | 1.4432 ms |
