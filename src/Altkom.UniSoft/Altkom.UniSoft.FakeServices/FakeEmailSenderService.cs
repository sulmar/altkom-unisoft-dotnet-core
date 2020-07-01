using Alktom.UniSoft.IServices;
using Altkom.UniSoft.Models;
using FluentValidation.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.UniSoft.FakeServices
{
    public class FakeEmailSenderService : ISenderService
    {
        private readonly ILogger<FakeEmailSenderService> logger;

        public FakeEmailSenderService(ILogger<FakeEmailSenderService> logger)
        {
            this.logger = logger;
        }

        public void Send(Customer customer)
        {
            logger.LogInformation($"Sending email to {customer.FullName}");
        }
    }
}
