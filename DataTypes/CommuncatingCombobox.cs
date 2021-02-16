using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace DataTypes
{
    public class CommuncatingCombobox<T> where T : INaamToString
    {
        public Action InputLeftChanged;
        public Action InputRightChanged;
        public Action SourceListChanged;
        private List<T> _sourceListT;
        public List<T> SourceListT
        {
            get => this._sourceListT;
            set
            {
                this._sourceListT = value;
                SourceListChanged?.Invoke();
            }
        }

        public List<string> SourceListString //List voor keuze opties combombox
        {
            get
            {
                List<string> returnList = new List<string>();
                foreach (T itemT in SourceListT)
                {
                    returnList.Add(itemT.NaamToString);
                }
                return returnList;
            }
        }

        private string _inputRight;
        public string InputRight
        {
            get { return _inputRight; }
            set
            {
                if (value != null)
                {
                    _inputRight = value;
                    InputRightChanged?.Invoke();
                }
            }
        }

        private string _inputLeft;
        public string InputLeft
        {
            get { return _inputLeft; }

            set
            {
                if (value != null)
                {
                    _inputLeft = value;
                    InputLeftChanged?.Invoke();
                }
            }
        }

        private List<string> _listLeft;
        public List<string> ListLeft
        {
            get
            {
                return _listLeft;
            }
            set
            {
                _listLeft = value;
            }
        }

        private List<string> _listRight;
        public List<string> ListRight
        {
            get { return _listRight; }
            set { _listRight = value; }
        }

        public CommuncatingCombobox(List<T> sourceList)
        {

            SourceListT = sourceList;
            this.ListLeft = new List<string>();
            foreach (var item in SourceListString)
            {
                ListLeft.Add(item);
            }

            this.ListRight = new List<string>();
            foreach (var item in SourceListString)
            {

                ListRight.Add(item);
            }
        }

        public CommuncatingCombobox<T> ShallowCopy()
        {
            return (CommuncatingCombobox<T>)this.MemberwiseClone();
        }

        public void UpdateListRight(string inputLeft)
        {
            this.ListRight = new List<string>(SourceListString.Where(p => p != inputLeft));
        }

        public void UpdateListLeft(string inputRight)
        {
            this.ListLeft = new List<string>(SourceListString.Where(p => p != inputRight));

        }
        public void ResetInput()
        {
            this._inputLeft = null;
            this._inputRight = null;
            InputRightChanged?.Invoke();
            InputLeftChanged?.Invoke();
        }
    }
}

