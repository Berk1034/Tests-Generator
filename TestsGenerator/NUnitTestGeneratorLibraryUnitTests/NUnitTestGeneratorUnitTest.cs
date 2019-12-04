﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnitTestGeneratorLibrary;

namespace NUnitTestGeneratorLibraryUnitTests
{
    [TestClass]
    public class NUnitTestGeneratorUnitTest
    {
        private NUnitTestGenerator nUnitTestGenerator = new NUnitTestGenerator();

        [TestMethod]
        public void GenerateFromAnEmptyStringShouldReturnNull()
        {
            var result = nUnitTestGenerator.Generate("");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GenerateFromNullStringShouldReturnNull()
        {
            var result = nUnitTestGenerator.Generate(null);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GenerateFromWhitespaceCharsShouldReturnNull()
        {
            var result = nUnitTestGenerator.Generate("                              ");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GenerateFromValidCodeShouldReturnNotNull()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{}" + "}";
            Assert.IsNotNull(nUnitTestGenerator.Generate(sourceCode));
        }

        [TestMethod]
        public void GenerateFromValidCodeWithoutMethodInsideShouldReturnCorrectTestCase()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{}" + "}";
            string sourceCodeTest = "using System;\r\n" +
                                    "using System.Collection.Generic;\r\n" +
                                    "using System.Linq;\r\n" +
                                    "using System.Text;\r\n" +
                                    "using NUnit.Framework;\r\n" +
                                    "using Test;\r\n" +
                                    "\r\n" +
                                    "namespace Test.Tests\r\n" +
                                    "{\r\n" +
                                    "    [TestFixture]\r\n" +
                                    "    public class A\r\n" +
                                    "    {\r\n" +
                                    "    }\r\n" +
                                    "}";
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode).ToString());
        }

        [TestMethod]
        public void GenerateFromValidCodeWithOneMethodInsideShouldReturnCorrectTestCase()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{ public void getA();}" + "}";
            string sourceCodeTest = "using System;\r\n" +
                                    "using System.Collection.Generic;\r\n" +
                                    "using System.Linq;\r\n" +
                                    "using System.Text;\r\n" +
                                    "using NUnit.Framework;\r\n" +
                                    "using Test;\r\n" +
                                    "\r\n" +
                                    "namespace Test.Tests\r\n" +
                                    "{\r\n" +
                                    "    [TestFixture]\r\n" +
                                    "    public class A\r\n" +
                                    "    {\r\n" +
                                    "        [Test]\r\n" +
                                    "        public void getAMethodTest()\r\n" +
                                    "        {\r\n" +
                                    "            Assert.Fail(\"autogenerated\");\r\n" +
                                    "        }\r\n" +
                                    "    }\r\n" +
                                    "}";
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode).ToString());
        }

        [TestMethod]
        public void GenerateFromValidCodeWithTwoMethodsInsideShouldReturnCorrectTestCase()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{ public void getA(); public void SecondMethod();}" + "}";
            string sourceCodeTest = "using System;\r\n" +
                                    "using System.Collection.Generic;\r\n" +
                                    "using System.Linq;\r\n" +
                                    "using System.Text;\r\n" +
                                    "using NUnit.Framework;\r\n" +
                                    "using Test;\r\n" +
                                    "\r\n" +
                                    "namespace Test.Tests\r\n" +
                                    "{\r\n" +
                                    "    [TestFixture]\r\n" +
                                    "    public class A\r\n" +
                                    "    {\r\n" +
                                    "        [Test]\r\n" +
                                    "        public void getAMethodTest()\r\n" +
                                    "        {\r\n" +
                                    "            Assert.Fail(\"autogenerated\");\r\n" +
                                    "        }\r\n" +
                                    "\r\n" +
                                    "        [Test]\r\n" +
                                    "        public void SecondMethodMethodTest()\r\n" +
                                    "        {\r\n" +
                                    "            Assert.Fail(\"autogenerated\");\r\n" +
                                    "        }\r\n" +
                                    "    }\r\n" +
                                    "}";
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode).ToString());
        }
    }
}
