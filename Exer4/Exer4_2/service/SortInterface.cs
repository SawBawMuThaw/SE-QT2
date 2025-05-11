using Exer4_2.Models;
using NuGet.Protocol.Core.Types;

namespace Exer4_2.service
{
    public interface SortInterface
    {
        public List<Item> sortBestItems();
        public List<OrderDetail> sortBoughtItems(int AgentId);
    }
}
