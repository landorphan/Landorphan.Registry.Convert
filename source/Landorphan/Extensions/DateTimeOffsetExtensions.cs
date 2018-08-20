namespace Etl.Logic
{
   using System;
   using System.Globalization;

   /// <summary>
   /// Extension methods for <see cref="DateTimeOffset"/>.
   /// </summary>
   public static class DateTimeOffsetExtensions
   {
      /// <summary>
      /// Converts the <see cref="DateTimeOffset"/> to a round-trip date and time.
      /// </summary>
      /// <param name="value">
      /// The value.
      /// </param>
      /// <returns>
      /// A round-trip format of the value.
      /// </returns>
      public static String ToRoundtripString(this DateTimeOffset value)
      {
         var v = value;

         // DateTimeOffset coerces unspecified to local and always reports Kind as unspecified on DateTime values.
         return v.ToString("o", CultureInfo.InvariantCulture);
      }

      /// <summary>
      /// Converts the <see cref="DateTimeOffset"/> Object to UTC and outputs it using the format <b> yyyy-MM-dd HH:mm:ssZ </b>.
      /// </summary>
      /// <param name="value">
      /// The value.
      /// </param>
      /// <returns>
      /// A UTC string representation of the value.
      /// </returns>
      public static String ToUtcString(this DateTimeOffset value)
      {
         var v = value;

         // DateTimeOffset coerces unspecified to local and always reports Kind as unspecified on DateTime values.
         return v.ToString("u", CultureInfo.InvariantCulture);
      }
   }
}
