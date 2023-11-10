using BusinessObjects.Models;
using DataAccess;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        public void DeletePublisher(Publisher pub)
      => PublisherDAO.DeletePublisher(pub);

        public Publisher GetPublisherById(int id)
      => PublisherDAO.FindPublisherById(id);

        public List<Publisher> GetPublishers()
       => PublisherDAO.GetPublisher();

        public void SavePublisher(Publisher pub)
      => PublisherDAO.SavePublisher(pub);

        public void UpdatePublisher(Publisher pub)
      => PublisherDAO.UpdatePublisher(pub);
    }
}
