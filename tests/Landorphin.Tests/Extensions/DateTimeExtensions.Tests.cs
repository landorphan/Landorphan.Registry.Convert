namespace Landorphin.Tests
{
   using System;
   using System.Diagnostics.CodeAnalysis;
   using System.Globalization;
   using Etl.Logic;
   using Etl.TestUtilities;
   using FluentAssertions;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   // These tests employ Fluent.Assertions, see https://fluentassertions.com/documentation/

   // ReSharper disable InconsistentNaming

   [TestClass]
   public class When_I_call_ToUtc_On_A_Local_DateTime : ArrangeActAssert
   {
      private DateTime actual;
      private DateTime expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtc();
         var localZone = TimeZoneInfo.Local;
         expected = new DateTime(original.AddTicks(localZone.BaseUtcOffset.Negate().Ticks).Ticks, DateTimeKind.Utc);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Convert_From_Local_To_Utc_Representation()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtc_On_A_Utc_DateTime : ArrangeActAssert
   {
      private DateTime actual;
      private DateTime expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtc();
         expected = original;
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_The_Original_Value()
      {
         actual.Should().Be(expected);
         actual.Should().Be(original);
      }
   }

   [TestClass]
   public class When_I_call_ToUtc_On_A_Unspecified_DateTime : ArrangeActAssert
   {
      private DateTime actual;
      private DateTime expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtc();
         expected = new DateTime(original.Ticks, DateTimeKind.Utc);
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AsIf")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Treat_The_Original_As_If_It_Were_Utc()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToRoundTripString_On_A_Local_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local);
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = original.ToString("o", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Convert_From_Local_To_Utc_Representation()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToRoundTripString_On_A_Utc_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc);
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = original.ToString("o", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_The_Original_Value()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToRoundTripString_On_A_Unspecified_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified);
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = new DateTime(original.Ticks, DateTimeKind.Utc).ToRoundtripString();
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AsIf")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Treat_The_Original_As_If_It_Were_Utc()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Local_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = original.ToString("u", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Convert_From_Local_To_Utc_Representation()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Utc_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = original.ToString("u", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_The_Original_Value()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Unspecified_DateTime : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified);
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = new DateTime(original.Ticks, DateTimeKind.Utc).ToUtcString();
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AsIf")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Treat_The_Original_As_If_It_Were_Utc()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_Convert_A_Utc_DateTime_To_A_DateTimeOffset_and_back_again : ArrangeActAssert
   {
      private DateTime actual;
      private DateTime actualUtc;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc);
      }

      protected override void ActMethod()
      {
         var dto = new DateTimeOffset(original);
         actual = dto.DateTime;
         actualUtc = dto.UtcDateTime;
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      // [Ignore]
      public void It_should_Give_An_Equivalent_Local_Time()
      {
         // this is not kind sensitive, so it passes, which is part of the problem.
         actual.Equals(original).Should().BeTrue();

         // proof of the challenge, DateTimeOffest.DateTime gives a value that is of kind unspecified, which is interpreted as local by
         // ToUniversalTime(), thus the reason for the extension ToUtc().
         // (Cannot use Should().Be extension method, b/c it rejects Unspecified comparison to UTC for the same root cause).
         actual.ToUniversalTime().Equals(original.ToUniversalTime()).Should().BeTrue();
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      [Ignore]
      public void It_should_Give_An_Equivalent_Local_Time_Of_Kind_Local()
      {
         // proof of the challenge, it gives local ticks but the time is of Kind unspecified, which gets treated as local.
         actual.Kind.Should().Be(DateTimeKind.Local);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_ToUtc_Time()
      {
         actual.ToUtc().Should().Be(original);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_UTC_Time()
      {
         actualUtc.Should().Be(original);
         actualUtc.Kind.Should().Be(DateTimeKind.Utc);
      }
   }

   [TestClass]
   public class When_I_Convert_A_Local_DateTime_To_A_DateTimeOffset_and_back_again : ArrangeActAssert
   {
      private DateTime actual;
      private DateTime actualUtc;
      private DateTime original;

      protected override void ArrangeMethod()
      {
         original = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local);
      }

      protected override void ActMethod()
      {
         var dto = new DateTimeOffset(original);

         // TODO: (mwp) consider writing an FX cop rule that disallows DateTimeOffset.DateTime property).
         actual = new DateTime(dto.DateTime.Ticks, DateTimeKind.Local);
         actualUtc = dto.UtcDateTime;
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_Local_Time()
      {
         // this is not kind sensitive, so it passes, part of the problem.
         actual.Equals(original).Should().BeTrue();

         // proof of the challenge, DateTimeOffest.DateTime gives a value that is of kind unspecified, which is interpreted as local by
         // ToUniversalTime(), thus the reason for the extension ToUtc().
         // (Cannot use Should().Be extension method, b/c it rejects Unspecified comparison to UTC for the same root cause).
         actual.ToUniversalTime().Equals(original.ToUniversalTime()).Should().BeTrue();
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_Local_Time_Of_Kind_Local()
      {
         // proof of the challenge, it gives local ticks but the time is of Kind unspecified.
         // Desired Behavior:  actual.Kind.Should().Be(DateTimeKind.Local);
         // Actual Behavior: actual.Kind.Should().Be(DateTimeKind.Unspecified);

         // NOTE:  This is fixed on Windows in .Net Core 2.1, failed on 
         actual.Kind.Should().Be(DateTimeKind.Local);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_ToUtc_Time()
      {
         actual.ToUtc().Should().Be(original.ToUtc());
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_Give_An_Equivalent_UTC_Time()
      {
         // BCL uses tick equality, not value equivalence.
         actualUtc.Equals(original).Should().BeFalse();
         actualUtc.Should().Be(original);
         actualUtc.Kind.Should().Be(DateTimeKind.Utc);
      }
   }
}
