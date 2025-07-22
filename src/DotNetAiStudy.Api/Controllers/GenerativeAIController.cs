using DotNetAiStudy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAiStudy.Api.Controllers;

[ApiController]
[Route("ai")]
public class GenerativeAIController : ControllerBase
{
    private readonly ChatService _chatService;
    private readonly RecipeService _recipeService;

    public GenerativeAIController(ChatService chatService, RecipeService recipeService)
    {
        _chatService = chatService;
        _recipeService = recipeService;
    }

    [HttpGet("ask-ai")]
    public async Task<IActionResult> GetResponse([FromQuery] string prompt)
    {
        var response = await _chatService.GetResponseAsync(prompt);
        return Ok(response);
    }

    [HttpGet("ask-ai-options")]
    public async Task<IActionResult> GetResponseWithOptions([FromQuery] string prompt)
    {
        var response = await _chatService.GetResponseWithOptionsAsync(prompt);
        return Ok(response);
    }

    [HttpGet("recipe-creator")]
    public async Task<IActionResult> GenerateRecipe(
            [FromQuery] string ingredients,
            [FromQuery] string cuisine = "any",
            [FromQuery] string dietaryRestrictions = "none")
    {
        if (string.IsNullOrWhiteSpace(ingredients))
        {
            return BadRequest("The 'ingredients' parameter is required and cannot be empty.");
        }
        
        var recipe = await _recipeService.GenerateRecipe(ingredients, cuisine, dietaryRestrictions);
        return Ok(recipe);
    }

}