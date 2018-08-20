namespace Etl.TestUtilities
{
   using System;

   /// <summary>
   /// Categories describing when a test is executed.
   /// </summary>
   public static class TestTiming
   {
      /// <summary>
      /// Check-in test category.
      /// </summary>
      /// <remarks>
      /// Tests that must pass before check-in.
      /// </remarks>
      public const String CheckIn = "Check-In";

      /// <summary>
      /// Manual test category.
      /// </summary>
      /// <remarks>
      /// Tests that are executed manually.
      /// </remarks>
      public const String Manual = "Manual";

      /// <summary>
      /// Check-in test category.
      /// </summary>
      /// <remarks>
      /// Tests that are executed nightly.
      /// </remarks>
      public const String Nightly = "Nightly";
   }
}
