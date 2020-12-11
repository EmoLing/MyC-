using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Linq;
using Telegram.Bot;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types;
using System.Threading;

namespace TelegramBot
{
    class Program
    {
        static TelegramBotClient bot;

        static void Main(string[] args)
        {
            string token = System.IO.File.ReadAllText(@"token.txt");

            bot = new TelegramBotClient(token);
            bot.OnMessage += MessageListener;
            bot.StartReceiving();
            Console.ReadKey();
        }

        private static void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            #region Заполнение коллекций 

            string path = "files.txt";

            List<string> file_name = new List<string>();

            Check(path, file_name);



            string path_photo = "photo.txt";
            
            List<string> photo_name = new List<string>();

            Check(path_photo, photo_name);


            string path_audio = "audio.txt";

            List<string> audio_name = new List<string>();

            Check(path_audio, audio_name);


            string path_video = "video.txt";

            List<string> video_name = new List<string>();

            Check(path_video, video_name);

            #endregion

            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");

            #region Обработка документов, фото, аудио и видео

            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Document:
                    {
                        Save_in_DB(e.Message.Document.FileName, path);
                        Download(e.Message.Document.FileId, e.Message.Document.FileName);
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    {
                        var photo = e.Message.Photo;
                        var unic_name_photo = photo[photo.Length - 1].FileId;

                        Save_in_DB($"{unic_name_photo}.jpg", path_photo);
                        Download(unic_name_photo, $"{unic_name_photo}.jpg");
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Audio:
                    {
                        Save_in_DB(e.Message.Audio.FileId, path_audio);
                        Download(e.Message.Audio.FileId, $"{e.Message.Audio.FileId}.mp3");
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Video:
                    {
                        Save_in_DB(e.Message.Video.FileId, path_video);
                        Download(e.Message.Video.FileId, $"{e.Message.Video.FileId}.mp4");
                        break;
                    }
                default:
                    break;
            }

            #endregion

            #region Отправка файла

            foreach (string i in file_name)
            {
                if (e.Message.Text == i)
                {
                    Send(e, i);
                }
            }
            #endregion

            #region Обработка команд меню

            if (e.Message.Text == null) return;
            
            switch (e.Message.Text.ToString())
            {
                case null: return;
                case "/start":
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "/files - показ всех файлов \n" +
                                                                    "/pictures - показ картинок \n" +
                                                                    "/music - показ всех загруженных аудио \n" +
                                                                    "/video - показ всех загруженных видео \n" +
                                                                    "Название файла - загрузка файла с сервера бота");

                        break;
                    }
                case "/help":
                    {
                        bot.SendTextMessageAsync(e.Message.Chat.Id, "/files - показ списка загруженных файлов \n" +
                                                                    "/pictures - показ всех загруженных картинок \n" +
                                                                    "/music - показ всех загруженных аудио \n" +
                                                                    "/video - показ всех загруженных видео \n" +
                                                                    "Название файла - загрузка файла с сервера бота");
                        break;
                    }
                case "/files":
                    {
                        if (!System.IO.File.Exists(path))
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, "Файлов нет, вначале загрузите");
                        }
                            foreach (string i in file_name)
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, i);
                        }
                        break;
                    }
                case "/pictures":
                    {
                        if (!System.IO.File.Exists(path_photo))
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, "Картинок нет, вначале загрузите");
                        }
                            foreach (string i in photo_name)
                        {
                            Send(e, i,"картинка");
                        }
                        break;
                    }
                case "/music":
                    {
                        if (!System.IO.File.Exists(path_audio))
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, "Аудио нет, вначале загрузите");
                        }
                        foreach (string i in audio_name)
                        {
                            Send(e, i, 'a');
                        }
                        break;
                    }
                case "/video":
                    {
                        if (!System.IO.File.Exists(path_video))
                        {
                            bot.SendTextMessageAsync(e.Message.Chat.Id, "Видео нет, вначале загрузите");
                        }
                        foreach (string i in video_name)
                        {
                            Send(e, i, 1);
                        }
                        break;
                    }
                default:
                    {
                        var messageText = e.Message.Text;
                        bot.SendTextMessageAsync(e.Message.Chat.Id,
                            $"{messageText}"
                            );

                        break;
                    }
            }
            #endregion

        }

        static void Check (string path, List<string> list)
        {
            if (System.IO.File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string temp = streamReader.ReadToEnd();
                    string[] mass_temp = temp.Split("\t");
                    for (int i = 0; i < mass_temp.Length - 1; i++)
                    {
                        list.Add(mass_temp[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="fileid">ID файла</param>
        /// <param name="path">Путь</param>
        static async void Download(string fileid, string path)
        {
            var file = await bot.GetFileAsync(fileid);
            FileStream fileStream = new FileStream("_" + path, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, fileStream);
            fileStream.Close();

            fileStream.Dispose();
        }

        /// <summary>
        /// Отправка ботом файла
        /// </summary>
        /// <param name="e">Кому отправить</param>
        /// <param name="fileName">Название файла</param>
        static async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName)
        {
            // FileInfo fileInfo = new FileInfo($"_{fileName}");
            var file = System.IO.File.Open($"_{fileName}", FileMode.Open);
            await bot.SendDocumentAsync(e.Message.Chat.Id, document: new InputOnlineFile(file, fileName), caption: "Ваш файл");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Отправка картинки ботом
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="photo"></param>
        static async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName,string photo)
        {
            
            var file = System.IO.File.Open($"_{fileName}", FileMode.Open);
            await bot.SendPhotoAsync(e.Message.Chat.Id, photo: new InputOnlineFile(file, fileName), caption: "Картинка");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Отправка аудио ботом
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="photo"></param>
        static async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName, char audio)
        {

            var file = System.IO.File.Open($"_{fileName}", FileMode.Open);
            await bot.SendAudioAsync(e.Message.Chat.Id, audio: new InputOnlineFile(file, fileName), caption: "Песенка");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Отправка видео ботом
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="photo"></param>
        static async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName, int video)
        {

            var file = System.IO.File.Open($"_{fileName}", FileMode.Open);
            await bot.SendVideoAsync(e.Message.Chat.Id, video: new InputOnlineFile(file, fileName), caption: "Видео");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Сохранение списка файлов 
        /// </summary>
        /// <param name="fileName"> Название файла</param>
        /// <param name="path"> Пуит к файлу</param>
        static async void Save_in_DB(string fileName, string path)
        {

            if (System.IO.File.Exists(path))
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(path))
                {
                    fileStream.Close();
                    await System.IO.File.AppendAllTextAsync(path, $"{fileName}\t");
                }
            }
            else
            {
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    fileStream.Close();
                    await System.IO.File.AppendAllTextAsync(path, $"{fileName}\t");
                }
            }

        }
    }
}
