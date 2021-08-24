using LinkShortener.BLL.Services;
using LinkShortener.DAL.Interfaces;
using LinkShortener.DAL.Model;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("getAllShortenedLinks")]
    public IEnumerable<string> GetAllShortenedLinks()
    {
        return _linkShortenerService.GetAllShortenedLinks();
    }

    [HttpGet("/{shortenedLink}")]
    public IActionResult RedirectToOriginalLink(string shortenedLink)
    {
        try
        {
            return Redirect(_linkShortenerService.GetOriginalLink(shortenedLink));
        }
        catch (System.NullReferenceException)
        {
            return BadRequest("No such link found");
        }
    }

}
