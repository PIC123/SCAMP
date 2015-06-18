﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DocumentDbRepositories
{
    public interface IUserRepository
    {
        Task CreateUser(ScampUser newUser);
        Task<ScampUser> GetUserbyId(string userId);
        Task UpdateUser(ScampUser user);
        Task<bool> CheckNoUsers();

    }

    public interface IGroupRepository
    {
        Task<ScampResourceGroup> GetGroup(string groupID);
        Task<ScampResourceGroupWithResources> GetGroupWithResources(string groupID);
        Task<List<ScampResourceGroupWithResources>> GetGroupsByBudgetOwner(string userId);
        Task<IEnumerable<ScampResourceGroup>> GetGroups();
        Task<IEnumerable<ScampResourceGroup>> GetGroupsByUser(ScampUserReference user);
        Task CreateGroup(ScampResourceGroup newGroup);
        Task UpdateGroup(string groupID, ScampResourceGroup group);
        Task AddUserToGroup(string groupId, string userId);
    }

    public interface IResourceRepository
    {
        Task<ScampResource> GetResource(string resourceId);
        Task<IEnumerable<ScampResource>> GetResources();
        Task<List<ScampResourceReference>> GetResources(List<string> resourceIds);
        Task<IEnumerable<ScampResource>> GetResourcesByOwner(string userId);
        Task CreateResource(ScampResource resource);
        Task<IEnumerable<ScampResource>> GetResourcesByGroup(ScampUserReference user, string groupId);
        Task UpdateResource(ScampResource resource);
        Task DeleteResource(string resourceId);
    }

    public interface ISystemSettingsRepository
    {
        Task<List<ScampUser>> GetSystemAdministrators();
        Task<List<ScampUser>> GetGroupManagers();
        Task<ScampSubscription> GetSubscription(string subscriptionId);
        Task<List<ScampSubscription>> GetSubscriptions();
        Task UpsertSubscription(ScampSubscription updatedSubscription);
        Task DeleteSubscription(ScampSubscription subscription);
        Task<StyleSettings> GetSiteStyleSettings();
    }
}