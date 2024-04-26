using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using GithubIntegration.Facades.Github.Interfaces;

namespace GithubIntegration.Controllers
{

    /// <summary>
    /// Github Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class GitHubController : ControllerBase
    {
        private readonly IGithubFacade _githubFacade;

        /// <summary>
        /// Construction class
        /// </summary>
        public GitHubController(IGithubFacade githubFacade)
        {
            _githubFacade = githubFacade;
        }

        /// <summary>
        /// Request repository list  
        /// </summary>
        /// <returns>
        /// <param name="user"></param>
        /// <param name="quantity"></param>
        /// <param name="sort"></param>
        /// <param name="direction"></param>
        /// <param name="language"></param>
        /// </returns>
        [HttpGet("repositories")]
        public async Task<IActionResult> RepositoryList([Required(AllowEmptyStrings = false)] string user, [Required(AllowEmptyStrings = false)] int quantity, [Required(AllowEmptyStrings = false)] string sort, [Required(AllowEmptyStrings = false)] string direction, [Required(AllowEmptyStrings = true)] string language)
        {
            var value = await _githubFacade.RepositoryList(user, quantity, sort, direction, language);
            return Ok(value);
        }

    }
}
