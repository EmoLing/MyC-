using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Diagnostics;
using Telegram.Bot.Types.InputFiles;
using Newtonsoft.Json;
namespace UIbot
{
    class TelegramMessageClient
    {

        private MainWindow w;

        private TelegramBotClient bot;
        public ObservableCollection<MessageLog> BotMessageLog { get; set; }

        private void MessageListener(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            string folder = e.Message.Chat.Id.ToString();
            if (!Directory.Exists($"{folder})"))
            {
                Directory.CreateDirectory($"{folder}");
            }

            #region Заполнение коллекций 

            string path = $"{folder}/files.txt";

            List<string> file_name = new List<string>();

            Check(path, file_name);



            string path_photo = $"{folder}/photo.txt";

            List<string> photo_name = new List<string>();

            Check(path_photo, photo_name);


            string path_audio = $"{folder}/audio.txt";

            List<string> audio_name = new List<string>();

            Check(path_audio, audio_name);


            string path_video = $"{folder}/video.txt";

            List<string> video_name = new List<string>();

            Check(path_video, video_name);

            #endregion

            string text = $"{DateTime.Now.ToLongTimeString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Debug.WriteLine($"{text} TypeMessage: {e.Message.Type.ToString()}");

            #region Обработка документов, фото, аудио и видео

            switch (e.Message.Type)
            {
                case Telegram.Bot.Types.Enums.MessageType.Document:
                    {
                        Save_in_DB(e.Message.Document.FileName, path);
                        Download(e.Message.Document.FileId, $"{folder}/{e.Message.Document.FileName}");
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Photo:
                    {
                        var photo = e.Message.Photo;
                        var unic_name_photo = photo[photo.Length - 1].FileId;

                        Save_in_DB($"{unic_name_photo}.jpg", path_photo);
                        Download(unic_name_photo, $"{folder}/{unic_name_photo}.jpg");
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Audio:
                    {
                        Save_in_DB(e.Message.Audio.FileId, path_audio);
                        Download(e.Message.Audio.FileId, $"{folder}/{e.Message.Audio.FileId}.mp3");
                        break;
                    }
                case Telegram.Bot.Types.Enums.MessageType.Video:
                    {
                        Save_in_DB(e.Message.Video.FileId, path_video);
                        Download(e.Message.Video.FileId, $"{folder}/{e.Message.Video.FileId}.mp4");
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
                    Send(e.Message.Chat.Id, $"{folder}/{i}");
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
                            Send(e, $"{folder}/{i}", "картинка");
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
                            Send(e, $"{folder}/{i}", 'a');
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
                            Send(e, $"{folder}/{i}", 1);
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

            w.Dispatcher.Invoke(() =>
            {
                BotMessageLog.Add(
                new MessageLog(
                    DateTime.Now.ToLongTimeString(), e.Message.Type.ToString(), e.Message.Text, e.Message.Chat.FirstName, e.Message.Chat.Id));
                string json = JsonConvert.SerializeObject(BotMessageLog);
                File.WriteAllText($"{e.Message.Chat.Id}.json", json);
            });
        }
        public TelegramMessageClient(MainWindow W, string PathToken = "token.txt")
        {
            this.BotMessageLog = new ObservableCollection<MessageLog>();
            this.w = W;

            bot = new TelegramBotClient(File.ReadAllText(PathToken));

            bot.OnMessage += MessageListener;
            bot.StartReceiving();
        }

        public void SendMessage(string Text, string Id)
        {
            if (Id != string.Empty)
            {
                long id = Convert.ToInt64(Id);
                bot.SendTextMessageAsync(id, Text);
            }
        }
        public void Send(string Text, string Id)
        {
            if (Id != string.Empty)
            {
                long id = Convert.ToInt64(Id);
                Send(id, Text);
            }
        }

        public static void Check(string path, List<string> list)
        {
            if (System.IO.File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string temp = streamReader.ReadToEnd();
                    string[] mass_temp = temp.Split('\t');
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
        public async void Download(string fileid, string path)
        {
            var file = await bot.GetFileAsync(fileid);
            FileStream fileStream = new FileStream(path, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, fileStream);
            fileStream.Close();

            fileStream.Dispose();
        }

        /// <summary>
        /// Отправка ботом файла
        /// </summary>
        /// <param name="e">Кому отправить</param>
        /// <param name="fileName">Название файла</param>
        public async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName)
        {
            // FileInfo fileInfo = new FileInfo($"_{fileName}");
            var file = System.IO.File.Open($"{fileName}", FileMode.Open);
            await bot.SendDocumentAsync(e.Message.Chat.Id, document: new InputOnlineFile(file, fileName), caption: "Ваш файл");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Ручная рассылка с помощью бота
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        public async void Send(long e, string fileName)
        {
            // FileInfo fileInfo = new FileInfo($"_{fileName}");
            var file = System.IO.File.Open($"{fileName}", FileMode.Open);
            await bot.SendDocumentAsync(e, document: new InputOnlineFile(file, fileName));
            file.Close();
            file.Dispose();

        }
        /// <summary>
        /// Отправка картинки ботом
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fileName"></param>
        /// <param name="photo"></param>
        public async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName, string photo)
        {

            var file = System.IO.File.Open($"{fileName}", FileMode.Open);
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
        public async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName, char audio)
        {

            var file = System.IO.File.Open($"{fileName}", FileMode.Open);
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
        public async void Send(Telegram.Bot.Args.MessageEventArgs e, string fileName, int video)
        {

            var file = System.IO.File.Open($"{fileName}", FileMode.Open);
            await bot.SendVideoAsync(e.Message.Chat.Id, video: new InputOnlineFile(file, fileName), caption: "Видео");
            file.Close();
            file.Dispose();

        }

        /// <summary>
        /// Сохранение списка файлов 
        /// </summary>
        /// <param name="fileName"> Название файла</param>
        /// <param name="path"> Пуит к файлу</param>
        public void Save_in_DB(string fileName, string path)
        {

            if (System.IO.File.Exists(path))
            {
                using (FileStream fileStream = System.IO.File.OpenWrite(path))
                {
                    fileStream.Close();
                    System.IO.File.AppendAllText(path, $"{fileName}\t");
                }
            }
            else
            {
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    fileStream.Close();
                    System.IO.File.AppendAllText(path, $"{fileName}\t");
                }
            }

        }
    }
}
