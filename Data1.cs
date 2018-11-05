using System.ComponentModel;

namespace TCPtest
{
    class Data1 : INotifyPropertyChanged
    {
        private string status_;
        public string status
        {
            get
            {
                return status_;
            }
            set
            {
                status_ = value;
                NotifyPropertyChanged("status");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
