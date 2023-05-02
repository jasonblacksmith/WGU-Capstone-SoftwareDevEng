namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IUser
    {
        int UserId { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string Name { get; set; }
    }
}