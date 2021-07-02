using System;
using System.IO;
using System.Threading.Tasks;
using TextMagicClient.Api;
using TextMagicClient.Client;
using TextMagicClient.Model;

namespace TextMagic.Service
{
    public interface ITextMagicClient
    {
        Task<Tuple<int?, SendMessageResponse>> SendMessage(string msgText, string phones);
    }
}
