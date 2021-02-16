using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlService
{
    public class MessageService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string TotalMessage
        {
            get
            {
                if (MessageHeader != string.Empty)
                { return $"{MessageHeader}\n{MessageBody}"; }
                else
                {
                    return MessageBody;
                }
            }
        }
        private string MessageBody { get; set; }
        private string MessageHeader { get; set; }

        public void AddLineToMessageBody(string message)
        {
            if (this.MessageBody != null || this.MessageBody != String.Empty)
            {
                this.MessageBody += "\n";
            }
            this.MessageBody += message;
            OnPropertyChanged(nameof(TotalMessage));
            //Messagechanged?.Invoke();

        }
        public void SetMessageBody(string message)
        {
            this.MessageBody = message;
            OnPropertyChanged(nameof(TotalMessage));

            //  Messagechanged?.Invoke();
        }
        public void SetMessageHeader(string message)
        {
            this.MessageHeader = message;
            OnPropertyChanged(nameof(TotalMessage));


            //   Messagechanged?.Invoke();

        }

        internal void Clear()
        {
            SetMessageBody(string.Empty);
            SetMessageHeader(string.Empty);
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
