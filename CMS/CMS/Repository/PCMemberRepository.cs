﻿using CMS.Exception;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Repository
{
    public class PCMemberRepository : IPCMemberRepository
    {

        public PCMember Add(PCMember addedPCMember)
        {
            addedPCMember.Password = Crypto.Hash(addedPCMember.Password);

            try
            {
                using (var context = new DatabaseContext())
                {
                    addedPCMember.Role = context.Roles.FirstOrDefault(t => t.Type == "Member");

                    context.PCMembers.Add(addedPCMember);
                    context.SaveChanges();
                }
            }
            catch (System.Exception e)
            {
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return addedPCMember;
        }

        public IList<PCMember> FindAll()
        {
            IList<PCMember> pcmembers = new List<PCMember>();
            try
            {
                using (var context = new DatabaseContext())
                {
                    pcmembers = context.PCMembers.ToList();
                }
            }
            catch (System.Exception e)
            {
                throw new DatabaseException("Cannot connect to Database!\n");
            }

            return pcmembers;
        }

        public PCMember FindByEmail(string email)
        {
            IList<PCMember> pcmembers = FindAll();

            return pcmembers.SingleOrDefault(x => x.Email == email);
        }
    }
}