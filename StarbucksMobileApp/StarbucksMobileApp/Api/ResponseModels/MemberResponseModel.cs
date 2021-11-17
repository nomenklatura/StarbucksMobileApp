namespace StarbucksMobileApp.Api.ResponseModels
{
    public class MemberResponseModel : BaseResponseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Balance { get; set; } = 0;
        public int Star { get; set; }
        public string Description { get; set; }
        public bool IsPerson { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
