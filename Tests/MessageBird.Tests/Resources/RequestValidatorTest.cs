using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using JWT;
using MessageBird;
using MessageBird.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace MessageBird.Tests.Resources
{
    [TestClass]
    public class RequestValidatorTest
    {
        private static Dictionary<string, string> errorMap = new Dictionary<string, string>
        {
            { "invalid jwt: claim nbf is in the future", "Token is not yet valid." },
            { "invalid jwt: claim exp is in the past", "Token has expired." },
            { "invalid jwt: signature is invalid", "Invalid signature" },
            { "invalid jwt: signing method none is invalid", "Specified argument was out of the range of valid values" }
        };

        [DynamicData(nameof(TestData), DynamicDataDisplayName = nameof(TestCaseDisplayName))]
        [TestMethod()]
        public void TestValidateSignature(TestCase tc)
        {
            string signingKey = tc.secret != null && tc.secret.Length > 0 ? tc.secret : "";
            RequestValidator requestValidator = new RequestValidator(signingKey, new StaticDateTimeProvider(tc.timestamp));

            byte[] body = tc.payload != null && tc.payload.Length > 0 ? Encoding.UTF8.GetBytes(tc.payload) : null;

            Func<IDictionary<string, string>> runValidateSignature = () => requestValidator.ValidateSignature(tc.token, tc.url, body);

            if (tc.valid)
            {
                var decoded = runValidateSignature();
                Assert.IsNotNull(decoded);
            }
            else
            {
                string expectedErrorMessage = errorMap.ContainsKey(tc.reason) ? errorMap[tc.reason] : tc.reason;

                var err = Assert.ThrowsException<RequestValidationException>(runValidateSignature);

                StringAssert.Contains(err.Message, expectedErrorMessage);
            }
        }

        public static string TestCaseDisplayName(MethodInfo methodInfo, object[] testCases) =>
            $"{methodInfo.Name}_{((TestCase)testCases[0]).name}";

        public static IEnumerable<object[]> TestData
        {
            get
            {
                var json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"Resources", "webhooksignature.json"));
                List<TestCase> tcs = JsonConvert.DeserializeObject<List<TestCase>>(json);

                foreach (var tc in tcs)
                {
                    yield return new[] { tc };
                }
            }
        }

        public sealed class StaticDateTimeProvider : IDateTimeProvider
        {
            private readonly DateTimeOffset _now;

            public StaticDateTimeProvider(string timestamp)
            {
                _now = DateTimeOffset.Parse(timestamp);
            }

            public DateTimeOffset GetNow()
            {
                return _now;
            }
        }
        public class TestCase
        {
            public string name { get; set; }
            public string secret { get; set; }
            public string url { get; set; }
            public string payload { get; set; }
            public string timestamp { get; set; }
            public string token { get; set; }
            public bool valid { get; set; }
            public string reason { get; set; }
        }
    }
}
