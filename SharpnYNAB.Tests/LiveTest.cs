﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

namespace SharpnYNAB.Tests
{
    public class LiveTest
    {
        [Fact]
        public async Task ApiScalingOk()
        {
            var args = JsonConvert.DeserializeObject<Args>(File.ReadAllText("ynab.conf"));
            args.BudgetName = "Test Budget - Dont Remove";
            var factory = new ClientFactory();
            var client = await factory.CreateClient(args);
            await client.Sync();
            var transaction = client.Budget.Transactions.FirstOrDefault(tr => tr.Memo == "TEST TRANSACTION");
            Assert.Equal(12.34, transaction?.Amount);
        }
    }
}
