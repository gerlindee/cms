using CMS.Exception;
using CMS.Models.Author;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;

namespace CMS.Repository
{
    public class AuthorRepository:IAuthorRepository
    {

        public Author Add(Author addedAuthor)
        {
            addedAuthor.Password = Crypto.Hash(addedAuthor.Password);

            try
            {
                using (var context = new DatabaseContext())
                {
                    addedAuthor.Role = context.Roles.FirstOrDefault(t => t.Type == "Member");

                    context.Author.Add(addedAuthor);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return addedAuthor;
        }

        public IList<Author> FindAll()
        {
            IList<Author> authors = new List<Author>();
            try
            {
                using (var context = new DatabaseContext())
                {
                    pcmembers = context.Author.ToList();
                }
            }
            catch (System.Exception e)
            {
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return authors;
        }

        public Author FindByUsername(string username)
        {
            IList<Author> authors = FindAll();

            return authors.SingleOrDefault(x => x.Username == username);
        }
    }
}