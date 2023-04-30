using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class FileCollectionCall : IFileCollectionCalls
    {
        public FileCollection fileCollection;
        public ObservableCollection<FileCollection> fileCollections = new();

        //Creates and adds new Address record to DB
        public async Task<FileCollection> AddFileCollectionAsync(FileCollection fileCollection)
        {
            FileCollection AddFileCollection = fileCollection;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddFileCollection);
                return AddFileCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired Address record from the DB
        public async Task<FileCollection> GetFileCollectionAsync(int pk)
        {
            try
            {
                FileCollection GetFileCollection = await SqLiteDataService.Db.GetAsync<FileCollection>(pk);
                return GetFileCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Addresses int the table
        public async Task<ObservableCollection<FileCollection>> GetFileCollectionsAsync()
        {
            List<FileCollection> FileCollections = await SqLiteDataService.Db.Table<FileCollection>().ToListAsync();
            foreach (FileCollection FileCollection in FileCollections)
            {
                fileCollections.Add(FileCollection);
            }
            return fileCollections;
        }

        //Removes or Deletes the desired Address record from the DB
        public async Task<bool> RemoveFileCollectionAsync(FileCollection fileCollection)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<FileCollection>(fileCollection);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Address in the Address table in the DB
        public async Task<FileCollection> UpdateFileCollectionAsync(FileCollection fileCollection)
        {
            FileCollection UpdateFileCollection = fileCollection;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateFileCollection);
                return UpdateFileCollection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
