using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livis.Market.Models.ViewModel.ComponentViewModels
{
    public class LinkItemCollection : IEnumerable<LinkItem>
    {
        private IEnumerable<LinkItem> _links;
        public LinkItemCollection(IEnumerable<LinkItem> links)
        {
            _links = links;
        }

        public IEnumerator<LinkItem> GetEnumerator()
        {
            foreach (var item in _links)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
