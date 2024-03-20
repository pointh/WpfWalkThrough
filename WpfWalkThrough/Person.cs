namespace WpfWalkThrough
{
    public enum Stav { Svobodny, Zenaty, Rozvedeny }

    public class Person
    {

        // public static List<string> StavStr { get; set; }

        public string On { get; set; }

        public Stav Stav { get; set; }

        public string Ona { get; set; }

        public Person(string prijmeni, Stav stav, string manzelka)
        {
            On = prijmeni;
            Stav = stav;    
            Ona = manzelka;
        }

        public override string ToString()
        {
            return $"{On}, {Stav}, {Ona}";
        }
    }
}