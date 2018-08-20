﻿namespace Etl.Logic
{
   using System;
   using System.Globalization;

   /// <summary>
   /// Extension methods for <see cref="DateTime"/>.
   /// </summary>
   public static class DateTimeExtensions
   {
      /// <summary>
      /// Converts the <see cref="DateTime"/> to a round-trip date and time.
      /// </summary>
      /// <param name="value">
      /// The value.
      /// </param>
      /// <returns>
      /// A round-trip format of the value.
      /// </returns>
      public static String ToRoundtripString(this DateTime value)
      {
         var v = value;

         // coerce unspecified to UTC.
         if (value.Kind == DateTimeKind.Unspecified) v = new DateTime(v.Ticks, DateTimeKind.Utc);

         return v.ToString("o", CultureInfo.InvariantCulture);
      }

      /// <summary>
      /// Converts a <see cref="DateTime"/> value to Coordinated Universal Time (UTC).
      /// </summary>
      /// <remarks>
      /// Unlike <see cref="DateTime.ToUniversalTime"/>, this method does not adjust the value of
      /// <see cref="DateTimeKind.Unspecified"/>
      /// values as if they were local.
      /// </remarks>
      /// <exception cref="ArgumentException">
      /// Thrown when one or more arguments have unsupported or illegal values.
      /// </exception>
      /// <param name="value">
      /// The value.
      /// </param>
      /// <returns>
      /// A UTC value.
      /// </returns>
      /// <exception cref="System.ArgumentException">
      /// Thrown when the <see cref="DateTimeKind"/> of value is not recognized.
      /// </exception>
      public static DateTime ToUtc(this DateTime value)
      {
         switch (value.Kind)
         {
            case DateTimeKind.Utc:
               return value;

            case DateTimeKind.Local:
               return value.ToUniversalTime();

            case DateTimeKind.Unspecified:
               return DateTime.SpecifyKind(value, DateTimeKind.Utc);

            default:

               // .Net 4.5 throws on invalid DateTimeKind values so this code is unreachable on that platform.  
               throw new ArgumentException(
                  String.Format(CultureInfo.InvariantCulture, "Unrecognized DateTime.Kind ({0}).", (Int32) value.Kind),
                  nameof(value));
         }
      }

      /// <summary>
      /// Converts the <see cref="DateTime"/> Object to UTC and outputs it using the format <b> yyyy-MM-dd HH:mm:ssZ </b>.
      /// </summary>
      /// <param name="value">
      /// The value.
      /// </param>
      /// <returns>
      /// A UTC string representation of the value.
      /// </returns>
      public static String ToUtcString(this DateTime value)
      {
         var v = value;

         // coerce unspecified to UTC.
         if (value.Kind == DateTimeKind.Unspecified) v = new DateTime(v.Ticks, DateTimeKind.Utc);

         return v.ToString("u", CultureInfo.InvariantCulture);
      }
   }
}
