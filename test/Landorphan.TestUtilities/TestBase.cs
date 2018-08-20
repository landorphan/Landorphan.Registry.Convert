namespace Etl.TestUtilities
{
   using System;
   using System.IO;
   using System.Reflection;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>
   /// Base class for tests that use TestUtilities.
   /// </summary>
   /// <remarks>
   /// MSTest attribute inheritance and interaction is tricky.  Ensure your test classes are executing in the order you expect!!!
   /// In particular,
   ///   TestInitialize attributed members of base classes fire
   ///      BEFORE
   ///   ClassInitialize attributed members of descendant classes.
   ///   TODO: (mwp) confirm this remains true on this toolset
   /// </remarks>
   [TestClass]
   public abstract class TestBase
   {
      private readonly String _originalCurrentDirectory;

      /// <summary>
      /// Initializes a new instance of the <see cref="TestBase"/> class.
      /// </summary>
      protected TestBase()
      {
         var uri = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase));
         _originalCurrentDirectory = uri.LocalPath;
      }

      /// <summary>
      /// Gets or sets the test context.
      /// </summary>
      /// <value>
      /// The test context.
      /// </value>
      public TestContext TestContext { get; protected set; }

      /// <summary>
      /// Code that executes before any of the tests methods in the test class are executed.
      /// </summary>
      /// <remarks>
      /// Note:  ClassInitializeAttribute is not inheritable.
      /// </remarks>
      [ClassInitialize]
      public static void TestClassInitialize(TestContext context)
      {
         // currently empty
      }

      /// <summary>
      /// Steps that are run before each test.
      /// </summary>
      [TestInitialize]
      public void TestInitialize()
      {
         InitializeTestMethod();
      }

      /// <summary>
      /// Steps that are run after each test.
      /// </summary>
      [TestCleanup]
      public void TestCleanup()
      {
         TeardownTestMethod();
      }

      /// <summary>
      /// Code that executes after all of the test methods in the test class are executed.
      /// </summary>
      [ClassCleanup]
      public static void TestClassCleanup()
      {
      }

      /// <summary>
      /// Called once before each test method invocation.
      /// </summary>
      protected virtual void InitializeTestMethod()
      {
         // ensure the current directory is restored to the original current directory
         var currentDirectory = Directory.GetCurrentDirectory();
         if (!_originalCurrentDirectory.Equals(currentDirectory, StringComparison.OrdinalIgnoreCase))
            Directory.SetCurrentDirectory(_originalCurrentDirectory);
      }

      /// <summary>
      /// Called once after each test method invocation.
      /// </summary>
      protected virtual void TeardownTestMethod()
      {
         // currently empty
      }
   }
}
