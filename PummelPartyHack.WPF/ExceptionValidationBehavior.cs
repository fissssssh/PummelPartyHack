using Microsoft.Xaml.Behaviors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PummelPartyHack.WPF
{
    internal interface IValidationExceptionHandler
    {
        bool IsValid { get; set; }
    }
    internal class ExceptionValidationBehavior : Behavior<FrameworkElement>
    {
        private int validationExceptionCount = 0;
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AddHandler(Validation.ErrorEvent, new EventHandler<ValidationErrorEventArgs>((sender, e) =>
            {
                if (AssociatedObject.DataContext is IValidationExceptionHandler handler)
                {
                    if (e.Action == ValidationErrorEventAction.Added)
                    {
                        validationExceptionCount++;
                    }
                    else if (e.Action == ValidationErrorEventAction.Removed)
                    {
                        validationExceptionCount--;
                    }
                    handler.IsValid = validationExceptionCount == 0;
                }
            }));
        }
    }
}
