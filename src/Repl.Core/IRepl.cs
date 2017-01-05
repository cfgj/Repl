using System.Threading.Tasks;

namespace Repl.Core
{
    public interface IRepl
    {
        Task OnAsync();
    }
}
