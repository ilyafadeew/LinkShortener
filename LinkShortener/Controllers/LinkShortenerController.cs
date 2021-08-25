using LinkShortener.BLL.Services;
using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace LinkShortener.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LinkShortenerController : ControllerBase
{
    private readonly LinkShortenerService _linkShortenerService;

    public LinkShortenerController(LinkShortenerService linkShortenerService)
    {
        _linkShortenerService = linkShortenerService;
    }

    [HttpPost("addLink")]
    public async Task AddLinkInfoAsync(string originalLink, string shortenedLink)
    {      
        await _linkShortenerService.AddLinkInfoAsync(originalLink, shortenedLink);
    }


    [HttpGet("getMyShortenedLinks")]
    public IEnumerable<string> GetMyShortenedLinks()
    {
        return _linkShortenerService.GetMyShortenedLinks();
    }

    [HttpGet("/{shortenedLink}")]
    public async Task<IActionResult> RedirectToOriginalLink(string shortenedLink)
    {
        string redirectionLink = await _linkShortenerService.GetOriginalLinkOrNullAsync(shortenedLink);

        if (redirectionLink != null)
            return Redirect(redirectionLink);
        else
            return BadRequest("No such link found");
    }

}
