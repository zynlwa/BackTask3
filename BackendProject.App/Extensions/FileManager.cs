namespace BackendProject.App.Extensions
{
    public static class FileManager
    {
        public static string SaveFile(this IFormFile file, string folderPath)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/assets", folderPath, fileName);
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
            return fileName;
        }
        public static bool CheckSize(this IFormFile file, int mb)
        {
             return file.Length< mb * 1024 * 1024;
        }
        public static bool CheckType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static void DeleteFile(string folderPath, string fileName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets", folderPath, fileName);
           if(System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

    }
}
