namespace Landorphan.Tests
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
   public class When_I_call_ToRoundTripString_On_A_Local_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local));
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = original.ToString("o", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_convert_from_Local_to_Utc_representation()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToRoundTripString_On_A_Utc_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc));
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = original.ToString("o", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_give_the_original_value()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToRoundTripString_On_A_Unspecified_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified));
      }

      protected override void ActMethod()
      {
         actual = original.ToRoundtripString();
         expected = new DateTime(original.Ticks, DateTimeKind.Local).ToRoundtripString();
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AsIf")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_treat_the_original_as_if_it_were_Local()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Local_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local));
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = original.ToString("u", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_convert_from_Local_to_Utc_representation()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Utc_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc));
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = original.ToString("u", CultureInfo.InvariantCulture);
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_give_the_original_value()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_call_ToUtcString_On_A_Unspecified_DateTimeOffset : ArrangeActAssert
   {
      private String actual;
      private String expected;
      private DateTimeOffset original;

      protected override void ArrangeMethod()
      {
         original = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified));
      }

      protected override void ActMethod()
      {
         actual = original.ToUtcString();
         expected = new DateTimeOffset(new DateTime(original.Ticks, DateTimeKind.Local)).ToUtcString();
      }

      [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "AsIf")]
      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_treat_the_original_as_if_it_were_Local()
      {
         actual.Should().Be(expected);
      }
   }

   [TestClass]
   public class When_I_Compare_Equivalent_DateTimeOffsets : ArrangeActAssert
   {
      private DateTimeOffset local;
      private DateTimeOffset unspecified;
      private DateTimeOffset utc;

      protected override void ArrangeMethod()
      {
         var localDT = new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Local);
         local = new DateTimeOffset(localDT);
         unspecified = new DateTimeOffset(new DateTime(1, 2, 3, 4, 5, 6, 7, DateTimeKind.Unspecified));

         // create an equivalent DateTimeOffset from a UTC DateTime.
         var localZone = TimeZoneInfo.Local;
         utc = new DateTimeOffset(new DateTime(localDT.Ticks + localZone.BaseUtcOffset.Negate().Ticks, DateTimeKind.Utc));
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_treat_Local_and_Utc_as_equivalent_even_though_the_string_representations_are_not_equal()
      {
         local.Equals(utc).Should().BeTrue();
         local.Should().Be(utc);
         local.ToRoundtripString().Should().NotBe(utc.ToRoundtripString());
      }

      [TestMethod]
      [TestCategory(TestTiming.CheckIn)]
      public void It_should_treat_Unspecified_and_Utc_as_equivalent_even_though_the_string_representations_are_not_equal()
      {
         // The BCL treats Unspecified the same as local.
         unspecified.Equals(utc).Should().BeTrue();
         unspecified.Should().Be(utc);
         unspecified.ToRoundtripString().Should().NotBe(utc.ToRoundtripString());
      }
   }
}
