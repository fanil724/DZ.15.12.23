namespace Task3
{
    public class Resume
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public UInt32 Experience { get; set; }
        public UInt32 Salary { get; set; }

        public Resume(string str)
        {
            List<string> s = str.Split(":::").ToList();
            Name = s[0];
            Surname = s[1];
            City = s[2];
            Experience = Convert.ToUInt32(s[3]);
            Salary = Convert.ToUInt32(s[4]);
        }
    }
}
