namespace IDisposableExample
{
    public class Program
    {
        static void Main()
        {
             var printcsv=new PrintCSV();
            try
            {
              printcsv.printcsv();
            }
            finally
            {
              ((IDisposable)printcsv).Dispose();
            }
        }
    }
    public class PrintQuote : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("I am disposing the print object");
        }
    }
    public class PrintCSV:PrintQuote
    {
        public new void Dispose()
        {
            Console.WriteLine("I am disposing CSv object");
        }
        public void  printcsv()
        {
            Console.WriteLine("Printing csv file");
        }
    }
}