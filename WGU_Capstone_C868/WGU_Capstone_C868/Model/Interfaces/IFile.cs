namespace WGU_Capstone_C868.Model.Interfaces
{
    public interface IFile
    {
        int FileCollectionId { get; set; }
        int FileId { get; set; }
        string FilePath { get; set; }
        string FileTitle { get; set; }
        string FileType { get; set; }
    }
}