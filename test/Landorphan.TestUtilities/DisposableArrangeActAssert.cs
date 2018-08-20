namespace Etl.TestUtilities
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Reflection;
   using System.Threading;
   using Logic;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>
   /// Provides common services for BDD-style (context/specification) unit tests.  Serves as an adapter between the MSTest and  BDD-style tests.
   /// </summary>
   /// <remarks>
   /// Used when <see cref="IDisposable"/> fields are present.
   /// </remarks>
   [TestClass]
   public abstract class DisposableArrangeActAssert : ArrangeActAssert, INotifyingQueryDisposable
   {
      private Int32 _isDisposed;
      private Int32 _isDisposing;

      /// <inheritdoc/>
      [SuppressMessage("Microsoft.Design", "CA1063:ImplementIDisposableCorrectly", Justification = "Altered to be thread-safe(MWP)")]
      public void Dispose()
      {
         if (0 != Interlocked.Exchange(ref _isDisposed, 1)) return;

         if (0 != Interlocked.Exchange(ref _isDisposing, 1)) return;

         Dispose(true);

         // Use SupressFinalize in case a subclass implements a finalizer.
         GC.SuppressFinalize(this);
      }

      /// <summary>
      /// Releases the unmanaged resources used by the <see cref="DisposableArrangeActAssert"/> and optionally releases the managed resources.
      /// </summary>
      /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
      protected virtual void Dispose(Boolean disposing)
      {
         if (disposing)
         {
            // notify all listeners
            OnDisposing();

            // Clean up managed resources if disposing
            ReleaseManagedResources();
         }

         // Clean up native resources always
         ReleaseUnmanagedResources();
      }

      /// <summary>
      /// Throws an <see cref="ObjectDisposedException"/> if this instance has been disposed.
      /// </summary>
      protected void ThrowIfDisposed()
      {
         if (IsDisposed) throw new ObjectDisposedException(GetType().Name);
      }

      /// <summary>
      /// Finds and releases all managed resources.
      /// </summary>
      protected virtual void ReleaseManagedResources()
      {
         var derrivedType = GetType();

         while (derrivedType != typeof(DisposableArrangeActAssert))
         {
            // ReSharper disable once PossibleNullReferenceException
            var fields = derrivedType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
               var value = field.GetValue(this);
               if (value != null)
               {
                  var fieldType = value.GetType();
                  if (fieldType.IsValueType) continue;

                  // collections of IDisposable.
                  var disposables = value as IEnumerable<IDisposable>;
                  if (disposables != null)
                     foreach (var disposable in disposables)
                        disposable?.Dispose();

                  // TODO: (mwp) Add special treatment for IEnumerable<KeyValuePair<TKey, TValue>> because it is a common data structure.
                  // var openType = typeof(IEnumerable<KeyValuePair<,>>);

                  // IDisposable
                  var asDisposable = value as IDisposable;
                  if (asDisposable != null)
                     try
                     {
                        asDisposable.Dispose();
                     }
                     catch (NullReferenceException)
                     {
                        // eat the exception
                        // (this is sometimes seen when there are chains of disposables, ownership is not a well defined concept in .Net.
                     }

                  field.SetValue(this, null);

                  // else ... this disposable field is not owned by this instance.
               }
            }

            derrivedType = derrivedType.BaseType;
         }
      }

      /// <summary>
      /// Releases the unmanaged resources.
      /// </summary>
      protected virtual void ReleaseUnmanagedResources()
      {
      }

      /// <summary>
      /// Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the
      /// <see cref="DisposableArrangeActAssert"/>.
      /// </summary>
      ~DisposableArrangeActAssert()
      {
         Dispose(false);
      }

      /// <inheritdoc/>
      public event EventHandler<EventArgs> Disposing;

      /// <inheritdoc/>
      public Boolean IsDisposed => _isDisposed != 0;

      /// <inheritdoc/>
      public Boolean IsDisposing => _isDisposing != 0;

      /// <summary>
      /// Notifies all listeners that this instance is being disposed.
      /// </summary>
      protected virtual void OnDisposing()
      {
         var handler = Disposing;
         handler?.Invoke(this, EventArgs.Empty);
      }
   }
}
