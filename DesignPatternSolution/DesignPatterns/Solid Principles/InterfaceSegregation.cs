using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Solid_Principles
{
    public static class InterfaceSegregation
    {
        public static void Run()
        {

        }
    }

    public class Document
    {

    }
    public interface IMultiFunctionDevice:IScan,IPrint
    {
        void fax(Document d);
    }

    public interface IPrint
    {
        void Print(Document d);
    }
    public interface IScan
    {
        void scan(Document d);
    }
    public interface IFax
    {
        void fax(Document d);
    }


    class Photocopier : IPrint, IScan
    {
        public void Print(Document d)
        {
          //
        }

        public void scan(Document d)
        {
           //
        }
    }

    class MultifunctionPrinter : IMultiFunctionDevice
    {
        private IScan scanner ;
        private IPrint printer ;

        public MultifunctionPrinter(IScan scanner, IPrint printer)
        {
            this.scanner = scanner;
            this.printer = printer;
        }

        public void fax(Document d)
        {
            //
        }

        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void scan(Document d)
        {
            scanner.scan(d);
        }
    }

}
