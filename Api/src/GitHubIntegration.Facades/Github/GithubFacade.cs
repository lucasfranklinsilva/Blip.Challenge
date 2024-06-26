﻿using System.Threading.Tasks;
using GithubIntegration.Models;
using GithubIntegration.Services.Atlassian.Interface;
using GithubIntegration.Models.Github;
using GithubIntegration.Facades.Github.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GithubIntegration.Facades.Github
{
    public class GithubFacade : IGithubFacade
    {

        private readonly IRequestGithubService _requestGithubService;

        public GithubFacade(IRequestGithubService requestGithubService)
        {
            _requestGithubService = requestGithubService;
        }

        public async Task<IEnumerable<RepositoriesDto>> RepositoryList(string user, int quantity, string sort, string direction, string language)
        {
            string searchquery = ConstantsGithub.SEARCH_USER_REP_QUERY;
            string consult = searchquery.Replace(ConstantsGithub.VALUE_FIELD_SORT, sort);
            consult = consult.Replace(ConstantsGithub.VALUE_FIELD_DIRECTION, direction);

            var repoList = await _requestGithubService.RepositoriesList(user, consult);

            var repoListSized = repoList.Where(r => r.language == language).Take(quantity);

            return repoListSized.Select(x => RepositoriesToDTO(x)); 
        }
        
        internal RepositoriesDto RepositoriesToDTO(Repositories repos)
        {
            if (repos == null) return null;

            return new RepositoriesDto
            {
                name = repos.name,
                description = repos.description,
                language = repos.language,
                avatar = repos.owner.avatar_url
            };
        }


    }
}
