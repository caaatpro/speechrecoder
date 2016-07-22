using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpeechRecoder
{
    class Program
    {
        static void Main(string[] args)
        {
            var _uuid = "01ae13cb744628b58fb536d496daa1e6";
            var _apiKey = "707dd6ff-c3b7-4d7c-a138-5b182cdd9643";
            var _topic = "";
            var _lang = "";

            string postUrl = "https://asr.yandex.net/asr_xml?" +
            "uuid="+ _uuid + "&" +
            "key=" + _apiKey + "&" +
            "topic=notes";

            // Читаем данные из файла
            byte[] bytes = File.ReadAllBytes("f_839578dffa412ef2.wav");


            // Созадём запрос
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(postUrl);
            request.Method = "POST";
            request.Host = "asr.yandex.net";
            request.SendChunked = true;
            request.ContentType = "audio/x-wav";
            request.ContentLength = bytes.Length;

            // Отправляем данные
            using (var newStream = request.GetRequestStream())
            {
                newStream.Write(bytes, 0, bytes.Length);
            }

            // Обробатываем ответ сервера
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseToString = "";
            if (response != null)
            {
                var strreader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                responseToString = strreader.ReadToEnd();
            }

            Console.WriteLine(responseToString);
            Console.ReadKey();
        }
    }
}
