using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace MvvmDialog.Models
{
    public class SaveText
    {
        public SaveTextResponse Save(SaveTextRequest request)
        {
            SaveTextResponse response = null;

            try
            {
                if (!File.Exists(request.FilePath))
                {
                    using (StreamWriter sw = File.CreateText(request.FilePath))
                    {
                        sw.Write(request.InputText);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(request.FilePath, false))
                    {
                        sw.Write(request.InputText);
                    }
                }

                response = new SaveTextResponse()
                {
                    Succeed = true,
                    Message = "File saved successfully."
                };
            }
            catch (Exception ex)
            {
                response = new SaveTextResponse()
                {
                    Succeed = false,
                    Message = ex.Message
                };
            }

            return response;
        }
    }
}
