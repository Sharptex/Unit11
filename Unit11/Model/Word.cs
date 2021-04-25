namespace Unit11.Model
{
    public class Word
    {
        public string English { get; set; }

        public string Russian { get; set; }

        public string Theme { get; set; }

        public override string ToString()
        {
            return $"{Russian} {English} {Theme}";
        }
    }
}