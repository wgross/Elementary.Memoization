#User Story#

As a developer I don't want a time consuming calculation made twice with the same parameter set. Instead I want to reuse a previous result. 

#Memoization as Solution Idea#

Memoization optimizes execution of programs by caching function results: [Memoization in Wikipedia](http://en.wikipedia.org/wiki/Memoization).

An implementation of memoization in C# can be done quite elegantly by creating a delegate matching the functions declaration but holding as well a dictionary instance mapping tuples of function parameters to function results: 

```
#!c#

Func<int,int,int> memoizedSumFunction = (a,b) => { 
  var cache = new Dictionary<Tuple<int,int>,int>();
  var parameters = Tuple.Create(a,b);

  int result;
  if(cache.TryGetValue(parameters, out result))
    return result;

  result = a+b;
  cache.Add(parameters,result);
  return result
}
```

#Elementary.Memoization as an Implemention#

Elementary.Memoization provides an extensible and reusable implementation of this memoization.

**Usage Example**

Memoize a function with three parameters:

```
#!c#

using Elementary.Memoization;

// remember the calculation result for any combination of parameters 
  
var memoized = new MemoizationBuilder()
                .MapFromParameterTuples()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, string>(this.compute);

// use the memoized delegate instead of calling the function directly
  
var result1 = memoized(1, 2, 3); // first call calculates

var result2 = memoized(1, 2, 3); // second call is answered with the cached result

```

## A Second Building Strategy ##

In addition to the widely known implementation of memoization using a single dictionary with Tuples of call parameters as a key, Elementary.Memoization provides a second building strategy for the result cache: 
This second approach uses [Currying](http://en.wikipedia.org/wiki/Currying). Instead of a single dictionary mapping the complete parameter set to a comouted value a tree of dictionaries is created: At the root level the first parameter value is mapped to a delegate, which maps the second parameter value to a delegate, which maps the third parameter to a delegate and so on... 

The currying approach is a bit faster for lookup but more costly during building. If a lot of cache hits are expected (access to already commuted values happen more often then new calculations) the curried approach will be faster then the tupe-mappingl-approach. But in any case you should measure the performance difference before settling with the currying-approach.

**Usage Example**

Memoize again a function with three parameters:

```
#!c#

using Elementary.Memoization;

// remember the calculation result for any combination of parameters 
  
var memoized = new MemoizationBuilder()
                .MapFromCurriedParameters()
                .StoreInDictionaryWithStrongReferences()
                .From<int, int, int, string>(this.compute);

// use the memoized delegate instead of calling the function directly
  
var result1 = memoized(1, 2, 3); // first call calculates

var result2 = memoized(1, 2, 3); // second call is answered with the cached result

```

# Installation #

A Nuget package of the current version is provided at [Nuget.org](https://www.nuget.org/packages/Elementary.Memoization/). 
The package contains a portable class library supporting most of the current patforms and a library for Silverlight 5 without the dependency to *System.Collections.Concurrent*.

To install from Visual Studio Powershell:

```
#!PS#

Install-Package Elementary.Memoization 
```