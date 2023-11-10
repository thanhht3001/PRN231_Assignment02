using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IPublisherRepository
    {
        void SavePublisher(Publisher pub);
        Publisher GetPublisherById(int id);
        List<Publisher> GetPublishers();
        void UpdatePublisher(Publisher pub);
        void DeletePublisher(Publisher pub);
    }
}
