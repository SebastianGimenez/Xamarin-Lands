using System;
using System.Collections.Generic;
using System.Text;

namespace Land.Infrastructure
{
    using ViewModels;
    public class InstanceLocator
    {


        private MainViewModel main;

        public MainViewModel Main
        {
            get{ return this.main; }
            set {this.main=value; }
        }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
