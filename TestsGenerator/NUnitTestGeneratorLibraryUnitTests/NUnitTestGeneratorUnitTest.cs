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
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
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
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
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
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
        }

        [TestMethod]
        public void GenerateFromValidCodeWithTwoMethodsAndOnePrivateMethodInsideShouldReturnCorrectTestCaseWithoutPrivateMethodTest()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{ public void getA(); private void IamPrivate(); public void SecondMethod();}" + "}";
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
            Assert.AreEqual(sourceCodeTest, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
        }

        [TestMethod]
        public void GenerateFromValidCodeWithTwoClassesShouldReturnCorrectTestCaseSeparatelyForOneAndAnotherClasses()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{ public void getA(); private void IamPrivate(); public void SecondMethod();}" + "public class B{ public int getB(bool k); public void IamPublic(string s); public void ThirdMethod();}" + "}";
            string sourceCodeTest1 = "using System;\r\n" +
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
            string sourceCodeTest2 = "using System;\r\n" +
                                     "using System.Collection.Generic;\r\n" +
                                     "using System.Linq;\r\n" +
                                     "using System.Text;\r\n" +
                                     "using NUnit.Framework;\r\n" +
                                     "using Test;\r\n" +
                                     "\r\n" +
                                     "namespace Test.Tests\r\n" +
                                     "{\r\n" +
                                     "    [TestFixture]\r\n" +
                                     "    public class B\r\n" +
                                     "    {\r\n" +
                                     "        [Test]\r\n" +
                                     "        public int getBMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "\r\n" +
                                     "        [Test]\r\n" +
                                     "        public void IamPublicMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "\r\n" +
                                     "        [Test]\r\n" +
                                     "        public void ThirdMethodMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "    }\r\n" +
                                     "}";
            Assert.AreEqual(sourceCodeTest1, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
            Assert.AreEqual(sourceCodeTest2, nUnitTestGenerator.Generate(sourceCode)[1].ToString());
        }

        [TestMethod]
        public void GenerateFromValidCodeWithTwoNamespacesShouldReturnCorrectTestCaseSeparatelyForOneAndAnotherNamespaces()
        {
            string sourceCode = "using System;" + "namespace Test" + "{" + "public class A{ public void getA(); private void IamPrivate(); public void SecondMethod();}}" + "namespace TestB { public class B{ public int getB(bool k); public void IamPublic(string s); public void ThirdMethod();}" + "}";
            string sourceCodeTest1 = "using System;\r\n" +
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
            string sourceCodeTest2 = "using System;\r\n" +
                                     "using System.Collection.Generic;\r\n" +
                                     "using System.Linq;\r\n" +
                                     "using System.Text;\r\n" +
                                     "using NUnit.Framework;\r\n" +
                                     "using TestB;\r\n" +
                                     "\r\n" +
                                     "namespace TestB.Tests\r\n" +
                                     "{\r\n" +
                                     "    [TestFixture]\r\n" +
                                     "    public class B\r\n" +
                                     "    {\r\n" +
                                     "        [Test]\r\n" +
                                     "        public int getBMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "\r\n" +
                                     "        [Test]\r\n" +
                                     "        public void IamPublicMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "\r\n" +
                                     "        [Test]\r\n" +
                                     "        public void ThirdMethodMethodTest()\r\n" +
                                     "        {\r\n" +
                                     "            Assert.Fail(\"autogenerated\");\r\n" +
                                     "        }\r\n" +
                                     "    }\r\n" +
                                     "}";
            Assert.AreEqual(sourceCodeTest1, nUnitTestGenerator.Generate(sourceCode)[0].ToString());
            Assert.AreEqual(sourceCodeTest2, nUnitTestGenerator.Generate(sourceCode)[1].ToString());
        }
    }
}
