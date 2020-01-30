﻿using AutoMapper;
using CoreDX.Application.Service.IdentityServer.Helpers;
using CoreDX.Common.Util.TypeExtensions;
using CoreDX.Domain.Entity.App.IdentityServer;
using CoreDX.Domain.Repository.App.IdentityServer;
using CoreDX.Domain.Service.App.IdentityServer;
using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using X.PagedList;

namespace CoreDX.Application.Service.IdentityServer
{
    public class IdentityResourceService : IIdentityResourceService
    {
        protected readonly IIdentityResourceRepository IdentityResourceRepository;
        protected readonly IMapper _mapper;

        public IdentityResourceService(IIdentityResourceRepository identityResourceRepository, IMapper mapper)
        {
            IdentityResourceRepository = identityResourceRepository;
            _mapper = mapper;
        }

        public virtual async Task<IPagedList<IdentityResourceDto>> GetIdentityResourcesAsync(string search, int page = 1, int pageSize = 10)
        {
            var data = await IdentityResourceRepository.GetIdentityResourcesAsync(search, page, pageSize);

            return data.Select(x => _mapper.Map<IdentityResourceDto>(x));
        }

        public virtual async Task<IdentityResourceDto> GetIdentityResourceAsync(int identityResourceId)
        {
            var identityResource = await IdentityResourceRepository.GetIdentityResourceAsync(identityResourceId);
            if (identityResource == null) throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist.", identityResourceId));

            return _mapper.Map<IdentityResourceDto>(identityResource);
        }

        public virtual async Task<IdentityResourcePropertiesDto> GetIdentityResourcePropertiesAsync(int identityResourceId, int page = 1, int pageSize = 10)
        {
            var identityResource = await IdentityResourceRepository.GetIdentityResourceAsync(identityResourceId);
            if (identityResource == null) throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist.", identityResourceId));

            var pagedList = await IdentityResourceRepository.GetIdentityResourcePropertiesAsync(identityResourceId, page, pageSize);
            var identityResourcePropertiesAsync = _mapper.Map<IdentityResourcePropertiesDto>(pagedList);;
            identityResourcePropertiesAsync.IdentityResourceId = identityResourceId;
            identityResourcePropertiesAsync.IdentityResourceName = identityResource.Name;

            return identityResourcePropertiesAsync;
        }

        public virtual async Task<IdentityResourcePropertiesDto> GetIdentityResourcePropertyAsync(int identityResourcePropertyId)
        {
            var identityResourceProperty = await IdentityResourceRepository.GetIdentityResourcePropertyAsync(identityResourcePropertyId);
            //if (identityResourceProperty == null) throw new Exception(string.Format(IdentityResourceServiceResources.IdentityResourcePropertyDoesNotExist().Description, identityResourcePropertyId));
            if (identityResourceProperty == null) throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist."));

            var identityResource = await IdentityResourceRepository.GetIdentityResourceAsync(identityResourceProperty.IdentityResourceId);

            var identityResourcePropertiesDto = _mapper.Map<IdentityResourcePropertiesDto>(identityResourceProperty);
            identityResourcePropertiesDto.IdentityResourceId = identityResourceProperty.IdentityResourceId;
            identityResourcePropertiesDto.IdentityResourceName = identityResource.Name;

            return identityResourcePropertiesDto;
        }

        public virtual async Task<int> AddIdentityResourcePropertyAsync(IdentityResourcePropertiesDto identityResourceProperties)
        {
            var canInsert = await CanInsertIdentityResourcePropertyAsync(identityResourceProperties);
            if (!canInsert)
            {
                await BuildIdentityResourcePropertiesViewModelAsync(identityResourceProperties);
                //throw new Exception(string.Format(IdentityResourceServiceResources.IdentityResourcePropertyExistsValue().Description, identityResourceProperties.Key), IdentityResourceServiceResources.IdentityResourcePropertyExistsKey().Description, identityResourceProperties);
                throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist."));
            }

            var identityResourceProperty = _mapper.Map<IdentityResourceProperty>(identityResourceProperties);

            var added = await IdentityResourceRepository.AddIdentityResourcePropertyAsync(identityResourceProperties.IdentityResourceId, identityResourceProperty);

            return added;
        }

        private async Task BuildIdentityResourcePropertiesViewModelAsync(IdentityResourcePropertiesDto identityResourceProperties)
        {
            var propertiesDto = await GetIdentityResourcePropertiesAsync(identityResourceProperties.IdentityResourceId);
            identityResourceProperties.IdentityResourceProperties.AddRange(propertiesDto.IdentityResourceProperties);
            identityResourceProperties.TotalCount = propertiesDto.TotalCount;
        }

        public virtual async Task<bool> CanInsertIdentityResourcePropertyAsync(IdentityResourcePropertiesDto identityResourcePropertiesDto)
        {
            var resource = _mapper.Map<IdentityResourceProperty>(identityResourcePropertiesDto);

            return await IdentityResourceRepository.CanInsertIdentityResourcePropertyAsync(resource);
        }

        public virtual async Task<int> DeleteIdentityResourcePropertyAsync(IdentityResourcePropertiesDto identityResourceProperty)
        {
            var propertyEntity = _mapper.Map<IdentityResourceProperty>(identityResourceProperty);

            var deleted = await IdentityResourceRepository.DeleteIdentityResourcePropertyAsync(propertyEntity);

            return deleted;
        }

        public virtual async Task<bool> CanInsertIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var resource = _mapper.Map<IdentityResource>(identityResource);

            return await IdentityResourceRepository.CanInsertIdentityResourceAsync(resource);
        }

        public virtual async Task<int> AddIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var canInsert = await CanInsertIdentityResourceAsync(identityResource);
            if (!canInsert)
            {
                //throw new Exception(string.Format(IdentityResourceServiceResources.IdentityResourceExistsValue().Description, identityResource.Name), IdentityResourceServiceResources.IdentityResourceExistsKey().Description, identityResource);
                throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist."));
            }

            var resource = _mapper.Map<IdentityResource>(identityResource);

            var saved = await IdentityResourceRepository.AddIdentityResourceAsync(resource);

            return saved;
        }

        public virtual async Task<int> UpdateIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var canInsert = await CanInsertIdentityResourceAsync(identityResource);
            if (!canInsert)
            {
                //throw new Exception(string.Format(IdentityResourceServiceResources.IdentityResourceExistsValue().Description, identityResource.Name), IdentityResourceServiceResources.IdentityResourceExistsKey().Description, identityResource);
                throw new Exception(string.Format("Identity resource (id : {0}) doesn't exist."));
            }

            var resource = _mapper.Map<IdentityResource>(identityResource);

            var originalIdentityResource = await GetIdentityResourceAsync(resource.Id);

            var updated = await IdentityResourceRepository.UpdateIdentityResourceAsync(resource);

            return updated;
        }

        public virtual async Task<int> DeleteIdentityResourceAsync(IdentityResourceDto identityResource)
        {
            var resource = _mapper.Map<IdentityResource>(identityResource);

            var deleted = await IdentityResourceRepository.DeleteIdentityResourceAsync(resource);

            return deleted;
        }

        public virtual IdentityResourceDto BuildIdentityResourceViewModel(IdentityResourceDto identityResource)
        {
            ComboBoxHelpers.PopulateValuesToList(identityResource.UserClaimsItems, identityResource.UserClaims);

            return identityResource;
        }
    }
}
