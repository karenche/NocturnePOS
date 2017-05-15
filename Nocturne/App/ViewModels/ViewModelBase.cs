using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Nocturne.App.Helpers;
using System.Linq;
using System;
using Nocturne.BL.Helpers;

namespace Nocturne.App.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IValidatable
    {
        public ViewModelBase()
        {
            PropertyChanged += ViewModelBase_PropertyChanged;
        }

        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RemoveValidationMessages(e.PropertyName);
        }

        #region IValidatable

        public const string GenericError = "GenericError";

        private readonly Dictionary<string, IValidationMessage[]> _validationMessages = new Dictionary<string, IValidationMessage[]>();
        public Dictionary<string, IValidationMessage[]> ValidationMessages
        {
            get { return _validationMessages; }
        }

        public bool HasValidationMessageType<T>(string property = null)
        {
            if (string.IsNullOrEmpty(property))
            {
                return _validationMessages.Values.Any(collection => collection.Any(msg => msg is T));
            }
            return _validationMessages.ContainsKey(property) && _validationMessages[property].Any(msg => msg is T);
        }

        public void AddValidationMessage(IValidationMessage message, string property = null)
        {
            if (string.IsNullOrEmpty(property)) { return; }
            if (!_validationMessages.ContainsKey(property))
            {
                _validationMessages[property] = new[] { message };
            }
            else if (!_validationMessages[property].Any(msg => msg.Message.Equals(message.Message) || msg == message))
            {
                var messages = _validationMessages[property].ToList();
                messages.Add(message);
                _validationMessages[property] = messages.ToArray();
            }
            NotifyPropertyChanged(nameof(ValidationMessages));
        }

        public void RemoveValidationMessage(string message, string property = null)
        {
            if (string.IsNullOrEmpty(property)) { return; }
            if (!_validationMessages.ContainsKey(property)) { return; }
            var messages = _validationMessages[property].ToList();
            messages.RemoveAll(msg => msg.Message.Equals(message));
            if (messages.Count > 0)
            {
                _validationMessages[property] = messages.ToArray();
            }
            else
            {
                _validationMessages.Remove(property);
            }
            NotifyPropertyChanged(nameof(ValidationMessages));
        }

        public void RemoveValidationMessages(string property = null)
        {
            if (string.IsNullOrEmpty(property)) { return; }
            if (!_validationMessages.ContainsKey(property)) { return; }

            _validationMessages.Remove(property);
            NotifyPropertyChanged(nameof(ValidationMessages));
        }

        public void SetValidationMessages(Dictionary<string, IValidationMessage[]> messages)
        {
            _validationMessages.Clear();
            foreach (var message in messages)
            {
                _validationMessages.Add(message.Key, message.Value);
            }
            NotifyPropertyChanged(nameof(ValidationMessages));
        }

        public IValidationMessage ValidateProperty(Func<string, IValidationMessage> validationDelegate, string failureMessage, string propertyName = null)
        {
            IValidationMessage result = validationDelegate(failureMessage);
            if (result != null)
            {
                AddValidationMessage(result, propertyName);
            }
            else
            {
                RemoveValidationMessage(failureMessage, propertyName);
            }
            return result;
        }

        #endregion IValidatable 

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged           
    }
}
