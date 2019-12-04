# Task
## It is necessary to implement a multi-threaded template code generator for test classes for one of the testing libraries (NUnit, xUnit, MSTest) for the tested classes.

***Input Data**:*
+ *a list of files for classes from which it is necessary to generate test classes*
+ *path to the folder for recording created files*
+ *restrictions on conveyor sections (see below)*

***Output Data**:*
+ *files with test classes*
+ *all generated test classes should be compiled when included in a separate project, in which there is a link to the project with the tested classes*
+ *all generated tests should fail*

*Generation should be performed in **the conveyor mode "producer-consumer"** and consist of three stages:*
1. *parallel loading of source texts into memory (with a limitation of the number of files loaded at a time)*
2. *generation of test classes in multi-threaded mode (with a limit on the maximum number of simultaneously processed tasks)*
3. *parallel recording of results to disk (with a limitation of the number of simultaneously recorded files)*

***When implementing, use async / await and the asynchronous API. To implement the pipeline, you can use the Dataflow API.***


***The main generator method should return Task and not fulfill any expectations inside (blocking calls to task.Wait (), task.Result, etc). You must also use the asynchronous API for I / O.***

*It is necessary to generate one empty test for each public method of the tested class.*

*Example generated file for NUnit:*
````C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MyCode;

namespace MyCode.Tests
{
    [TestFixture]
    public class MyClassTests
    {
        [Test]
        public void FirstMethodTest()
        {
            Assert.Fail("autogenerated");
        }
        
        [Test]
        public void SecondMethodTest()
        {
            Assert.Fail("autogenerated");
        }
        ...
    }
}
````

*For parsing and generating source code, use **Roslyn**.*
