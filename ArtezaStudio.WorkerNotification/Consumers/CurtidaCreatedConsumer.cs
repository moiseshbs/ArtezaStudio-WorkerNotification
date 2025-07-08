using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ArtezaStudio.WorkerNotification.Models;
using ArtezaStudio.WorkerNotification.Services;
using Confluent.Kafka;

namespace ArtezaStudio.WorkerNotification.Consumers
{
    public class CurtidaCreatedConsumer : BackgroundService
    {
        private readonly EmailService _emailService;
        private readonly ApiService _apiService;

        public CurtidaCreatedConsumer(EmailService emailService, ApiService apiService)
        {
            _emailService = emailService;
            _apiService = apiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "notification-worker",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe("curtida-created");

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);
                var curtidaEvent = JsonSerializer.Deserialize<CurtidaEvent>(result.Message.Value);

                //buscar o email do autor da publicação
                var autorEmail = await _apiService.ObterEmailAutorPorPublicacaoId(curtidaEvent.PublicacaoId);

                if (!string.IsNullOrWhiteSpace(autorEmail))
                {
                    await _emailService.EnviarEmailAsync(autorEmail, "Nova curtida", "Sua publicação recebeu um like!");
                }

                await Task.Delay(100);
            }
        }
    }
}
