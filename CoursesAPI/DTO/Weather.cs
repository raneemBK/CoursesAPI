namespace CoursesAPI.DTO
{
    public class Wind
    {
        public string speed { get; set; }
    }
    public class Main
    {
        public string temp { get; set; }
        public string humidity { get; set; }
    }
    public class Weather
    {
        public Wind wind { get; set; }
        public Main Main {  get; set; }
        public int Timezone { get; set; }
        public string Name { get; set; }

    }
}
