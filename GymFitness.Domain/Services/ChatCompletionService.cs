using System.Text;
using System.Text.Json;
using GymFitness.Domain.Models;
using Microsoft.Extensions.Logging;
using GymFitness.Domain.Abstractions.Services;

namespace GymFitness.Domain.Services
{
    public class ChatCompletionService : IChatCompletionService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ChatCompletionService> _logger;
        private readonly OpenAIConfig _config;
        private const string SYSTEM_MESSAGE = @"You are an expert gymnastics and fitness trainer. 
Only answer questions related to gym exercises, workout techniques, and fitness. 
If a question is not related to fitness or gym, politely decline to answer and explain that 
you only provide guidance about gym and fitness-related topics.
Always prioritize safety and proper form in your answers.";

        public ChatCompletionService(IHttpClientFactory httpClientFactory, ILogger<ChatCompletionService> logger, OpenAIConfig config)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _config = config;
        }

        public async Task<string> GetChatCompletionAsync(string question)
        {
            _logger.LogInformation("=== Starting API Call ===");
            _logger.LogInformation($"Base URL: {_config.BaseUrl}");

            var httpClient = _httpClientFactory.CreateClient("ChatGPT");

            ChatCompletionRequest completionRequest = new()
            {
                Model = "gpt-3.5-turbo",
                MaxTokens = 150,
                Messages = [
                    new Message()
                    {
                        Role = "system",
                        Content = SYSTEM_MESSAGE
                    },
                    new Message()
                    {
                        Role = "user",
                        Content = question
                    }
                ]
            };

            using var httpReq = new HttpRequestMessage(HttpMethod.Post, _config.BaseUrl);
            httpReq.Headers.Add("Authorization", $"Bearer {_config.ApiKey}");

            string requestString = JsonSerializer.Serialize(completionRequest);
            httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");

            try 
            {
                using HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq);
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                
                _logger.LogInformation($"Response Status: {httpResponse.StatusCode}");
                _logger.LogInformation($"Response Content: {responseContent}");
                
                httpResponse.EnsureSuccessStatusCode();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var completionResponse = JsonSerializer.Deserialize<ChatCompletionResponse>(responseContent, options);
                
                // Extract the message content
                var messageContent = completionResponse?.Choices?
                    .FirstOrDefault()?.Message?.Content;

                if (string.IsNullOrEmpty(messageContent))
                {
                    _logger.LogWarning("No message content found in the response");
                    return "No response generated";
                }

                _logger.LogInformation($"Extracted message: {messageContent}");
                return messageContent;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error calling OpenAI API: {ex.Message}");
                _logger.LogError($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
} 