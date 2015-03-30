﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace DocumentDbRepositories.Implementation
{

    public class GroupRepository
    {
        private readonly DocumentClient _client;
        private readonly DocumentCollection _collection;

        public GroupRepository(DocumentClient client, DocumentCollection collection)
        {
            _client = client;
            _collection = collection;
        }
        public Task<ScampResourceGroup> GetGroup(string groupID)
        {
            var groupQuery = from g in _client.CreateDocumentQuery<ScampResourceGroup>(_collection.SelfLink)
                             where g.Id == groupID && g.Type == "group"
                             select g;
            var group = groupQuery.ToList().FirstOrDefault();

            return Task.FromResult(group);
        }

        public Task<ScampResourceGroupWithResources> GetGroupWithResources(string groupID)
        {
            var groupQuery = from g in _client.CreateDocumentQuery<ScampResourceGroupWithResources>(_collection.SelfLink)
                             where g.Id == groupID && g.Type == "group"
                             select g;
            var group = groupQuery.ToList().FirstOrDefault();
            
            if (group == null)
                return Task.FromResult((ScampResourceGroupWithResources)null);

            var resourcesQuery = from r in _client.CreateDocumentQuery<ScampResource>(_collection.SelfLink)
                                 where r.Type == "resource" && r.ResourceGroup.Id == groupID
                                 select r;

            group.Resources = resourcesQuery.ToList();

            return Task.FromResult(group);
        }

        public Task<IEnumerable<ScampResourceGroup>> GetGroups()
        {
            var groups = from u in _client.CreateDocumentQuery<ScampResourceGroup>(_collection.SelfLink)
                         where u.Type == "group"
                         select u;
            var grouplist = groups.ToList();
            return Task.FromResult((IEnumerable<ScampResourceGroup>)grouplist);
        }

        public async Task CreateGroup(ScampResourceGroup newGroup)
        {
            var created = await _client.CreateDocumentAsync(_collection.SelfLink, newGroup);
        }

        public Task AddResource(string groupID)
        {
            //TODO: stuff
            throw new NotImplementedException();
        }

        public Task AddMember(string groupID)
        {
            //TODO: stuff
            throw new NotImplementedException();
        }
        public Task AddAdmin(string groupID)
        {
            //TODO: stuff
            throw new NotImplementedException();
        }
    }
}
