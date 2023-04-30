using System;
using System.Collections.Generic;
using System.Linq;
using WGU_Capstone_C868.Services.Interfaces;

namespace WGU_Capstone_C868.Services.Calls
{
    public class FileCalls : IFileCalls
    {

        public Model.File file;
        public ObservableCollection<Model.File> files = new();

        //Creates and adds new File record to DB
        public async Task<Model.File> AddFileAsync(Model.File file)
        {
            Model.File AddFile = file;
            try
            {
                _ = await SqLiteDataService.Db.InsertAsync(AddFile);
                return AddFile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns the desired File record from the DB
        public async Task<Model.File> GetFileAsync(int pk)
        {
            try
            {
                Model.File GetFile = await SqLiteDataService.Db.GetAsync<Model.File>(pk);
                return GetFile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        //Returns an ObservableCollection of all Files int the table
        public async Task<ObservableCollection<Model.File>> GetFilesAsync()
        {
            List<Model.File> Files = await SqLiteDataService.Db.Table<Model.File>().ToListAsync();
            foreach (Model.File File in Files)
            {
                files.Add(File);
            }
            return files;
        }

        //Removes or Deletes the desired Address record from the DB
        public async Task<bool> RemoveFileAsync(Model.File file)
        {
            try
            {
                _ = await SqLiteDataService.Db.DeleteAsync<Model.File>(file.FileId);

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        //Updates the desired Address in the Address table in the DB
        public async Task<Model.File> UpdateFileAsync(Model.File file)
        {
            Model.File UpdateFile = file;
            try
            {
                _ = await SqLiteDataService.Db.UpdateAsync(UpdateFile);
                return UpdateFile;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

    }
}
