using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace AuthApi.Services
{
    public class ServiceResponse<T>
    {

        public T? Model { get; set; }
        public List<string> Erros { get; set; }


        public ServiceResponse()
        {
            Model = default;
            Erros = new List<string>();
        }


        public bool Ok
        {
            get
            {
                return Erros.Count == 0;
            }
        }
        public string Mensagem
        {
            get
            {
                if (!Ok)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string erro in Erros)
                    {
                        sb.AppendLine(erro);
                    }
                    return sb.ToString();
                }
                else
                {
                    return "";
                }


            }
        }
    }
}
