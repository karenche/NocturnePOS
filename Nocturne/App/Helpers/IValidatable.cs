using Nocturne.BL.Helpers;
using System;
using System.Collections.Generic;

namespace Nocturne.App.Helpers
{
    public interface IValidatable
    {
        Dictionary<string, IValidationMessage[]> ValidationMessages { get; }

        void AddValidationMessage(IValidationMessage message, string property = null);
        void RemoveValidationMessage(string message, string property = null);
        void RemoveValidationMessages(string property = null);
        bool HasValidationMessageType<T>(string property = null);
        IValidationMessage ValidateProperty(Func<string, IValidationMessage> validationDelegate, string failureMessage, string propertyName = null);
    }
}
