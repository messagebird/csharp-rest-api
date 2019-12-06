using System.Text;
using MessageBird;
using MessageBird.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageBirdUnitTests.Resources
{
    [TestClass]
    public class RequestSignerTest
    {

        [TestMethod]
        public void TestIsMatchEmptyQueryParamsAndEmptyData() {
            var requestSigner = new RequestSigner(GetBytes("secret"));
            const string expectedSignature = "LISw4Je7n0/MkYDgVSzTJm8dW6BkytKTXMZZk1IElMs=";
            var request = new Request("1544544948", "", GetBytes(""));
    
            Assert.IsTrue(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestIsMatchWithData() {
            var requestSigner = new RequestSigner(GetBytes("secret"));
            const string expectedSignature = "p2e20OtAg39DEmz1ORHpjQ556U4o1ZaH4NWbM9Q8Qjk=";
            var request = new Request("1544544948", "", GetBytes("{\"a key\":\"some value\"}"));
    
            Assert.IsTrue(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestIsMatchWithQueryParams() {
            var requestSigner = new RequestSigner(GetBytes("secret"));
            const string expectedSignature = "Tfn+nRUBsn6lQgf6IpxBMS1j9lm7XsGjt5xh47M3jCk=";
            var request = new Request("1544544948", "abc=foo&def=bar", GetBytes(""));
    
            Assert.IsTrue(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestIsMatchWithShuffledQueryParams() {
            var requestSigner = new RequestSigner(GetBytes("secret"));
            const string expectedSignature = "Tfn+nRUBsn6lQgf6IpxBMS1j9lm7XsGjt5xh47M3jCk=";
            var request = new Request("1544544948", "def=bar&abc=foo", GetBytes(""));
    
            Assert.IsTrue(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestIsMatchWithDataAndQueryParams() {
            var requestSigner = new RequestSigner(GetBytes("other-secret"));
            const string expectedSignature = "orb0adPhRCYND1WCAvPBr+qjm4STGtyvNDIDNBZ4Ir4=";
            var request = new Request("1544544948", "abc=foo&def=bar", GetBytes("{\"a key\":\"some value\"}"));
    
            Assert.IsTrue(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestIsNotMatch() {
            var requestSigner = new RequestSigner(GetBytes("secret"));
            const string expectedSignature = "";
            var request = new Request("1544544948", "abc=foo&def=bar", GetBytes("{\"a key\":\"some value\"}"));
    
            Assert.IsFalse(requestSigner.IsMatch(expectedSignature, request));
        }
    
        [TestMethod]
        public void TestWithRealSignature() {
            /*
             * Here we use real signature from MessageBird webhook call
             */
    
            var requestSigner = new RequestSigner(GetBytes("Wb3N9gKeFf8ZoCzlOb5lJSic7bHLUcSu"));
            const string requestSignature = "5Jha9Yyhwgc1nTsgJ9WyzeHilsuUumydICdf4LuIZE8=";
            const string requestTimestamp = "1547036603";
            const string requestParams = "id=57db52e04e2f4001b555f79813a0f503&mccmnc=20409&ported=0&recipient=31667788880&reference=curl&status=delivered&statusDatetime=2019-01-09T12%3A23%3A23%2B00%3A00";
            var requestBody = new byte[] {};
    
            const string spoiledSignature = "5Jha9Yyhwgc1nTsgJ9WyzeHilsuUumydICdf4LUIZE8=";
            const string spoiledTimestamp = "1547036605";
            const string spoiledParams = "id=57db52e04e2f4001b555f79813a0f503&mccmnc=20409&ported=0&recipient=31667788880&reference=curvy&status=delivered&statusDatetime=2019-01-09T12%3A23%3A23%2B00%3A00";
            var spoiledBody = GetBytes("get shit spoiled");
    
            Assert.IsTrue(
                requestSigner.IsMatch(requestSignature, new Request(requestTimestamp, requestParams, requestBody)),
                "Definitely valid signature is threaten as invalid"
            );
            Assert.IsFalse(
                requestSigner.IsMatch(spoiledSignature, new Request(requestTimestamp, requestParams, requestBody)),
                "Invalid signature is threaten as invalid"
            );
            Assert.IsFalse(
                requestSigner.IsMatch(requestSignature, new Request(spoiledTimestamp, requestParams, requestBody)),
                "Signature is still valid with replaced timestamp"
            );
            Assert.IsFalse(
                requestSigner.IsMatch(requestSignature, new Request(requestTimestamp, spoiledParams, requestBody)),
                "Signature is still valid with replaced params"
            );
            Assert.IsFalse(
                requestSigner.IsMatch(requestSignature, new Request(requestTimestamp, requestParams, spoiledBody)),
                "Signature is still valid with replaced body"
            );
        }
            
        /**
         * Helper to get the bytes the provided UTF-8 encoded string represents.
         */
        private static byte[] GetBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
    }
}
