using System.Threading.Tasks;

namespace Common.Interface.IService
{
    public interface ILuisService
    {
        Task<dynamic> GetIntention(string text);
    }
}
