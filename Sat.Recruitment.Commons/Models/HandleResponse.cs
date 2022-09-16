
namespace Sat.Recruitment.Commons.Models
{
    public class HandleResponse <T>  
        where T : class
    {
        public HandleResponse(T item)
        {
            this.Item = item;
        }
        public T Item { get; }
    }
}
