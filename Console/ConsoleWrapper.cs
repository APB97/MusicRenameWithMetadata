namespace Console
{
    public class ConsoleWrapper
    {
        private bool Silent { get; set; }

        public void WriteLine()
        {
            if (Silent) return;
            System.Console.WriteLine();
        }

        public void WriteLine(string text)
        {
            if (Silent) return;
            System.Console.WriteLine(text);
        }

        public void Clear()
        {
            System.Console.Clear();
        }

        public void WriteLine(object value)
        {
            if (Silent) return;
            System.Console.WriteLine(value);
        }
        
        public void BeSilent()
        {
            Silent = true;
        }

        public void DontBeSilent()
        {
            Silent = false;
        }
    }
}