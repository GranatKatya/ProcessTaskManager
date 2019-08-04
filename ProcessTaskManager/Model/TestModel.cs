using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism;

using Prism.Mvvm;


namespace ProcessTaskManager.Model
{
    class TestModel : BindableBase
    {
        private List<Keyvalue> _DataList;
        public List<Keyvalue> DataList { get { return _DataList; } set { SetProperty(ref _DataList, value); } }
    }
    class Keyvalue : BindableBase
    {
        private string _Key;
        public string Key { get { return _Key; } set { SetProperty(ref _Key, value); } }

        private ulong _Value;
        public ulong Value { get { return _Value; } set { SetProperty(ref _Value, value); } }
    }


}
