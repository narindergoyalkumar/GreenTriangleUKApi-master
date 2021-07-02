using System;
using System.Threading.Tasks;
using TextMagicClient.Api;
using TextMagicClient.Client;
using TextMagicClient.Model;

namespace TextMagic.Service
{
    public class TextMagicClient : ITextMagicClient
    {
        private readonly TextMagicApi apiInstance = null;
        public TextMagicClient()
        {
            Configuration.Default.Username = "christianyeates1";
            Configuration.Default.Password = "PbLFwirTaeiAORSviHrZXsVA3Xigz1";
            Configuration.Default.BasePath = "https://rest.textmagic.com";
            apiInstance = new TextMagicApi();
        }
        public async Task<Tuple<int?, SendMessageResponse>> SendMessage(string msgText, string phones)
        {
            var sendMessageInputObject = new SendMessageInputObject
            {
                Text = msgText,
                Phones = phones,
                Destination = "mms",
                PartsCount = 6,
                ReferenceId = new Random().Next()
            };
            int? referenceId = sendMessageInputObject.ReferenceId;
            var result = await apiInstance.SendMessageAsync(sendMessageInputObject);
            return new Tuple<int?, SendMessageResponse>(referenceId, result);
        }
    }
}
