namespace Tuto {
    class Tuto {
        static void Main() {
            var v = new Vector<int>();
            v.Insert(0, 13);
            v.Insert(0, 14);
            v.Add(15);
            v.Remove(14);
            Console.WriteLine(v.ToString());
        }
    }
}