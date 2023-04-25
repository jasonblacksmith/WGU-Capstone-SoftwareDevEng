namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IUser
    {
        string Name { get; set; }
        string Password { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
    }
}